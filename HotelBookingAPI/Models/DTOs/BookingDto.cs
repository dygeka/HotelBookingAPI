using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Models.DTOs
{
    public class BookingDto
    {
        [Required]
        public  string BookingReference { get; set; } = string.Empty;

        [Required]
        public string HotelName { get; set; } = string.Empty;

        [Required]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        public string GuestName { get; set; } = string.Empty;

        [Required]
        public int NumberOfGuests { get; set; }

        [Required]
        public DateOnly CheckIn { get; set; }

        [Required]
        public DateOnly CheckOut { get; set; }

    }
}
