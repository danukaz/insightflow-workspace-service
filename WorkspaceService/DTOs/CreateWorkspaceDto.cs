using System.ComponentModel.DataAnnotations;

namespace WorkspaceService.DTOs
{
    public class CreateWorkspaceDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string? Theme { get; set; }
        public IFormFile Icon { get; set; } // archivo
        public Guid UserId { get; set; }     // Usuario autenticado (simulado)
    }
}
