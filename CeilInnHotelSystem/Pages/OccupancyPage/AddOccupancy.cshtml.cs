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
    public class AddOccupancyModel : PageModel
    {

        private readonly CeilInnHotelDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        [BindProperty]
        public OccupancyAddModel OccupancyAddModel { get; set; }
        [BindProperty]
        public List<Room> RoomList { get; set; }
        public List<Customer> CustomerList { get; set; }
        public AddOccupancyModel(IMapper mapper, CeilInnHotelDbContext context, UserManager<AppUser> userManager,
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
            CustomerList = _context.Customers.Where(i => i.Status == true).ToList();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var occ = _mapper.Map<Occupancy>(OccupancyAddModel);
            occ.Id = Guid.NewGuid();
            
            await _context.AddAsync(occ);
            await _context.SaveChangesAsync();

            return Page();
        }
    }
}
