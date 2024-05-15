using RepositoryProject.Areas.Student.Data;

namespace RepositoryProject.Service.Interface
{
    public interface IStudent
    {
        Task<int> SaveStudent(Students student);
        Task<IEnumerable<Students>> GetStudent();
        Task<Students> GetStudentById(int id);
        Task<string> CheckStuCode(string stuCode);
        Task<bool> DeleteStudentById(int id);
    }
}
