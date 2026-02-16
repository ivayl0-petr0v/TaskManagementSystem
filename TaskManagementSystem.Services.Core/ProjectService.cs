using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TaskManagementSystem.Data.Models;
using TaskManagementSystem.Services.Core.Interfaces;
using TaskManagementSystem.ViewModels.Project;
using TaskManagementSystem.Web.Data;

namespace TaskManagementSystem.Services.Core
{
    using static TaskManagementSystem.GCommon.ApplicationConstants;

    public class ProjectService : IProjectService
    {
        private readonly TaskManagementDbContext dbContext;

        public ProjectService(TaskManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ProjectAllViewModel>> GetAllProjectsAsync()
        {
            return await dbContext.Projects
                    .AsNoTracking()
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
                IsOwner = currentUserId?.ToLowerInvariant() == project.UserId.ToLowerInvariant(),
                IsCompleted = project.Status.Name == "Completed"
            };
        }

        public async Task<ProjectInputModel> GetProjectForCreateAsync()
        {
            return new ProjectInputModel
            {
                Statuses = await GetSelectProjectStatusesAsync(),
                Categories = await GetSelectProjectCategoriesAsync()
            };
        }

        public async Task<int> CreateProjectAsync(ProjectInputModel inputModel, string currentUserId)
        {
            bool statusExists = await dbContext
                .Statuses
                .AnyAsync(s => s.Id == inputModel.StatusId);
            bool categoryExists = await dbContext
                .Categories
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

            dbContext.Projects.Add(project);
            await dbContext.SaveChangesAsync();

            return project.Id;
        }

        public async Task<ProjectEditInputModel?> GetProjectForEditAsync(int id, string currentUserId)
        {
            Project? project = await FindProjectById(id);

            if (project == null || project.UserId != currentUserId)
            {
                return null;
            }

            if (project.UserId.ToLowerInvariant() != currentUserId.ToLowerInvariant())
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

            if (project.UserId.ToLowerInvariant() != currentUserId.ToLowerInvariant())
            {
                throw new UnauthorizedAccessException("You are not the owner of this project.");
            }

            //bool statusExists = await dbContext
            //    .Statuses
            //    .AnyAsync(s => s.Id == inputModel.StatusId);
            //bool categoryExists = await dbContext
            //    .Categories
            //    .AnyAsync(c => c.Id == inputModel.CategoryId);

            //if (!statusExists || !categoryExists)
            //{
            //    throw new ArgumentException("Invalid status and category.");
            //}

            project.Title = inputModel.Title;
            project.Description = inputModel.Description;
            project.DueDateTime = inputModel.DueDate;
            project.StatusId = inputModel.StatusId;
            project.CategoryId = inputModel.CategoryId;

            dbContext.Projects.Update(project);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ProjectDeleteViewModel?> GetProjectForDeleteAsync(int id, string currentUserId)
        {
            Project? project = await dbContext
                .Projects
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return null;
            }

            if (project.UserId.ToLowerInvariant() != currentUserId.ToLowerInvariant())
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
            Project? project = await FindProjectById(id);
            if (project == null)
            {
                throw new ArgumentException("Project not found");
            }

            if (project.UserId.ToLowerInvariant() != currentUserId.ToLowerInvariant())
            {
                throw new UnauthorizedAccessException("You are not the owner of this project.");
            }

            dbContext.Projects.Remove(project);
            await dbContext.SaveChangesAsync();
        }

        public async Task CompleteProjectAsync(int id, string currentUserId)
        {
            Project? project = await dbContext
                .Projects
                .Include(p => p.Status)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                throw new ArgumentException("Project not found");
            }

            if (project.UserId.ToLowerInvariant() != currentUserId.ToLowerInvariant())
            {
                throw new UnauthorizedAccessException("You are not the owner of this project.");
            }

            if (project.Status?.Name != "Completed")
            {
                Status? completedStatus = await dbContext
                    .Statuses
                    .FirstOrDefaultAsync(s => s.Name == "Completed");
                if (completedStatus != null)
                {
                    project.Status = completedStatus;
                    dbContext.Projects.Update(project);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private async Task<Project?> GetCurrentProject(int id)
        {
            return await dbContext
                .Projects
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Status)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        private async Task<IEnumerable<SelectProjectStatusViewModel>> GetSelectProjectStatusesAsync()
        {
            IEnumerable<SelectProjectStatusViewModel> projectStatuses = await dbContext
                .Statuses
                .AsNoTracking()
                .Select(s => new SelectProjectStatusViewModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToArrayAsync();

            return projectStatuses;
        }

        private async Task<IEnumerable<SelectProjectCategoryViewModel>> GetSelectProjectCategoriesAsync()
        {
            IEnumerable<SelectProjectCategoryViewModel> projectCategories = await dbContext
                .Categories
                .AsNoTracking()
                .Select(c => new SelectProjectCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArrayAsync();

            return projectCategories;
        }

        private async Task<Project?> FindProjectById(int id)
        {
            return await dbContext
                .Projects
                .FindAsync(id);
        }
    }
}
