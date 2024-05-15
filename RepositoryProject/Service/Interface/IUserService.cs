using RepositoryProject.Areas.Student.Data;

namespace RepositoryProject.Service.Interface
{
    public interface IUserService
    {
        Task<List<User>> List();
        Task<User> Add(User entity);
        Task<User> Edit(User entity);
        Task<bool> Delete(int idUser);
        Task<User> GetByCredentials(string email, string password);
        Task<User> GetById(int IdUser);
    }
}
