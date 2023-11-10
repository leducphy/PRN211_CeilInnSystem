using AutoMapper;
using CeilInnHotelSystem.Model;
using CeilInnHotelSystem.Models;
using CeilInnHotelSystem.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CeilInnHotelSystem.Pages.RoomPage
{
    public class AssignRoomModel : PageModel
    {
        private readonly CeilInnHotelDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        [BindProperty]
        public OccupancyAddModel OccupancyAddModel { get; set; }
        [BindProperty]
        public OccupancyAddModel RoomAssign { get; set; }
        public Room Room { get; set; }
        public List<Customer> CustomerList { get; set; }
        public AssignRoomModel(
            IMapper mapper, CeilInnHotelDbContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Room = _context.Rooms.ToList().FirstOrDefault(r => r.Id == id);
            if (Room == null)
            {
                return NotFound();
            }
            CustomerList = _context.Customers.Where(i => i.Status == true).ToList();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var occ = _mapper.Map<Occupancy>(OccupancyAddModel);
            occ.Id = Guid.NewGuid();
            occ.CreatedDate = DateTime.Now;
            await _context.AddAsync(occ);

            var room = await _context.Rooms.FirstOrDefaultAsync(i => i.Id == occ.RoomId);
            room.RoomStatus = false;

            await _context.SaveChangesAsync();

            return RedirectToPage("/RoomPage/Room");
        }
    }
}
