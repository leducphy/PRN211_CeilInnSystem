using CeilInnHotelSystem.Models;
using CeilInnHotelSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CeilInnHotelSystem.Pages.CustomerPage
{
    [Authorize(Roles = RoleConstant.ADMIN + "," + RoleConstant.EMPLOYEE)]
    public class CustomerModel : PageModel
    {
        private readonly CeilInnHotelDbContext _context;

        public List<Customer> ListCustomer { get; set; }
        [BindProperty]
        public string? Keyword { get; set; }
        [BindProperty]
        public bool? Status { get; set; }
        public int PageIndex { get; set; } = 1;
        public int TotalPage { get; set; }
        public CustomerModel(CeilInnHotelDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string? keyword, bool? status, int pageIndex, int pagesize)
        {
            
            Keyword = keyword;
            Status = status;
            if (pageIndex == 0) pageIndex = 1;
            PageIndex = pageIndex;
            pagesize = 4;
            var search = await Search(keyword, pageIndex, pagesize);
            ListCustomer = search.Data.ToList();
            TotalPage = (int)(Math.Ceiling(search.TotalCount / (double)pagesize));
            return Page();
        }

        public async Task<PagedList<Customer>> Search(string? keyword, int page, int pagesize)
        {
            var query = _context.Customers.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => (!string.IsNullOrEmpty(c.FirstName) && c.FirstName.Contains(keyword.ToLower().Trim()))
                                      || (!string.IsNullOrEmpty(c.LastName) && c.LastName.Contains(keyword.ToLower().Trim())));
            }
            var query2 = await query.Skip((page - 1) * pagesize)
                .Take(pagesize).ToListAsync();
            var res = await query.ToListAsync();
            return new PagedList<Customer>
            {
                Data = query2,
                TotalCount = res.Count
            };
        }
    }
}
