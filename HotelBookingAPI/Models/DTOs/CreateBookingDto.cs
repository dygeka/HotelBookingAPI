using HotelBookingAPI.Validation;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Models.DTOs
{
    public class CreateBookingDto
    {
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
        [DateGreaterThan("CheckIn", ErrorMessage = "Check out cannot be before check in.")]
        public DateOnly CheckOut { get; set; }

    }
}
