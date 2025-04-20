using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.Projects.Projects
{
    public class ProjectRepository : EntityRepository<DBContext, ProjectModel, Project, int>
    {
        public ProjectRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(ProjectModel model)
        {
            return model.ID;
        }

        public virtual List<ProjectModel> GetAll()
        {
            List<Project> entities = _DbContext.Project.ToList();

            return HydrateModels(entities);
        }

        public virtual List<ProjectModel> GetByCreatedByUserId(int userId)
        {
            List<Project> entities = _DbContext.Project.Where(project =>
                project.CreatedBy != null && project.CreatedBy.ID == userId).ToList();

            return HydrateModels(entities);
        }

        protected override DbSet<Project> GetDbSet()
        {
            return _DbContext.Project;
        }
    }
}
