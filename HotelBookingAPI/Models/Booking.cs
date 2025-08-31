namespace HotelBookingAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public Room? Room { get; set; }

        public string BookingReference { get; set; } = string.Empty; 
        public string GuestName { get; set; } = string.Empty;
        public int NumberOfGuests { get; set; }

        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
    }
}
