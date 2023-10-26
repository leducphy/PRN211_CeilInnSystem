using System.ComponentModel.DataAnnotations;

namespace CeilInnHotelSystem.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string UserName{ get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
	}
}
