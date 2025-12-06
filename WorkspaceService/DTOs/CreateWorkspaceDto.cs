using System.ComponentModel.DataAnnotations;

namespace WorkspaceService.DTOs
{
    public class CreateWorkspaceDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string? Theme { get; set; }
        public string? IconUrl { get; set; } // URL para la imagen
        public Guid UserId { get; set; }     // Usuario autenticado (simulado)
    }
}
