using Microsoft.AspNetCore.Mvc;

using HotelBookingAPI.Models;
using HotelBookingAPI.Services;

namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly RoomService _roomService;
        private readonly HotelService _hotelService;

        public RoomsController(RoomService service, HotelService hotelService)
        {
            _roomService = service;
            _hotelService = hotelService;
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<Room>>> GetAvailableRooms(string hotelName, DateOnly checkIn, DateOnly checkOut, int numberOfGuests)
        {
            if(numberOfGuests < 1)
            {
                return BadRequest("Number of guests must be at least one");
            }

            var hotel = await _hotelService.GetHotelObjectAsync(hotelName);

            if (hotel == null)
            {
                return NotFound("Hotel " + hotelName + " not found ");
            }

            var availableRooms = _roomService.GetAvailableRooms(hotel, checkIn, checkOut, numberOfGuests);

            return Ok(availableRooms);
        }
    }
}