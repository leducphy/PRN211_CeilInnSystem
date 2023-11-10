using CeilInnHotelSystem.Models;
using CeilInnHotelSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Drawing.Printing;

namespace CeilInnHotelSystem.Pages.RoomPage
{
    [Authorize(Roles = RoleConstant.ADMIN + "," + RoleConstant.EMPLOYEE + "," + RoleConstant.CUSTOMER)]
    public class RoomModel : PageModel
    {
        private readonly CeilInnHotelDbContext _context;

        public List<Room> ListRoom { get; set; }
        [BindProperty]
        public string? Keyword { get; set; }
        [BindProperty]
        public bool? Status { get; set; }

        [BindProperty]
        public Occupancy occupancy { get; set; }
        public int PageIndex { get; set; } = 1;
        public int TotalPage { get; set; }
        public RoomModel(CeilInnHotelDbContext context)
        {

            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(string? keyword, bool? status, int pageIndex, int pagesize)
        {

            Keyword = keyword;
            Status = status;
            if (pageIndex == 0) pageIndex = 1;
            PageIndex = pageIndex;
            pagesize = 99999;
            var search = await Search(keyword, pageIndex, pagesize);
            ListRoom = search.Data.ToList();
            TotalPage = (int)(Math.Ceiling(search.TotalCount / (double)pagesize));
            return Page();
        }

        public async Task<PagedList<Room>> Search(string? keyword, int page, int pagesize)
        {
            var query = _context.Rooms.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => (!string.IsNullOrEmpty(c.RoomType) && c.RoomType.Contains(keyword.ToLower().Trim()))
                                      || (!string.IsNullOrEmpty(c.BedType) && c.BedType.Contains(keyword.ToLower().Trim())));
            }
            var query2 = await query.Skip((page - 1) * pagesize)
                .Take(pagesize).ToListAsync();
            var res = await query.ToListAsync();
            return new PagedList<Room>
            {
                Data = query2,
                TotalCount = res.Count
            };
        }

        public async Task<IActionResult> OnGetDetailOccupancy(string id)
        {
            var occ = await _context.Occupancies.Where(i => i.RoomId == Guid.Parse(id))
                .Include(i => i.Customer)
                .Include(i => i.Employee)
                .Include(i => i.Room)
                .OrderByDescending(i => i.CreatedDate).FirstOrDefaultAsync();
            if (occ != null)
            {
                occupancy = occ;
                ViewData["open"] = "yes";
            }

            var search = await Search("", 1, 100);
            ListRoom = search.Data.ToList();


            return Page();
        }
    }
}