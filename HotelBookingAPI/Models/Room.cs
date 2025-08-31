namespace HotelBookingAPI.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public RoomType Type { get; set; }

        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
