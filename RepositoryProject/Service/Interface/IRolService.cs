using RepositoryProject.Areas.Student.Data;

namespace RepositoryProject.Service.Interface
{
    public interface IRolService
    {
        Task<List<Rol>> List();
    }
}
