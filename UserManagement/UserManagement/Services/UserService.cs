using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserManagement.Data;
using UserManagement.Entities;
using Microsoft.EntityFrameworkCore;
using UserManagement.Pages;

namespace UserManagement.Services
{
    public class UserService
    {
        private readonly DataContext _context = default!;

        [BindProperty]
        public int GroupId { get; set; }


        [BindProperty]
        public string UserName { get; set; }

        public List<User> Users { get; set; }

        public List<SelectListItem> GroupItems { get; set; }

        public UserService(DataContext context)
        {
            _context = context;
        }

        public IList<User> GetUsers()
        {
            if (_context.Users != null)
            {
                return _context.Users.ToList();
            }
            return new List<User>();
        }

        public IList<System.Text.RegularExpressions.Group> GetGroups()
        {
            if (_context.Users != null)
            {
                return (IList<System.Text.RegularExpressions.Group>)_context.Groups.ToList();
            }
             return new List<System.Text.RegularExpressions.Group>();
        }

        public void AddUser(User user)
        {
            if (_context.Users != null)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            if (_context.Users != null)
            {
                var user = _context.Users.Find(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            //     return await _context.Users
            //.Include(u => u.Groups)
            //.ToListAsync(); 

            return await _context.Users
                .Include(u => u.Groups)
                .Include(u => u.Permissions)
                .ToListAsync();
        }

        public List<SelectListItem> GetGroupItems()
        {
            return _context.Groups
                .Select(g => new SelectListItem { Value = g.GroupId.ToString(), Text = g.GroupName })
                .ToList();
        }

        public async Task AddUserAsync(string userName, int groupId, List<int> permissionIds)
        {
            var user = new User { UserName = userName };
            var group = await _context.Groups.FindAsync(groupId);

            if (group != null)
            {
                user.Groups.Add(group);
                // Include permissions for the user
                user.Permissions = await _context.Permissions
                    .Where(p => permissionIds.Contains(p.PermissionId))
                    .ToListAsync();
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public List<SelectListItem> GetPermissionItems()
        {

            return _context.Permissions
                .Select(p => new SelectListItem { Value = p.PermissionId.ToString(), Text = p.PermissionName })
                .ToList();
        }

        public int GetUserCount()
        {
            return _context.Users.Count();
        }

        public Dictionary<string, int> GetUsersPerGroupCount()
        {
            var usersPerGroupCount = _context.Groups
                .Select(g => new
                {
                    GroupName = g.GroupName,
                    UserCount = g.Users.Count()
                })
                .ToDictionary(x => x.GroupName, x => x.UserCount);

            return usersPerGroupCount;
        }
         public void UpdateUser(User updatedUser)
        {
            var existingUser = _context.Users.Find(updatedUser.UserId);

            if (existingUser != null)
            {
                // Update user properties
                existingUser.UserName = updatedUser.UserName;
                existingUser.Groups = updatedUser.Groups; // Update groups if necessary
                existingUser.Permissions = updatedUser.Permissions; // Update permissions if necessary

                _context.SaveChanges();
            }
            // Handle error or validation logic if necessary
        }

        public User GetUserById(int userId)
        {
            return _context.Users
                .Include(u => u.Groups)  // Include related entities if necessary
                .Include(u => u.Permissions)
                .FirstOrDefault(u => u.UserId == userId);
        }

    }
    
}
