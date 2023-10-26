using AutoMapper;
using CeilInnHotelSystem.Model;
using CeilInnHotelSystem.Models;
using CeilInnHotelSystem.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace CeilInnHotelSystem.Pages.OccupancyPage
{
    public class OccupancyModel : PageModel
    {

        private readonly CeilInnHotelDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        [BindProperty]
        public OccupancyAddModel OccupancyAddModel { get; set; }
        [BindProperty]
        public List<Room> RoomList { get; set; }
        public OccupancyModel(IMapper mapper, CeilInnHotelDbContext context, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            RoomList = _context.Rooms.ToList();
           
            return Page();
        }
    }
}
