using WorkspaceService.Models;
using WorkspaceService.DTOs;
using WorkspaceService.Repository;

namespace WorkspaceService.Services
{
    public class WorkspaceServiceImpl
    {
        private readonly IWorkspaceRepository _repo;

        public WorkspaceServiceImpl(IWorkspaceRepository repo)
        {
            _repo = repo;
        }

        public Workspace Create(CreateWorkspaceDto dto)
        {
            // Validar nombre único
            if (_repo.GetByName(dto.Name) != null)
                throw new Exception("El nombre del espacio de trabajo ya existe.");

            var workspace = new Workspace
            {
                Name = dto.Name,
                Description = dto.Description,
                Theme = dto.Theme,
                IconUrl = dto.IconUrl,
                OwnerId = dto.UserId,
                Members = new List<WorkspaceMember>
                {
                    new WorkspaceMember
                    {
                        UserId = dto.UserId,
                        Role = "Propietario"
                    }
                }
            };

            _repo.Add(workspace);
            return workspace;
        }

        public List<Workspace> GetByUser(Guid userId)
        {
            return _repo.GetAll()
                .Where(w => w.IsActive && w.Members.Any(m => m.UserId == userId))
                .ToList();
        }

        public Workspace? GetById(Guid id) => _repo.GetById(id);

        public Workspace Update(Guid id, UpdateWorkspaceDto dto, Guid requesterId)
        {
            var ws = _repo.GetById(id) ?? throw new Exception("Workspace no encontrado.");

            var requester = ws.Members.FirstOrDefault(m => m.UserId == requesterId);
            if (requester == null || requester.Role != "Propietario")
                throw new Exception("No tienes permisos para editar este espacio.");

            if (dto.Name != null)
            {
                if (_repo.GetByName(dto.Name) != null)
                    throw new Exception("El nombre ya está en uso.");

                ws.Name = dto.Name;
            }

            if (dto.IconUrl != null)
                ws.IconUrl = dto.IconUrl;

            _repo.Update(ws);
            return ws;
        }

        public void SoftDelete(Guid id, Guid requesterId)
        {
            var ws = _repo.GetById(id) ?? throw new Exception("Workspace no encontrado.");

            if (ws.OwnerId != requesterId)
                throw new Exception("Solo el propietario puede eliminar este espacio.");

            ws.IsActive = false;
            _repo.Update(ws);
        }
    }
}
