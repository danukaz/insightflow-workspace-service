using System;
using System.Collections.Generic;

namespace WorkspaceService.Models
{
    public class WorkspaceMember
    {
        public Guid UserId { get; set; }
        public string Role { get; set; } = "Editor"; // Propietario o Editor
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }

    public class Workspace
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Theme { get; set; }
        public string? IconUrl { get; set; }
        public Guid OwnerId { get; set; }
        public List<WorkspaceMember> Members { get; set; } = new();
        public bool IsActive { get; set; } = true;
    }
}
