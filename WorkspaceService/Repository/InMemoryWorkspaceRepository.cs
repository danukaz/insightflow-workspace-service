using WorkspaceService.Models;

namespace WorkspaceService.Repository
{
    public class InMemoryWorkspaceRepository : IWorkspaceRepository
    {
        private static readonly List<Workspace> _workspaces = new();

        public List<Workspace> GetAll() => _workspaces;

        public Workspace? GetById(Guid id) =>
            _workspaces.FirstOrDefault(w => w.Id == id);

        public Workspace? GetByName(string name) =>
            _workspaces.FirstOrDefault(w => w.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        public void Add(Workspace workspace) => _workspaces.Add(workspace);

        public void Update(Workspace workspace) 
        {
            // Nada especial porque es referencia en memoria
        }
    }
}
