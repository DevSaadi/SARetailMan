using RepositoryProject.Areas.Student.Data;
using RepositoryProject.Service.Interface;

namespace RepositoryProject.Service
{
    public class RolService : IRolService
    {
        private readonly IGenericRepository<Rol> _repository;
        public RolService(IGenericRepository<Rol> repository)
        {
            _repository = repository;
        }
        public async Task<List<Rol>> List()
        {
            IQueryable<Rol> query = await _repository.Query();
            return query.ToList();
        }

    }
}