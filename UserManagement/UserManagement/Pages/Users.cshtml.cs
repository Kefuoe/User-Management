
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserManagement.Entities;
using UserManagement.Services;


namespace UserManagement.Pages
{
    public class Users : PageModel
    {
     
            private readonly UserService _service;
            public List<User> UserList { get; set; }
            public List<SelectListItem> GroupItems { get; set; }
            public List<SelectListItem> PermissionItems { get; set; }

            [BindProperty]
            public User NewUser { get; set; } = new User();

            [BindProperty]
            public int GroupId { get; set; }

            [BindProperty]
            public List<int> SelectedPermissionIds { get; set; } 


            public Users(UserService service)
            {
                _service = service;
            }

        public void OnGetGroups()
        {
            GroupItems = _service.GetGroupItems();
            PermissionItems = _service.GetPermissionItems();

        }
        public async Task OnGet()
        {
            //UserList = _service.GetUsers();
            UserList = await _service.GetAllUsersAsync();
            OnGetGroups();
        }


        public IActionResult OnPostDelete(int id)
        {
            _service.DeleteUser(id);

            return RedirectToAction("Get");
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