
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

        public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}