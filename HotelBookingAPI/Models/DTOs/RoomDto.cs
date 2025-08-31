using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Models.DTOs
{
    public class RoomDto
    {
        [Required]
        public string HotelName { get; set; } = string.Empty;

        [Required]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty; // Enum as string

    }
}
