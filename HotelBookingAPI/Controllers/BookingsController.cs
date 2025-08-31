using Microsoft.AspNetCore.Mvc;

using HotelBookingAPI.Models.DTOs;
using HotelBookingAPI.Services;

namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BookingsController : Controller
    {
        private readonly BookingService _bookingService;
        private readonly HotelService _hotelService;

        public BookingsController(BookingService service, HotelService hotelService)
        {
            _bookingService = service;
            _hotelService = hotelService;
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> CreateBooking(CreateBookingDto dto)
        {
            if(dto.NumberOfGuests < 1)
            {
                return BadRequest("Number of guests must be at least one");
            }

            var hotel = await _hotelService.GetHotelObjectAsync(dto.HotelName);

            if(hotel == null)
            {
                return NotFound("Hotel " + dto.HotelName + " not found.");
            }

            var (success, errorMessage, booking) = await _bookingService.CreateBookingAsync(dto, hotel);

            if (!success)
            {
                return BadRequest(errorMessage);
            }

            return CreatedAtAction(nameof(GetBooking), new { bookingReference = booking.BookingReference }, booking);
        }

        [HttpGet("{bookingReference}")]
        public async Task<ActionResult<BookingDto>> GetBooking(string bookingReference)
        {
            var booking = await _bookingService.GetBookingByReferenceAsync(bookingReference);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }
    }
}
