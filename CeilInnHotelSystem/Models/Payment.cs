using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CeilInnHotelSystem.Models
{
    public class Payment
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
        public DateTime? PaymentDate { get; set; }
        public double? AmountCharged { get; set; }
        public double? TaxRate { get; set; }
    }
}
