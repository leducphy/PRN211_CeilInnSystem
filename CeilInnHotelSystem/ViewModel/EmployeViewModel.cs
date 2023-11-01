using CeilInnHotelSystem.Models;

namespace CeilInnHotelSystem.ViewModel
{
    public class EmployeeAddRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Title { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
    public class EmployeeUpdateRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Title { get; set; }
    }

    public class EmployeeViewModel
    {
        public Employee employee { get; set; }
        public string? UserName { get; set; }
    }
}
