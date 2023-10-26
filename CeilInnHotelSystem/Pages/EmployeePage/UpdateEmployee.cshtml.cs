using AutoMapper;
using CeilInnHotelSystem.Model;
using CeilInnHotelSystem.Models;
using CeilInnHotelSystem.Utility;
using CeilInnHotelSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace CeilInnHotelSystem.Pages.EmployeePage
{
    [Authorize(Roles = RoleConstant.ADMIN)]
    public class UpdateEmployeeModel : PageModel
    {
        private readonly CeilInnHotelDbContext _context;
        private readonly IMapper _mapper;

        [BindProperty]
        public EmployeeUpdateRequest EmployeeRequest { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; }
        public UpdateEmployeeModel(CeilInnHotelDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var emp = await _context.Employees.FirstOrDefaultAsync(i => i.Id == id);
            if(emp != null)
            {
                EmployeeRequest = _mapper.Map<EmployeeUpdateRequest>(emp);
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var old = await _context.Employees.FirstOrDefaultAsync(i => i.Id == EmployeeRequest.Id);
            if (old != null)
            {
                var newE = _mapper.Map<EmployeeUpdateRequest, Employee>(EmployeeRequest, old);
                _context.Employees.Update(newE);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/EmployeePage/Employee", new { pageIndex = PageIndex });
        }
    }
}
