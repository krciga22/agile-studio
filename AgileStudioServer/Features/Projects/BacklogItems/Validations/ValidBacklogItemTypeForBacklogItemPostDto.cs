using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEntries;
using AgileStudioServer.Features.Projects.Projects;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Features.Projects.BacklogItems.Validations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidBacklogItemTypeForBacklogItemPostDto : ValidationAttribute
    {
        public string GetErrorMessage() => "Invalid Backlog Item Type for Project";

        public override bool RequiresValidationContext => true;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var projectService = (ProjectService?)validationContext.GetService(
                typeof(ProjectService)) ??
                throw new ServiceNotFoundException(nameof(ProjectService));

            var backlogItemTypeService = (BacklogItemTypeService?)validationContext.GetService(
                typeof(BacklogItemTypeService)) ??
                throw new ServiceNotFoundException(nameof(BacklogItemTypeService));

            var backlogItemTypeSchemaEntryService = (BacklogItemTypeSchemaEntryService?)validationContext.GetService(
                typeof(BacklogItemTypeSchemaEntryService)) ??
                throw new ServiceNotFoundException(nameof(BacklogItemTypeSchemaEntryService));

            var dto = (BacklogItemPostDto)value;

            var project = projectService.Get(dto.ProjectId) ??
                throw new ModelNotFoundException(nameof(ProjectModel), dto.ProjectId.ToString());

            var backlogItemType = backlogItemTypeService.Get(dto.BacklogItemTypeId) ??
                throw new ModelNotFoundException(nameof(BacklogItemTypeModel), dto.BacklogItemTypeId.ToString());

            try
            {
                // todo backlogItemType.ID might be a parent type
                var backlogItemTypeSchemaEntry = backlogItemTypeSchemaEntryService.GetByChildTypeIdAndSchemaId(
                    backlogItemType.ID, project.BacklogItemTypeSchemaID);
            }
            catch(ModelNotFoundException)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
