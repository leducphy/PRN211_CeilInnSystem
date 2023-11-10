using CeilInnHotelSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CeilInnHotelSystem.ViewModel
{
    public class OccupancyAddModel
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? RoomId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? RateApplied { get; set; }
        public double? PhoneCharge { get; set; }
    }

    public class OccupancyViewModel
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public Guid? RoomId { get; set; }
        public Room? Room { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? RateApplied { get; set; }
        public double? PhoneCharge { get; set; }
    }
}
