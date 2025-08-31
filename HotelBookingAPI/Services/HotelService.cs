using HotelBookingAPI.Models;
using HotelBookingAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Services
{
    public class HotelService
    {
        private readonly BookingDbContext _db;

        public HotelService(BookingDbContext db)
        {
            _db = db;
        }

        public async Task<Hotel?> GetHotelObjectAsync(string hotelName)
        {
            return await _db.Hotels
                .Include(h => h.Rooms)
                .ThenInclude(r => r.Bookings)
                .FirstOrDefaultAsync(h => h.Name == hotelName);
        }

        public async Task<HotelDto?> GetHotelDtoAsync(string hotelName)
        {
            return await _db.Hotels
                .Where(h => h.Name == hotelName)
                .Select(h => new HotelDto
                {
                    Name = h.Name,
                    Address = h.Address
                })
                .FirstOrDefaultAsync();
        }
    }
}