using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data.Models;
using TaskManagementSystem.Data.Repository.Contracts;
using TaskManagementSystem.Services.Core.Contracts.Project;
using TaskManagementSystem.Services.Core.Interfaces;

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

        private async Task<IEnumerable<LookupDto>> GetSelectProjectStatusesAsync()
        {
            return await statusRepository
                .GetAllStatusesAsNoTracking()
                .Select(s => new LookupDto
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToArrayAsync();
        }

        private async Task<IEnumerable<LookupDto>> GetSelectProjectCategoriesAsync()
        {
            return await categoryRepository
                .GetAllCategoriesAsNoTracking()
                .Select(c => new LookupDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArrayAsync();
        }

        private async Task<Project?> FindProjectById(int id)
        {
            return await projectRepository
                .GetByIdAsync(id);
        }

        public async Task<ProjectCreateDataDto> GetProjectCreateDataAsync()
        {
            return new ProjectCreateDataDto
            {
                Statuses = await GetSelectProjectStatusesAsync(),
                Categories = await GetSelectProjectCategoriesAsync()
            };
        }

        private async Task<Project?> GetCurrentProject(int id)
        {
            return await projectRepository
                .GetByIdWithDetailsAsync(id);
        }

        public async Task<int> CreateProjectAsync(CreateProjectRequest request, string currentUserId)
        {
            bool statusExists = await statusRepository
                .ExistsStatusByIdAsync(request.StatusId);

            bool categoryExists = await categoryRepository
                .ExistsCategoryByIdAsync(request.CategoryId);

            if (!statusExists || !categoryExists)
            {
                throw new ArgumentException("Invalid status and category.");
            }

            Project project = new()
            {
                Title = request.Title,
                Description = request.Description,
                DueDateTime = request.DueDate,
                StatusId = request.StatusId,
                CategoryId = request.CategoryId,
                UserId = currentUserId
            };

            await projectRepository.AddAsync(project);
            await unitOfWork.SaveChangesAsync();

            return project.Id;
        }

        public async Task<IEnumerable<ProjectListItemDto>> GetAllProjectsAsync()
        {
            return await projectRepository
                    .GetAllForListAsNoTracking()
                    .Select(p => new ProjectListItemDto
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        DueDate = p.DueDateTime,
                        Status = p.Status.Name,
                        Category = p.Category.Name,
                        UserFullName = p.User.UserFullName,
                        UserId = p.UserId
                    })
                    .ToListAsync();
        }

        public async Task<ProjectDetailsDto?> GetProjectDetailsByIdAsync(int id, string currentUserId)
        {
            Project? project = await GetCurrentProject(id);

            if (project == null)
            {
                return null;
            }

            return new ProjectDetailsDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Category = project.Category.Name,
                DueDate = project.DueDateTime,
                Status = project.Status.Name,
                UserFullName = project.User.UserFullName,
                IsOwner = project.UserId.Equals(currentUserId, StringComparison.InvariantCultureIgnoreCase),
                IsCompleted = project.Status.Name == "Completed"
            };
        }

        public async Task<UpdateProjectRequest?> GetProjectForEditAsync(int id, string currentUserId)
        {
            Project? project = await FindProjectById(id);

            if (project == null || !project.UserId.Equals(currentUserId, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            return new UpdateProjectRequest
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                DueDate = project.DueDateTime,
                StatusId = project.StatusId,
                CategoryId = project.CategoryId
            };
        }

        public async Task EditProjectAsync(int id, UpdateProjectRequest inputModel, string currentUserId)
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

        public async Task<ProjectDeleteDto?> GetProjectForDeleteAsync(int id, string currentUserId)
        {
            Project? project = await projectRepository.
                GetByIdAsNoTrackingAsync(id);

            if (project == null)
            {
                return null;
            }

            if (!project.UserId.Equals(currentUserId, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            return new ProjectDeleteDto
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
                .GetByIdWithStatusAsync(id) ?? throw new ArgumentException("Project not found");

            if (!project.UserId.Equals(currentUserId, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new UnauthorizedAccessException("You are not the owner of this project.");
            }

            if (project.Status?.Name != "Completed")
            {
                Status? completedStatus = await statusRepository
                    .GetByStatusNameAsync();

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