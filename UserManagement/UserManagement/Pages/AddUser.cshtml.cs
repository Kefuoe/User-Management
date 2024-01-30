

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserManagement.Entities;
using UserManagement.Services;

namespace UserManagement.Pages
{
    public class AddUser : PageModel
    {
        private readonly UserService _service;

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public int GroupId { get; set; }

        public List<SelectListItem> GroupItems { get; set; }

        [BindProperty]
        public User NewUser { get; set; } = new User();


        [BindProperty]
        public List<int> SelectedPermissionIds { get; set; } = new List<int>();


        public AddUser(UserService service)
        {
            _service = service;
        }

        public void OnnGetGroups()
        {
            GroupItems = _service.GetGroupItems();
        }

        public async Task<IActionResult> OnPostAddUserAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Call the AddUserAsync method from the UserService
            await _service.AddUserAsync(NewUser.UserName, GroupId, SelectedPermissionIds);

            // Redirect to the Users page after successfully adding the user
            return RedirectToPage("Users");
        }
    }
}
