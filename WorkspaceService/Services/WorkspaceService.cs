using WorkspaceService.Models;
using WorkspaceService.DTOs;
using WorkspaceService.Repository;

namespace WorkspaceService.Services
{
    public class WorkspaceServiceImpl
    {
        private readonly IWorkspaceRepository _repo;
        private readonly CloudinaryService _cloudinary;

        public WorkspaceServiceImpl(IWorkspaceRepository repo, CloudinaryService cloudinary)
        {
            _repo = repo;
            _cloudinary = cloudinary;

        }

        public async Task<Workspace> CreateAsync(CreateWorkspaceDto dto)
        {
            // validar nombre único
            if (_repo.GetByName(dto.Name) != null)
                throw new Exception("El nombre ya existe.");

            // subir imagen
            var iconUrl = await _cloudinary.UploadImageAsync(dto.Icon);

            var workspace = new Workspace
            {
                Name = dto.Name,
                Description = dto.Description,
                Theme = dto.Theme,
                IconUrl = iconUrl,
                OwnerId = dto.UserId,
                Members = new List<WorkspaceMember>
        {
            new WorkspaceMember { UserId = dto.UserId, Role = "Propietario" }
        }
            };

            _repo.Add(workspace);
            return workspace;
        }

        public List<Workspace> GetAll()
        {
            return _repo.GetAll().Where(w => w.IsActive).ToList();
        }
        public List<Workspace> GetByUser(Guid userId)
        {
            return _repo.GetAll()
                .Where(w => w.IsActive && w.Members.Any(m => m.UserId == userId))
                .ToList();
        }

        public Workspace? GetById(Guid id) => _repo.GetById(id);

        public async Task<Workspace> UpdateAsync(Guid id, UpdateWorkspaceDto dto, Guid requesterId)
        {
            var ws = _repo.GetById(id) ?? throw new Exception("Workspace no encontrado.");

            var requester = ws.Members.FirstOrDefault(m => m.UserId == requesterId);
            if (requester == null || requester.Role != "Propietario")
                throw new Exception("No tienes permisos.");

            if (dto.Name != null)
            {
                if (_repo.GetByName(dto.Name) != null)
                    throw new Exception("El nombre ya está en uso.");

                ws.Name = dto.Name;
            }

            // Si se sube nueva imagen →
            if (dto.NewIcon != null)
            {
                var newUrl = await _cloudinary.UploadImageAsync(dto.NewIcon);
                ws.IconUrl = newUrl;
            }

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
