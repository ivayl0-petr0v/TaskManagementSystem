using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TaskManagementSystem.Data.Models;
using TaskManagementSystem.Data.Repository.Contracts;
using TaskManagementSystem.Services.Core.Interfaces;
using TaskManagementSystem.ViewModels.Project;
using static TaskManagementSystem.GCommon.ApplicationConstants;

namespace TaskManagementSystem.Services.Core
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProjectRepository projectRepository;
        private readonly IStatusRepository statusRepository;
        private readonly ICategoryRepository categoryRepository;

        public ProjectService(
            IUnitOfWork unitOfWork,
            IProjectRepository projectRepository,
            IStatusRepository statusRepository,
            ICategoryRepository categoryRepository)
        {
            this.unitOfWork = unitOfWork;
            this.projectRepository = projectRepository;
            this.statusRepository = statusRepository;
            this.categoryRepository = categoryRepository;
        }

        private async Task<IEnumerable<SelectProjectStatusViewModel>> GetSelectProjectStatusesAsync()
        {
            return await statusRepository
                .AllAsNoTracking()
                .Select(s => new SelectProjectStatusViewModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToArrayAsync();
        }

        private async Task<IEnumerable<SelectProjectCategoryViewModel>> GetSelectProjectCategoriesAsync()
        {
            return await categoryRepository
                .AllAsNoTracking()
                .Select(c => new SelectProjectCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArrayAsync();
        }

        private async Task<Project?> FindProjectById(int id)
        {
            return await projectRepository.GetByIdAsync(id);
        }

        public async Task<ProjectInputModel> GetProjectForCreateAsync()
        {
            return new ProjectInputModel
            {
                Statuses = await GetSelectProjectStatusesAsync(),
                Categories = await GetSelectProjectCategoriesAsync()
            };
        }

        private async Task<Project?> GetCurrentProject(int id)
        {
            return await projectRepository
                .AllAsNoTracking()
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Status)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> CreateProjectAsync(ProjectInputModel inputModel, string currentUserId)
        {
            bool statusExists = await statusRepository
                .All()
                .AnyAsync(s => s.Id == inputModel.StatusId);
            bool categoryExists = await categoryRepository
                .All()
                .AnyAsync(c => c.Id == inputModel.CategoryId);

            if (!statusExists || !categoryExists)
            {
                throw new ArgumentException("Invalid status and category.");
            }

            Project project = new Project
            {
                Title = inputModel.Title,
                Description = inputModel.Description,
                DueDateTime = inputModel.DueDate,
                StatusId = inputModel.StatusId,
                CategoryId = inputModel.CategoryId,
                UserId = currentUserId
            };

            await projectRepository.AddAsync(project);
            await unitOfWork.SaveChangesAsync();

            return project.Id;
        }

        public async Task<IEnumerable<ProjectAllViewModel>> GetAllProjectsAsync()
        {
            return await projectRepository.AllAsNoTracking()
                    .OrderBy(p => p.Title)
                    .ThenBy(p => p.DueDateTime)
                    .Select(p => new ProjectAllViewModel
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        DueDate = p.DueDateTime.ToString(DateFormat, CultureInfo.InvariantCulture),
                        Status = p.Status.Name,
                        Category = p.Category.Name,
                        UserFullName = p.User.UserFullName,
                        UserId = p.UserId
                    })
                    .ToListAsync();
        }

        public async Task<ProjectDetailsViewModel?> GetProjectDetailsByIdAsync(int id, string currentUserId)
        {
            Project? project = await GetCurrentProject(id);

            if (project == null)
            {
                return null;
            }

            return new ProjectDetailsViewModel
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Category = project.Category.Name,
                DueDate = project.DueDateTime.ToString(DateFormat, CultureInfo.InvariantCulture),
                Status = project.Status.Name,
                UserFullName = project.User.UserFullName,
                IsOwner = project.UserId.Equals(currentUserId, StringComparison.InvariantCultureIgnoreCase),
                IsCompleted = project.Status.Name == "Completed"
            };
        }

        public async Task<ProjectEditInputModel?> GetProjectForEditAsync(int id, string currentUserId)
        {
            Project? project = await FindProjectById(id);

            if (project == null || !project.UserId.Equals(currentUserId, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            return new ProjectEditInputModel
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                DueDate = project.DueDateTime,
                StatusId = project.StatusId,
                CategoryId = project.CategoryId,
                Statuses = await GetSelectProjectStatusesAsync(),
                Categories = await GetSelectProjectCategoriesAsync()
            };
        }

        public async Task EditProjectAsync(int id, ProjectEditInputModel inputModel, string currentUserId)
        {
            Project? project = await FindProjectById(id);

            if (project == null)
            {
                throw new ArgumentException("Project not found");
            }

            if (!project.UserId.Equals(currentUserId, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new UnauthorizedAccessException("You are not the owner of this project.");
            }

            project.Title = inputModel.Title;
            project.Description = inputModel.Description;
            project.DueDateTime = inputModel.DueDate;
            project.StatusId = inputModel.StatusId;
            project.CategoryId = inputModel.CategoryId;

            projectRepository.Update(project);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<ProjectDeleteViewModel?> GetProjectForDeleteAsync(int id, string currentUserId)
        {
            Project? project = await projectRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return null;
            }

            if (!project.UserId.Equals(currentUserId, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            return new ProjectDeleteViewModel
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description ?? string.Empty
            };
        }

        public async Task DeleteProjectAsync(int id, string currentUserId)
        {
            Project? project = await FindProjectById(id) ?? throw new ArgumentException("Project not found");

            if (!project.UserId.Equals(currentUserId, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new UnauthorizedAccessException("You are not the owner of this project.");
            }

            projectRepository.Remove(project);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task CompleteProjectAsync(int id, string currentUserId)
        {
            Project? project = await projectRepository
                .All()
                .Include(p => p.Status)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                throw new ArgumentException("Project not found");
            }

            if (!project.UserId.Equals(currentUserId, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new UnauthorizedAccessException("You are not the owner of this project.");
            }

            if (project.Status?.Name != "Completed")
            {
                Status? completedStatus = await statusRepository
                    .All()
                    .FirstOrDefaultAsync(s => s.Name == "Completed");

                if (completedStatus != null)
                {
                    project.Status = completedStatus;
                    projectRepository.Update(project);
                    await unitOfWork.SaveChangesAsync();
                }
            }
        }
    }
}