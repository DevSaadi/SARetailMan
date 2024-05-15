using RepositoryProject.Areas.Student.Data;

namespace RepositoryProject.Areas.Student.Models
{
    public class StudentViewModels
    {
        public int studentId { get; set; }
        public string studentCode { get; set; }
        public string studentName { get; set; }
        public string studentDepartment { get; set; }
        public string studentFee { get; set; }

        public IEnumerable<Students> students;
    }
}
