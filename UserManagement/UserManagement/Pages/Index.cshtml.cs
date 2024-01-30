using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Entities;

namespace UserManagement.Pages
{
    public class IndexModel : PageModel
    {
        // private readonly DataContext _context;

        //public IndexModel(DataContext context)
        //{
        //    _context = context;
        //}
        //public List<User> Users { get; set; }

        //public async Task OnGetAsync()
        //{
        //    Users = await _context.Users.ToListAsync();
        //}

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}