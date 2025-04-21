using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItems;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItems.Validations;
using AgileStudioServer.Data;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItems;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServerTest.CoreFeatures.Projects.Projects;
using AgileStudioServerTest.CoreFeatures.Releases.Releases;
using AgileStudioServerTest.CoreFeatures.Sprints.Sprints;
using AgileStudioServerTest.CoreFeatures.Workflows.Workflows;
using AgileStudioServerTest.CoreFeatures.Workflows.WorkflowStates;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.BacklogItems.BacklogItems
{
    public class BacklogItemValidationTest : DBTest
    {
        private readonly BacklogItemFixture _BacklogItemFixture;

        private readonly BacklogItemTypeFixture _BacklogItemTypeFixture;

        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;

        private readonly WorkflowFixture _WorkflowFixture;

        private readonly WorkflowStateFixture _WorkflowStateFixture;

        private readonly SprintFixture _SprintFixture;
        
        private readonly ReleaseFixture _ReleaseFixture;

        private readonly ProjectFixture _ProjectFixture;

        private readonly IServiceProvider? _ServiceProvider;

        public BacklogItemValidationTest(
            DBContext dbContext,
            IServiceProvider? serviceProvider,
            BacklogItemFixture backlogItemFixture,
            BacklogItemTypeFixture backlogItemTypeFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            WorkflowFixture workflowFixture,
            WorkflowStateFixture workflowStateFixture,
            SprintFixture sprintFixture,
            ReleaseFixture releaseFixture,
            ProjectFixture projectFixture) : base(dbContext)
        {
            _ServiceProvider = serviceProvider;
            _BacklogItemFixture = backlogItemFixture;
            _BacklogItemTypeFixture = backlogItemTypeFixture;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _WorkflowFixture = workflowFixture;
            _WorkflowStateFixture = workflowStateFixture;
            _SprintFixture = sprintFixture;
            _ReleaseFixture = releaseFixture;
            _ProjectFixture = projectFixture;
        }

        [Fact]
        public void PostBacklogItem_WithBacklogItemTypeFromSameProjectsSchema_IsValid()
        {
            var project = _ProjectFixture.Create();
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Get(
                        project.BacklogItemTypeSchemaID);
            var backlogItemType = _BacklogItemTypeFixture.Create(
                    backlogItemTypeSchema: backlogItemTypeSchema);
            var workflowState = _WorkflowStateFixture.Create();
            var attribute = new ValidBacklogItemTypeForBacklogItemPostDto();
            var backlogItem = new BacklogItemPostDto("Valid Backlog Item", project.ID, backlogItemType.ID, workflowState.ID);

            var result = attribute.GetValidationResult(backlogItem, CreateValidationContext(backlogItem));

            Assert.Null(result);
        }

        [Fact]
        public void PostBacklogItem_WithBacklogItemTypeFromDifferentProjectsSchema_IsInvalid()
        {
            var project = _ProjectFixture.Create();
            var otherBacklogItemTypeSchema = _BacklogItemTypeSchemaFixture.Create();
            var backlogItemTypeInvalid = _BacklogItemTypeFixture.Create(
                    backlogItemTypeSchema: otherBacklogItemTypeSchema);
            var workflowState = _WorkflowStateFixture.Create();
            var attribute = new ValidBacklogItemTypeForBacklogItemPostDto();
            var backlogItem = new BacklogItemPostDto("Invalid Backlog Item", project.ID, backlogItemTypeInvalid.ID, workflowState.ID);

            var result = attribute.GetValidationResult(backlogItem, CreateValidationContext(backlogItem));

            Assert.IsType<ValidationResult>(result);
        }

        [Fact]
        public void PostBacklogItem_WithSprintForSameProject_IsValid()
        {
            var project = _ProjectFixture.Create();
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Get(
                        project.BacklogItemTypeSchemaID);
            var backlogItemType = _BacklogItemTypeFixture.Create(
                    backlogItemTypeSchema: backlogItemTypeSchema);
            var workflowState = _WorkflowStateFixture.Create();
            var sprint = _SprintFixture.Create(project: project);

            var backlogItemPostDto = new BacklogItemPostDto(
                title: "Test Backlog Item",
                projectId: project.ID,
                backlogItemTypeId: backlogItemType.ID,
                workflowStateId: workflowState.ID);

            backlogItemPostDto.SprintId = sprint.ID;

            var attribute = new ValidSprintForBacklogItem();
            var result = attribute.GetValidationResult(backlogItemPostDto, CreateValidationContext(backlogItemPostDto));

            Assert.Null(result);
        }

        [Fact]
        public void PostBacklogItem_WithSprintForDifferentProject_IsInvalid()
        {
            var project1 = _ProjectFixture.Create();
            var project2 = _ProjectFixture.Create();
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Get(
                        project1.BacklogItemTypeSchemaID);
            var backlogItemType = _BacklogItemTypeFixture.Create(
                    backlogItemTypeSchema: backlogItemTypeSchema);
            var workflowState = _WorkflowStateFixture.Create();
            var sprint = _SprintFixture.Create(project: project2);

            var backlogItemPostDto = new BacklogItemPostDto(
                title: "Test Backlog Item",
                projectId: project1.ID,
                backlogItemTypeId: backlogItemType.ID,
                workflowStateId: workflowState.ID);

            backlogItemPostDto.SprintId = sprint.ID;

            var attribute = new ValidSprintForBacklogItem();
            var result = attribute.GetValidationResult(backlogItemPostDto, CreateValidationContext(backlogItemPostDto));

            Assert.IsType<ValidationResult>(result);
        }

        [Fact]
        public void PatchBacklogItem_WithSprintForSameProject_IsValid()
        {
            var project = _ProjectFixture.Create();
            var sprint = _SprintFixture.Create(project: project);
            var backlogItem = _BacklogItemFixture.Create(
                project: project,
                sprint: sprint
            );

            var backlogItemPatchDto = new BacklogItemPatchDto(
                id: backlogItem.ID,
                title: "Test Backlog Item",
                workflowStateId: backlogItem.WorkflowStateID);

            backlogItemPatchDto.SprintId = sprint.ID;

            var attribute = new ValidSprintForBacklogItem();
            var result = attribute.GetValidationResult(backlogItemPatchDto, CreateValidationContext(backlogItemPatchDto));

            Assert.Null(result);
        }

        [Fact]
        public void PatchBacklogItem_WithSprintForDifferentProject_IsInvalid()
        {
            var project1 = _ProjectFixture.Create();
            var project2 = _ProjectFixture.Create();
            var sprint1 = _SprintFixture.Create(project: project1);
            var sprint2 = _SprintFixture.Create(project: project2);
            var backlogItem = _BacklogItemFixture.Create(
                project: project1,
                sprint: sprint1
            );

            var backlogItemPatchDto = new BacklogItemPatchDto(
                id: backlogItem.ID,
                title: "Test Backlog Item",
                workflowStateId: backlogItem.WorkflowStateID);

            backlogItemPatchDto.SprintId = sprint2.ID;

            var attribute = new ValidSprintForBacklogItem();
            var result = attribute.GetValidationResult(backlogItemPatchDto, CreateValidationContext(backlogItemPatchDto));

            Assert.IsType<ValidationResult>(result);
        }

        [Fact]
        public void PostBacklogItem_WithReleaseForSameProject_IsValid()
        {
            var project = _ProjectFixture.Create();
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Get(
                        project.BacklogItemTypeSchemaID);
            var backlogItemType = _BacklogItemTypeFixture.Create(
                    backlogItemTypeSchema: backlogItemTypeSchema);
            var workflowState = _WorkflowStateFixture.Create();
            var release = _ReleaseFixture.Create(project: project);

            var backlogItemPostDto = new BacklogItemPostDto(
                title: "Test Backlog Item",
                projectId: project.ID,
                backlogItemTypeId: backlogItemType.ID,
                workflowStateId: workflowState.ID);

            backlogItemPostDto.ReleaseId = release.ID;

            var attribute = new ValidReleaseForBacklogItem();
            var result = attribute.GetValidationResult(backlogItemPostDto, CreateValidationContext(backlogItemPostDto));

            Assert.Null(result);
        }

        [Fact]
        public void PostBacklogItem_WithReleaseForDifferentProject_IsInvalid()
        {
            var project1 = _ProjectFixture.Create();
            var project2 = _ProjectFixture.Create();
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Get(
                        project1.BacklogItemTypeSchemaID);
            var backlogItemType = _BacklogItemTypeFixture.Create(
                    backlogItemTypeSchema: backlogItemTypeSchema);
            var workflowState = _WorkflowStateFixture.Create();
            var release = _ReleaseFixture.Create(project: project2);

            var backlogItemPostDto = new BacklogItemPostDto(
                title: "Test Backlog Item",
                projectId: project1.ID,
                backlogItemTypeId: backlogItemType.ID,
                workflowStateId: workflowState.ID);

            backlogItemPostDto.ReleaseId = release.ID;

            var attribute = new ValidReleaseForBacklogItem();
            var result = attribute.GetValidationResult(backlogItemPostDto, CreateValidationContext(backlogItemPostDto));

            Assert.IsType<ValidationResult>(result);
        }

        [Fact]
        public void PatchBacklogItem_WithReleaseForSameProject_IsValid()
        {
            var project = _ProjectFixture.Create();
            var release = _ReleaseFixture.Create(project: project);
            var backlogItem = _BacklogItemFixture.Create(
                project: project,
                release: release
            );

            var backlogItemPatchDto = new BacklogItemPatchDto(
                id: backlogItem.ID,
                title: "Test Backlog Item",
                workflowStateId: backlogItem.WorkflowStateID);

            backlogItemPatchDto.ReleaseId = release.ID;

            var attribute = new ValidReleaseForBacklogItem();
            var result = attribute.GetValidationResult(backlogItemPatchDto, CreateValidationContext(backlogItemPatchDto));

            Assert.Null(result);
        }

        [Fact]
        public void PatchBacklogItem_WithReleaseForDifferentProject_IsInvalid()
        {
            var project1 = _ProjectFixture.Create();
            var project2 = _ProjectFixture.Create();
            var release1 = _ReleaseFixture.Create(project: project1);
            var release2 = _ReleaseFixture.Create(project: project2);
            var backlogItem = _BacklogItemFixture.Create(
                project: project1,
                release: release1
            );

            var backlogItemPatchDto = new BacklogItemPatchDto(
                id: backlogItem.ID,
                title: "Test Backlog Item",
                workflowStateId: backlogItem.WorkflowStateID);

            backlogItemPatchDto.ReleaseId = release2.ID;

            var attribute = new ValidReleaseForBacklogItem();
            var result = attribute.GetValidationResult(backlogItemPatchDto, CreateValidationContext(backlogItemPatchDto));

            Assert.IsType<ValidationResult>(result);
        }

        [Fact]
        public void PostBacklogItem_WithWorkflowStateForSameWorkflow_IsValid()
        {
            var project = _ProjectFixture.Create();
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Get(
                        project.BacklogItemTypeSchemaID);
            var backlogItemType = _BacklogItemTypeFixture.Create(
                    backlogItemTypeSchema: backlogItemTypeSchema);
            var workflow = _WorkflowFixture.Get(
                        backlogItemType.WorkflowID);
            var workflowState = _WorkflowStateFixture.Create(
                    workflow: workflow);

            var backlogItemPostDto = new BacklogItemPostDto(
                title: "Test Backlog Item",
                projectId: project.ID,
                backlogItemTypeId: backlogItemType.ID,
                workflowStateId: workflowState.ID);

            var attribute = new ValidWorkflowStateForBacklogItem();
            var result = attribute.GetValidationResult(backlogItemPostDto, CreateValidationContext(backlogItemPostDto));

            Assert.Null(result);
        }

        [Fact]
        public void PostBacklogItem_WithWorkflowStateForDiferentWorkflow_IsValid()
        {
            var project = _ProjectFixture.Create();
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Get(
                        project.BacklogItemTypeSchemaID);
            var backlogItemType1 = _BacklogItemTypeFixture.Create(
                    backlogItemTypeSchema: backlogItemTypeSchema);
            var backlogItemType2 = _BacklogItemTypeFixture.Create(
                    backlogItemTypeSchema: backlogItemTypeSchema);
            var workflow = _WorkflowFixture.Get(backlogItemType1.WorkflowID);
            var workflowState = _WorkflowStateFixture.Create(
                    workflow: workflow);

            var backlogItemPostDto = new BacklogItemPostDto(
                title: "Test Backlog Item",
                projectId: project.ID,
                backlogItemTypeId: backlogItemType2.ID,
                workflowStateId: workflowState.ID);

            var attribute = new ValidWorkflowStateForBacklogItem();
            var result = attribute.GetValidationResult(backlogItemPostDto, CreateValidationContext(backlogItemPostDto));

            Assert.IsType<ValidationResult>(result);
        }

        [Fact]
        public void PatchBacklogItem_WithWorkflowStateForSameWorkflow_IsValid()
        {
            var project = _ProjectFixture.Create();
            var workflow = _WorkflowFixture.Create();
            var workflowState1 = _WorkflowStateFixture.Create(
                    workflow: workflow);
            var workflowState2 = _WorkflowStateFixture.Create(
                    workflow: workflow);
            var backlogItemType = _BacklogItemTypeFixture.Create(
                    workflow: workflow);
            var backlogItem = _BacklogItemFixture.Create(
                project: project,
                backlogItemType: backlogItemType,
                workflowState: workflowState1
            );

            var backlogItemPatchDto = new BacklogItemPatchDto(
                id: backlogItem.ID,
                title: "Test Backlog Item",
                workflowStateId: workflowState2.ID);

            var attribute = new ValidWorkflowStateForBacklogItem();
            var result = attribute.GetValidationResult(backlogItemPatchDto, CreateValidationContext(backlogItemPatchDto));

            Assert.Null(result);
        }

        [Fact]
        public void PatchBacklogItem_WithWorkflowStateForDiferentWorkflow_IsValid()
        {
            var project = _ProjectFixture.Create();
            var workflow1 = _WorkflowFixture.Create();
            var workflow2 = _WorkflowFixture.Create();
            var workflowState1 = _WorkflowStateFixture.Create(
                    workflow: workflow1);
            var workflowState2 = _WorkflowStateFixture.Create(
                    workflow: workflow2);
            var backlogItemType = _BacklogItemTypeFixture.Create(
                    workflow: workflow1);
            var backlogItem = _BacklogItemFixture.Create(
                project: project,
                backlogItemType: backlogItemType,
                workflowState: workflowState1
            );

            var backlogItemPatchDto = new BacklogItemPatchDto(
                id: backlogItem.ID,
                title: "Test Backlog Item",
                workflowStateId: workflowState2.ID);

            var attribute = new ValidWorkflowStateForBacklogItem();
            var result = attribute.GetValidationResult(backlogItemPatchDto, CreateValidationContext(backlogItemPatchDto));

            Assert.IsType<ValidationResult>(result);
        }

        [Fact]
        public void PostBacklogItem_WithParentBacklogItemForSameProject_IsValid()
        {
            var project = _ProjectFixture.Create();

            var parentBacklogItem = _BacklogItemFixture.Create(
                project: project
            );

            var backlogItemType = _BacklogItemTypeFixture.Create(
                    backlogItemTypeSchema: _BacklogItemTypeSchemaFixture.Get(
                        project.BacklogItemTypeSchemaID)
            );
            var workflowState = _WorkflowStateFixture.Create();
            var backlogItemPostDto = new BacklogItemPostDto(
                title: "Test Backlog Item",
                projectId: project.ID,
                backlogItemTypeId: backlogItemType.ID,
                workflowStateId: workflowState.ID
            );
            backlogItemPostDto.ParentBacklogItemId = parentBacklogItem.ID;

            var attribute = new ValidParentBacklogItemForBacklogItem();
            var result = attribute.GetValidationResult(
                backlogItemPostDto,
                CreateValidationContext(backlogItemPostDto)
            );

            Assert.Null(result);
        }

        [Fact]
        public void PostBacklogItem_WithParentBacklogItemForDifferentProject_IsInvalid()
        {
            var project1 = _ProjectFixture.Create();
            var project2 = _ProjectFixture.Create();

            var parentBacklogItem = _BacklogItemFixture.Create(
                project: project1
            );

            var backlogItemType = _BacklogItemTypeFixture.Create(
                    backlogItemTypeSchema: _BacklogItemTypeSchemaFixture.Get(
                        project2.BacklogItemTypeSchemaID)
            );
            var workflowState = _WorkflowStateFixture.Create();
            var backlogItemPostDto = new BacklogItemPostDto(
                title: "Test Backlog Item",
                projectId: project2.ID,
                backlogItemTypeId: backlogItemType.ID,
                workflowStateId: workflowState.ID
            );
            backlogItemPostDto.ParentBacklogItemId = parentBacklogItem.ID;

            var attribute = new ValidParentBacklogItemForBacklogItem();
            var result = attribute.GetValidationResult(
                backlogItemPostDto,
                CreateValidationContext(backlogItemPostDto)
            );

            Assert.IsType<ValidationResult>(result);
        }

        [Fact]
        public void PatchBacklogItem_WithParentBacklogItemForSameProject_IsValid()
        {
            var project = _ProjectFixture.Create();

            var parentBacklogItem = _BacklogItemFixture.Create(
                project: project
            );

            var backlogItem = _BacklogItemFixture.Create(
                project: project
            );

            var backlogItemPatchDto = new BacklogItemPatchDto(
                id: backlogItem.ID,
                title: backlogItem.Title,
                workflowStateId: backlogItem.WorkflowStateID
            );
            backlogItemPatchDto.ParentBacklogItemId = parentBacklogItem.ID;

            var attribute = new ValidParentBacklogItemForBacklogItem();
            var result = attribute.GetValidationResult(
                backlogItemPatchDto,
                CreateValidationContext(backlogItemPatchDto)
            );

            Assert.Null(result);
        }

        [Fact]
        public void PatchBacklogItem_WithParentBacklogItemForDifferentProject_IsInvalid()
        {
            var project1 = _ProjectFixture.Create();
            var project2 = _ProjectFixture.Create();

            var parentBacklogItem = _BacklogItemFixture.Create(
                project: project1
            );

            var backlogItem = _BacklogItemFixture.Create(
                project: project2
            );

            var backlogItemPatchDto = new BacklogItemPatchDto(
                id: backlogItem.ID,
                title: backlogItem.Title,
                workflowStateId: backlogItem.WorkflowStateID
            );
            backlogItemPatchDto.ParentBacklogItemId = parentBacklogItem.ID;

            var attribute = new ValidParentBacklogItemForBacklogItem();
            var result = attribute.GetValidationResult(
                backlogItemPatchDto,
                CreateValidationContext(backlogItemPatchDto)
            );

            Assert.IsType<ValidationResult>(result);
        }

        private ValidationContext CreateValidationContext(object instance)
        {
            if (_ServiceProvider is null)
            {
                throw new Exception("Service Provider is null");
            }

            return new ValidationContext(
                instance: instance,
                serviceProvider: _ServiceProvider,
                items: null
            );
        }
    }
}
