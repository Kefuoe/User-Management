using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Entities
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }

        [Required]
        public string PermissionName { get; set; }

        // Define the relationship with groups
        public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
    }

}