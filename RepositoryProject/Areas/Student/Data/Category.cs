using System.ComponentModel.DataAnnotations;

namespace RepositoryProject.Areas.Student.Data
{
    public class Category
    {
        [Key]
        public int IdCategory { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
