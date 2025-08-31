using HotelBookingAPI.Models;
using HotelBookingAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Services
{
    public class BookingService
    {
        private readonly BookingDbContext _db;

        public BookingService(BookingDbContext db)
        {
            _db = db;
        }

        public async Task<(bool Success, string ErrorMessage, BookingDto? Booking)> CreateBookingAsync(CreateBookingDto dto, Hotel hotel)
        {

            var room = hotel.Rooms.FirstOrDefault(r => r.RoomNumber == dto.RoomNumber);
            if (room == null)
            {
                var errorMessage = "Room number " + dto.RoomNumber + " not found in hotel " + dto.HotelName;
                return (false, errorMessage, null);
            }

            bool available = await IsRoomAvailable(room.Id, dto.CheckIn, dto.CheckOut);
            if (!available)
            {
                var errorMessage = "Room number " + dto.RoomNumber + " is not available for the selected dates.";
                return (false, errorMessage, null);
            }

            string bookingReference = await GenerateBookingReferenceAsync();

            var booking = new Booking
            {
                BookingReference = bookingReference,
                RoomId = room.Id,
                Room = room,
                NumberOfGuests = dto.NumberOfGuests,
                GuestName = dto.GuestName,
                CheckIn = dto.CheckIn,
                CheckOut = dto.CheckOut,
            };

            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();

            var bookingDto = new BookingDto
            {
                BookingReference = booking.BookingReference,
                HotelName = hotel.Name,
                RoomNumber = room.RoomNumber,
                GuestName = booking.GuestName,
                NumberOfGuests = booking.NumberOfGuests,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut
            };

            return (true, string.Empty, bookingDto);
        }

        public async Task<BookingDto?> GetBookingByReferenceAsync(string bookingReference)
        {
            return await _db.Bookings
                .Where(b => b.BookingReference == bookingReference)
                .Select(b => new BookingDto
                {
                    BookingReference = b.BookingReference,
                    HotelName = b.Room.Hotel.Name,
                    RoomNumber = b.Room.RoomNumber,
                    GuestName = b.GuestName,
                    NumberOfGuests = b.NumberOfGuests,
                    CheckIn = b.CheckIn,
                    CheckOut = b.CheckOut
                })
                .FirstOrDefaultAsync();
        }

        private async Task<string> GenerateBookingReferenceAsync()
        {
            string bookingReference;
            bool exists;

            do
            {
                bookingReference = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
                exists = await _db.Bookings.AnyAsync(b => b.BookingReference == bookingReference);

            } while (exists);

            return "B-" + bookingReference;
        }

        private async Task<bool> IsRoomAvailable(int roomId, DateOnly checkIn, DateOnly checkOut)
        {
            var room = await _db.Rooms
                .Include(r => r.Bookings)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null)
                return false;

            return room.Bookings.All(b => b.CheckOut <= checkIn || b.CheckIn >= checkOut);
        }
    }
}