using WorkspaceService.Models;
using WorkspaceService.Repository;

namespace WorkspaceService.Seeders
{
    public static class WorkspaceSeeder
    {
        public static void Seed(IWorkspaceRepository repo)
        {
            if (repo.GetAll().Any())
                return; // Evitar agregar datos duplicados

            var userA = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var userB = Guid.Parse("22222222-2222-2222-2222-222222222222");

            var ws1 = new Workspace
            {
                Name = "Proyecto Arquitectura",
                Description = "Workspace para el proyecto de arquitectura.",
                Theme = "Educación",
                IconUrl = "icon1.png",
                OwnerId = userA,
                Members = new List<WorkspaceMember>
                {
                    new WorkspaceMember { UserId = userA, Role = "Propietario" },
                    new WorkspaceMember { UserId = userB, Role = "Editor" }
                }
            };

            var ws2 = new Workspace
            {
                Name = "Investigación TI",
                Description = "Workspace de investigación sobre microservicios.",
                Theme = "Tecnología",
                IconUrl = "icon2.png",
                OwnerId = userB,
                Members = new List<WorkspaceMember>
                {
                    new WorkspaceMember { UserId = userB, Role = "Propietario" }
                }
            };

            repo.Add(ws1);
            repo.Add(ws2);
        }
    }
}
