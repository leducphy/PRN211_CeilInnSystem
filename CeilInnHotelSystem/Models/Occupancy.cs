using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CeilInnHotelSystem.Models
{
    public class Occupancy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey("Employee")]
        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        [ForeignKey("Room")]
        public Guid? RoomId { get; set; }
        public Room? Room { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? PhoneCharge { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
