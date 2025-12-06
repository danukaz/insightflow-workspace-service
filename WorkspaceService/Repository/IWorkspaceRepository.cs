using WorkspaceService.Models;

namespace WorkspaceService.Repository
{
    public interface IWorkspaceRepository
    {
        List<Workspace> GetAll();
        Workspace? GetById(Guid id);
        Workspace? GetByName(string name);
        void Add(Workspace workspace);
        void Update(Workspace workspace);
    }
}
