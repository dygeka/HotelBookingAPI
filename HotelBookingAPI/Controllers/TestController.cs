using HotelBookingAPI.Models;
using HotelBookingAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//in a prod environment this class would only be accessible when running in a development environment TODO FIX COMMENT
namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly BookingDbContext _db;

        public TestController(BookingDbContext db)
        {
            _db = db;
        }

        [HttpPost("seed")]
        public async Task<IActionResult> SeedTestData()
        {
            var hotelA = new Hotel
            {
                Name = "HotelA",
                Address = "123 A Street",
                Rooms = new List<Room>
                {
                    new Room { RoomNumber = "1", Type = RoomType.Single },
                    new Room { RoomNumber = "2", Type = RoomType.Single },
                    new Room { RoomNumber = "3", Type = RoomType.Double },
                    new Room { RoomNumber = "4", Type = RoomType.Double },
                    new Room { RoomNumber = "5", Type = RoomType.Deluxe },
                    new Room { RoomNumber = "6", Type = RoomType.Deluxe }
                }
            };

            var hotelB = new Hotel
            {
                Name = "HotelB",
                Address = "123 B Street",
                Rooms = new List<Room>
                {
                    new Room { RoomNumber = "1", Type = RoomType.Single },
                    new Room { RoomNumber = "2", Type = RoomType.Single },
                    new Room { RoomNumber = "3", Type = RoomType.Double },
                    new Room { RoomNumber = "4", Type = RoomType.Double },
                    new Room { RoomNumber = "5", Type = RoomType.Deluxe },
                    new Room { RoomNumber = "6", Type = RoomType.Deluxe }
                }
            };

            var testRoom1 = hotelA.Rooms.First();
            var testBooking1 = new Booking
            {
                RoomId = testRoom1.Id,
                Room = testRoom1,
                BookingReference = "B-12345678",
                GuestName = "Dougall",
                NumberOfGuests = 1,
                CheckIn = DateOnly.Parse("2025-09-06"),
                CheckOut = DateOnly.Parse("2025-09-09")
            };

            var testRoom2 = hotelB.Rooms.First();
            var testBooking2 = new Booking
            {
                RoomId = testRoom2.Id,
                Room = testRoom2,
                BookingReference = "B-87654321",
                GuestName = "Reece",
                NumberOfGuests = 1,
                CheckIn = DateOnly.Parse("2025-09-15"),
                CheckOut = DateOnly.Parse("2025-09-21")
            };

            _db.Hotels.Add(hotelA);
            _db.Hotels.Add(hotelB);

            _db.Bookings.Add(testBooking1);
            _db.Bookings.Add(testBooking2);


            await _db.SaveChangesAsync();
            return Ok("Test data added");
        }

        [HttpPost("reset")]
        public async Task<IActionResult> Reset()
        {
            _db.Bookings.RemoveRange(_db.Bookings);
            _db.Rooms.RemoveRange(_db.Rooms);
            _db.Hotels.RemoveRange(_db.Hotels);

            await _db.SaveChangesAsync();
            return Ok("Test data reset");
        }

        [HttpGet("getHotels")]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            var hotels = await _db.Hotels
                .Select(h => new HotelDto
                {
                    Name = h.Name,
                    Address = h.Address,
                }).ToListAsync();

            return Ok(hotels);
        }

        [HttpGet("getRooms")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRooms()
        {
            var rooms = await _db.Rooms
                .Select(r => new RoomDto
                {
                    HotelName = r.Hotel.Name,
                    RoomNumber = r.RoomNumber,
                    Type = r.Type.ToString()
                })
                .ToListAsync();

            return Ok(rooms);
        }

        [HttpGet("getBookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings()
        {
            var bookings = await _db.Bookings
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
            .ToListAsync();

            return Ok(bookings);
        }
    }
}
