using CeilInnHotelSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CeilInnHotelSystem.ViewModel
{
    public class PaymentViewModel
    {
    }

    public class PaymentAddModel
    {
        
        public Guid Id { get; set; }
       
        public Guid? EmployeeId { get; set; }
      
        public Guid? CustomerId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public double? AmountCharged { get; set; }
        public double? TaxRate { get; set; }
    }
}
