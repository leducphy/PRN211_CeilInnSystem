using AutoMapper;
using CeilInnHotelSystem.Model;
using CeilInnHotelSystem.Models;
using CeilInnHotelSystem.Utility;
using CeilInnHotelSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CeilInnHotelSystem.Pages.EmployeePage
{
    [Authorize(Roles = RoleConstant.ADMIN)]
    public class AddEmployeeModel : PageModel
    {
        private readonly CeilInnHotelDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        [BindProperty]
        public EmployeeAddRequest EmployeeAddRequest { get; set; }
        public AddEmployeeModel(IMapper mapper, CeilInnHotelDbContext context, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> OnPostAsync()
        {

            var dept = _mapper.Map<Employee>(EmployeeAddRequest);
            dept.Id = Guid.NewGuid();
            dept.Status = true;
            await _context.Employees.AddAsync(dept);
            await _context.SaveChangesAsync();

            var user = new AppUser()
            {
                Id = dept.Id.ToString(),
                FullName = dept.FirstName,
                Login = EmployeeAddRequest.UserName,
                UserName = EmployeeAddRequest.UserName,
                Type = 0,
                Activated = true,
            };
            var res = await _userManager.CreateAsync(user, EmployeeAddRequest.Password);
            if (res.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RoleConstant.EMPLOYEE);
            }

            return RedirectToPage("/EmployeePage/Employee");
        }
    }
}
