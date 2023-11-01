using AutoMapper;
using CeilInnHotelSystem.Model;
using CeilInnHotelSystem.Models;
using CeilInnHotelSystem.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CeilInnHotelSystem.Pages.OccupancyPage
{
    public class OccupancyModel : PageModel
    {
        private CeilInnHotelDbContext _context;

        public List<Occupancy> ListOccupancy { get; set; }
        [BindProperty]
        public string? Keyword { get; set; }
        [BindProperty]
        public bool? Status { get; set; }
        public int PageIndex { get; set; } = 1;
        public int TotalPage { get; set; }
        public OccupancyModel(CeilInnHotelDbContext context)
        {

            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(string? keyword, bool? status, int pageIndex, int pagesize)
        {


            Keyword = keyword;
            Status = status;
            if (pageIndex == 0) pageIndex = 1;
            PageIndex = pageIndex;
            pagesize = 10;
            var search = await Search(keyword, pageIndex, pagesize);
            ListOccupancy = search.Data.ToList();
            TotalPage = (int)(Math.Ceiling(search.TotalCount / (double)pagesize));
            return Page();
        }

        public async Task<PagedList<Occupancy>> Search(string? keyword, int page, int pagesize)
        {
            var query = _context.Occupancies.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => (!string.IsNullOrEmpty(c.Employee.FirstName) && c.Employee.FirstName.Contains(keyword.ToLower().Trim()))
                                      || (!string.IsNullOrEmpty(c.Employee.LastName) && c.Employee.LastName.Contains(keyword.ToLower().Trim()))
                                      || (!string.IsNullOrEmpty(c.Customer.FirstName) && c.Customer.FirstName.Contains(keyword.ToLower().Trim()))
                                      || (!string.IsNullOrEmpty(c.Customer.LastName) && c.Customer.LastName.Contains(keyword.ToLower().Trim())));
            }
            var query2 = await query.Include(i => i.Customer)
                                    .Include(i => i.Employee)
                                    .Include(i => i.Room)
                                    .Skip((page - 1) * pagesize)
                                    .Take(pagesize).ToListAsync();
            var res = await query.ToListAsync();
            return new PagedList<Occupancy>
            {
                Data = query2,
                TotalCount = res.Count
            };
        }
    }
}
