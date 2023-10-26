using CeilInnHotelSystem.Models;
using CeilInnHotelSystem.Utility;
using CeilInnHotelSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CeilInnHotelSystem.Pages.CustomerPage
{

    [Authorize(Roles = RoleConstant.ADMIN + "," + RoleConstant.EMPLOYEE)]
    public class DeleteCustomerModel : PageModel
    {
        private readonly CeilInnHotelDbContext _context;

        [BindProperty]
        public CustomerAddRequest Request { get; set; }
        public List<Employee> listClass { get; set; }
        public DeleteCustomerModel(CeilInnHotelDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var cus = await _context.Customers.FirstOrDefaultAsync(i => i.Id == id);
            if (cus != null)
            {
                cus.Status = false;
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("/CustomerPage/Customer");
        }
    }

}
