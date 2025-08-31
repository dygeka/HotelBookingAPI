using HotelBookingAPI.Models;
using HotelBookingAPI.Services;

namespace HotelBookingAPI.Tests.Services
{
    public class RoomServiceTests
    {
        private readonly RoomService _roomService;

        public RoomServiceTests()
        {
            _roomService = new RoomService();
        }

        [Fact]
        public void ReturnAllRoomsWhenNoBookings()
        {
            var hotelA = new Hotel
            {
                Name = "HotelA",
                Address = "123 A Street",
                Rooms = new List<Room>
                {
                    new Room { RoomNumber = "1", Type = RoomType.Single },
                    new Room { RoomNumber = "2", Type = RoomType.Double },
                    new Room { RoomNumber = "3", Type = RoomType.Deluxe }
                }
            };

            foreach(var room in hotelA.Rooms)
            {
                room.Hotel = hotelA;
                room.HotelId = hotelA.Id;
            }

            var availableRooms = _roomService.GetAvailableRooms(hotelA, DateOnly.Parse("2025-08-31"), DateOnly.Parse("2025-09-06"), 1);

            Assert.Equal(3, availableRooms.Count);
        }

        [Fact]
        public void ReturnCorrectNumberOfRoomsWhichAreAvailable()
        {
            var hotelA = new Hotel
            {
                Name = "HotelA",
                Address = "123 A Street",
                Rooms = new List<Room>
                {
                    new Room { RoomNumber = "1", Type = RoomType.Single },
                    new Room { RoomNumber = "2", Type = RoomType.Double },
                    new Room { RoomNumber = "3", Type = RoomType.Deluxe }
                }
            };

            foreach (var room in hotelA.Rooms)
            {
                room.Hotel = hotelA;
                room.HotelId = hotelA.Id;
            }

            var bookedRoom = hotelA.Rooms.First();
            var booking = new Booking
            {
                RoomId = bookedRoom.Id,
                Room = bookedRoom,
                BookingReference = "B-12345678",
                GuestName = "Dougall",
                NumberOfGuests = 1,
                CheckIn = DateOnly.Parse("2025-08-31"),
                CheckOut = DateOnly.Parse("2025-09-06")
            };

            bookedRoom.Bookings.Add(booking);

            var availableRooms = _roomService.GetAvailableRooms(hotelA, DateOnly.Parse("2025-08-31"), DateOnly.Parse("2025-09-06"), 1);

            Assert.Equal(2, availableRooms.Count);
        }

        [Fact]
        public void ReturnNoRoomsWhenFullyBooked()
        {
            var hotelA = new Hotel
            {
                Name = "HotelA",
                Address = "123 A Street",
                Rooms = new List<Room>
                {
                    new Room { RoomNumber = "1", Type = RoomType.Single },
                    new Room { RoomNumber = "2", Type = RoomType.Double },
                    new Room { RoomNumber = "3", Type = RoomType.Deluxe }
                }
            };

            foreach (var room in hotelA.Rooms)
            {
                room.Hotel = hotelA;
                room.HotelId = hotelA.Id;
            }

            var checkIn = DateOnly.Parse("2025-08-31");
            var checkOut = DateOnly.Parse("2025-09-06");

            foreach(var room in hotelA.Rooms)
            {
                var booking = new Booking
                {
                    RoomId = room.Id,
                    Room = room,
                    BookingReference = "B-12345678",
                    GuestName = "Dougall",
                    NumberOfGuests = 1,
                    CheckIn = checkIn,
                    CheckOut = checkOut

                };

                room.Bookings.Add(booking);
            }

            var availableRooms = _roomService.GetAvailableRooms(hotelA, DateOnly.Parse("2025-08-31"), DateOnly.Parse("2025-09-06"), 1);

            Assert.Empty(availableRooms);
        }
    }
}