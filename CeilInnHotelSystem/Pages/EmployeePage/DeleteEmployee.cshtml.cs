using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CeilInnHotelSystem.Model;
using Microsoft.AspNetCore.Authorization;
using CeilInnHotelSystem.Utility;
using CeilInnHotelSystem.ViewModel;
using CeilInnHotelSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CeilInnHotelSystem.Pages.EmployeePage
{
    [Authorize(Roles = RoleConstant.ADMIN)]
    public class DeleteEmployeeModel : PageModel
    {
        private readonly CeilInnHotelDbContext _context;

        [BindProperty]
        public EmployeeAddRequest Request { get; set; }
        public List<Employee> listClass { get; set; }
        public DeleteEmployeeModel(CeilInnHotelDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var emp = await _context.Employees.FirstOrDefaultAsync(i => i.Id == id);
            if (emp != null)
            {
                emp.Status = false;
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("/EmployeePage/Employee");
        }
    }

}
