namespace WorkspaceService.DTOs
{
    public class UpdateWorkspaceDto
    {
    public string? Name { get; set; }
    public string? IconUrl { get; set; } // opcional
    public IFormFile? NewIcon { get; set; } // reemplazo real

    }
}
