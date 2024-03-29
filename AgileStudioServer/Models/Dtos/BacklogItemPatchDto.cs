﻿using AgileStudioServer.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Models.Dtos
{
    [ValidSprintForBacklogItem]
    [ValidReleaseForBacklogItem]
    [ValidWorkflowStateForBacklogItem]
    public class BacklogItemPatchDto
    {
        [Required]
        public int ID;

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [Required]
        public int WorkflowStateId { get; set; }

        public int? SprintId { get; set; } = null;

        public int? ReleaseId { get; set; } = null;

        public BacklogItemPatchDto(int id, string title, int workflowStateId, int? sprintId = null, int? releaseId = null)
        {
            ID = id;
            Title = title;
            WorkflowStateId = workflowStateId;
            SprintId = sprintId;
            ReleaseId = releaseId;
        }
    }
}
