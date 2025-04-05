using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Projects.Projects
{
    public class ProjectService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public ProjectService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<ProjectModel> GetAll()
        {
            List<Project> entities = _DBContext.Project.ToList();

            return HydrateProjectModels(entities);
        }

        public virtual List<ProjectModel> GetByCreatedByUserId(int userId)
        {
            List<Project> entities = _DBContext.Project.Where(project =>
                project.CreatedBy != null && project.CreatedBy.ID == userId).ToList();

            return HydrateProjectModels(entities);
        }

        public virtual ProjectModel? Get(int id)
        {
            Project? entity = _DBContext.Project.Find(id);
            if (entity is null)
            {
                return null;
            }

            return HydrateProjectModel(entity);
        }

        public virtual ProjectModel Create(ProjectModel project)
        {
            Project entity = HydrateProjectEntity(project);

            _DBContext.Project.Add(entity);
            _DBContext.SaveChanges();

            return HydrateProjectModel(entity);
        }

        public virtual ProjectModel Update(ProjectModel project)
        {
            Project entity = HydrateProjectEntity(project);

            _DBContext.Project.Update(entity);
            _DBContext.SaveChanges();

            return HydrateProjectModel(entity);
        }

        public virtual void Delete(ProjectModel project)
        {
            Project entity = HydrateProjectEntity(project);

            _DBContext.Project.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<ProjectModel> HydrateProjectModels(List<Project> entities, int depth = 3)
        {
            List<ProjectModel> models = new();

            entities.ForEach(entity =>
            {
                ProjectModel model = HydrateProjectModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private ProjectModel HydrateProjectModel(Project project, int depth = 3)
        {
            return (ProjectModel)_Hydrator.Hydrate(
                project, typeof(ProjectModel), depth
            );
        }

        private Project HydrateProjectEntity(ProjectModel project, int depth = 3)
        {
            return (Project)_Hydrator.Hydrate(
                project, typeof(Project), depth
            );
        }
    }
}
