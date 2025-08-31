using HotelBookingAPI.Models;
using HotelBookingAPI.Models.DTOs;

namespace HotelBookingAPI.Services
{
    public class RoomService
    {
        public RoomService()
        {
        }

        public List<RoomDto> GetAvailableRooms(Hotel hotel, DateOnly checkIn, DateOnly checkOut, int numberOfGuests)
        {
            var candidateRooms = hotel.Rooms
                .Where(r =>
                    (r.Type == RoomType.Single && numberOfGuests == 1) ||
                    ((r.Type == RoomType.Double || r.Type == RoomType.Deluxe) && numberOfGuests >=1)
                );

            var availableRooms = candidateRooms
                .Where(r => r.Bookings.All(b =>
                    b.CheckOut <= checkIn || b.CheckIn >= checkOut))
                .Select(r => new RoomDto
                {
                    RoomNumber = r.RoomNumber,
                    Type = r.Type.ToString(),
                    HotelName = r.Hotel.Name
                })
                .ToList();

            return availableRooms;
        }
    }
}
