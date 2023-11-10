using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CeilInnHotelSystem.Models
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string RoomType { get; set; }
        public string BedType { get; set; }
        public float Rate { get; set; }
        public bool RoomStatus { get; set; }
       

        public static implicit operator Room(Task<Room?> v)
        {
            throw new NotImplementedException();
        }
    }

}
