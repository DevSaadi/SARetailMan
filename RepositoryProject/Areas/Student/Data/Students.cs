using System.ComponentModel.DataAnnotations;

namespace RepositoryProject.Areas.Student.Data
{
    public class Students
    {
        [Key]
        public int Id { get; set; }

        public string studentCode { get; set; }
        public string studentName { get; set; }
        public string studentDepartment { get; set; }
        public string studentFee { get; set; }
    }
}
