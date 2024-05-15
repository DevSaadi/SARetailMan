using Microsoft.EntityFrameworkCore;
using RepositoryProject.Areas.Student.Data;
using RepositoryProject.DBContext;
using RepositoryProject.Service.Interface;

namespace RepositoryProject.Service
{
    public class StudentService : IStudent
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }


        //Student Info

        public async Task<int> SaveStudent(Students student)
        {
            if (student.Id != 0)
                _context.Students.Update(student);
            else
                _context.Students.Add(student);

            await _context.SaveChangesAsync();
            return student.Id;
        }

        public async Task<IEnumerable<Students>> GetStudent()
        {
            return await _context.Students.AsNoTracking().ToListAsync();
        }

        public async Task<Students> GetStudentById(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<string> CheckStuCode(string stuCode)
        {
            var result = await _context.Students.Where(x => x.studentCode == stuCode).Select(x => x.studentCode).FirstOrDefaultAsync();
            if (result != null)
            {
                result = "duplicate";
                return result;
            }
            return result;
        }

        public async Task<bool> DeleteStudentById(int id)
        {
            _context.Students.Remove(_context.Students.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
