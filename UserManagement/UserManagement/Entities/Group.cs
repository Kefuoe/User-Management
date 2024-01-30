using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Entities
{
    public class Group
    {
         [Key]
        public int GroupId { get; set; }

        [Required]
        public string GroupName { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();

        public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}