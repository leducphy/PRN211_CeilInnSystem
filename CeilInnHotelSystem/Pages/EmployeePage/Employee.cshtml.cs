using CeilInnHotelSystem.Model;
using CeilInnHotelSystem.Models;
using CeilInnHotelSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CeilInnHotelSystem.Pages.EmployeePage
{
    [Authorize(Roles = RoleConstant.ADMIN)]
    public class EmployeeModel : PageModel
    {
        private readonly CeilInnHotelDbContext _context;

        public List<Employee> ListEmployee { get; set; }
        [BindProperty]
        public string? Keyword { get; set; }
        [BindProperty]
        public bool? Status { get; set; }
        public int PageIndex { get; set; } = 1;
        public int TotalPage { get; set; }
        public EmployeeModel( CeilInnHotelDbContext context)
        {
           
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(string? keyword, bool? status, int pageIndex, int pagesize)
        {

            /*if (role.Contains(RoleConstant.TEACHER))
            {
                Keyword = keyword;
                Status = status;
                if (pageIndex == 0) pageIndex = 1;
                PageIndex = pageIndex;
                pagesize = 4;
                ListEmployee = await _repository.Search(keyword, status, user.Id, pageIndex, pagesize);
                TotalPage = (int)(Math.Ceiling(ListEmployee.TotalCount / (double)pagesize));
            }
            else if (role.Contains(RoleConstant.STUDENT))
            {
                var st = await _studentRepository.GetById(Guid.Parse(user.Id));
                Keyword = keyword;
                Status = status;
                if (pageIndex == 0) pageIndex = 1;
                PageIndex = pageIndex;
                pagesize = 4;
                ListEmployee = await _repository.SearchClassByStudent(keyword, status, st.Id, pageIndex, pagesize);
                TotalPage = (int)(Math.Ceiling(ListEmployee.TotalCount / (double)pagesize));
            }
            else
            {
                Keyword = keyword;
                Status = status;
                if (pageIndex == 0) pageIndex = 1;
                PageIndex = pageIndex;
                pagesize = 4;
                ListEmployee = await _repository.Search(keyword, status, null, pageIndex, pagesize);
                TotalPage = (int)(Math.Ceiling(ListEmployee.TotalCount / (double)pagesize));
            }*/
            Keyword = keyword;
            Status = status;
            if (pageIndex == 0) pageIndex = 1;
            PageIndex = pageIndex;
            pagesize = 4;
            var search = await Search(keyword, pageIndex, pagesize);
            ListEmployee = search.Data.ToList();
            TotalPage = (int)(Math.Ceiling(search.TotalCount / (double)pagesize));
            return Page();
        }

        public async Task<PagedList<Employee>> Search(string? keyword, int page, int pagesize)
        {
            var query = _context.Employees.Where(i => i.Status == true).AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => (!string.IsNullOrEmpty(c.FirstName) && c.FirstName.Contains(keyword.ToLower().Trim()))
                                      || (!string.IsNullOrEmpty(c.Title) && c.Title.Contains(keyword.ToLower().Trim())));
            }
            var query2 = await query.Skip((page - 1) * pagesize)
                .Take(pagesize).ToListAsync();
            var res = await query.ToListAsync();
            return new PagedList<Employee>
            {
                Data = query2,
                TotalCount = res.Count
            };
        }

    }
}
