using AutoMapper;
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
    public class UpdateCustomerModel : PageModel
    {
        private readonly CeilInnHotelDbContext _context;
        private readonly IMapper _mapper;

        [BindProperty]
        public CustomerUpdateRequest CustomerRequest { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; }
        public UpdateCustomerModel(CeilInnHotelDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var emp = await _context.Customers.FirstOrDefaultAsync(i => i.Id == id);
            if (emp != null)
            {
                CustomerRequest = _mapper.Map<CustomerUpdateRequest>(emp);
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var old = await _context.Customers.FirstOrDefaultAsync(i => i.Id == CustomerRequest.Id);
            if (old != null)
            {
                var newC = _mapper.Map<CustomerUpdateRequest, Customer>(CustomerRequest, old);
                _context.Customers.Update(newC);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/CustomerPage/Customer", new { pageIndex = PageIndex });
        }

    }
}
