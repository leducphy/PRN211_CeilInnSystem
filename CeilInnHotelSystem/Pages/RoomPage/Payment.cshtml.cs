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
    public class PaymentModel : PageModel
    {
        private readonly CeilInnHotelDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        [BindProperty]
        public PaymentAddModel paymentAddModel { get; set; }
        [BindProperty]
        public Occupancy occv { get; set; }
        [BindProperty]
        public string roomDetails { get; set; }


        public double stayDuration { get; set; }



        public PaymentModel(IMapper mapper, CeilInnHotelDbContext context,
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
            occv = await _context.Occupancies.Where(x => x.RoomId == id)
               .Include(i => i.Customer)
               .Include(i => i.Employee)
               .Include(i => i.Room)
               .OrderByDescending(i => i.CreatedDate).FirstOrDefaultAsync();

            TimeSpan totalDay = (TimeSpan)(occv.EndDate - occv.StartDate);
            stayDuration = totalDay.TotalHours / 24;

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var payment = _mapper.Map<Payment>(paymentAddModel);
            payment.Id = Guid.NewGuid();
            payment.PaymentDate = DateTime.Now;

            await _context.AddAsync(payment);

            var room = await _context.Rooms.FirstOrDefaultAsync(i => i.Id == Guid.Parse(roomDetails));
            var customer = await _context.Customers.FirstOrDefaultAsync(i => i.Id == payment.CustomerId);

            customer.Status = false;


            room.RoomStatus = true;


            _context.SaveChanges();

           

            return RedirectToPage("/RoomPage/Room");
        }
    }
}
