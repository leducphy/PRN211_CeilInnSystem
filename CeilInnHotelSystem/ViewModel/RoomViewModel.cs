namespace CeilInnHotelSystem.ViewModel
{
    public class RoomViewModel
    {
        public Guid Id { get; set; }
        public string RoomType { get; set; }
        public string BedType { get; set; }
        public float? Rate { get; set; }
        public bool? RoomStatus { get; set; }
        public bool? Status { get; set; }
    }
}
