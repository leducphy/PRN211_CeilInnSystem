using CeilInnHotelSystem.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CeilInnHotelSystem.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AppUser> _signManager;

        public LogoutModel(SignInManager<AppUser> signInManager)
        {
            _signManager = signInManager;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            await _signManager.SignOutAsync();
            return RedirectToPage("/Login");
        }
    }
}
