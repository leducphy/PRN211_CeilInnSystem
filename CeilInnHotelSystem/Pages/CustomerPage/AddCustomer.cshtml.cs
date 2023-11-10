using AutoMapper;
using CeilInnHotelSystem.Model;
using CeilInnHotelSystem.Models;
using CeilInnHotelSystem.Utility;
using CeilInnHotelSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CeilInnHotelSystem.Pages.CustomerPage
{
    [Authorize(Roles = RoleConstant.ADMIN + "," + RoleConstant.EMPLOYEE)]
    public class AddCustomerModel : PageModel
    {
        private readonly CeilInnHotelDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        [BindProperty]
        public CustomerAddRequest CustomerAddRequest { get; set; }
        public AddCustomerModel(IMapper mapper, CeilInnHotelDbContext context, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> OnPostAsync()
        {

            var dept = _mapper.Map<Customer>(CustomerAddRequest);
            dept.Id = Guid.NewGuid();
            dept.Status = true;
            await _context.Customers.AddAsync(dept);
            await _context.SaveChangesAsync();

            //var user = new AppUser()
            //{
            //    Id = dept.Id.ToString(),
            //    FullName = dept.FirstName,
            //    Login = CustomerAddRequest.UserName,
            //    UserName = CustomerAddRequest.UserName,
            //    Type = 0,
            //    Activated = true,
            //};
            //var res = await _userManager.CreateAsync(user, CustomerAddRequest.Password);
            //if (res.Succeeded)
            //{
            //    await _userManager.AddToRoleAsync(user, RoleConstant.CUSTOMER);
            //}

            return RedirectToPage("/CustomerPage/Customer");
        }
    }
}
