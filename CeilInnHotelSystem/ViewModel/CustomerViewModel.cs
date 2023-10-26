namespace CeilInnHotelSystem.ViewModel
{
    public class CustomerAddRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmergencyName { get; set; }
        public string? EmergencyPhone { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public class CustomerUpdateRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmergencyName { get; set; }
        public string? EmergencyPhone { get; set; }
    }
}
