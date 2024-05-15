using System.ComponentModel.DataAnnotations;

namespace RepositoryProject.Areas.Student.Data
{
    public class Rol
    {
        //public Rol()
        //{
           
        //    Users = new HashSet<User>();
        //}
        [Key]
        public int IdRol { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}

