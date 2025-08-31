using Microsoft.AspNetCore.Mvc;

using HotelBookingAPI.Models.DTOs;
using HotelBookingAPI.Services;

namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class HotelsController : Controller
    {
        private readonly HotelService _hotelService;

        public HotelsController(HotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<HotelDto>> GetHotel(string name)
        {
            var hotelDto = await _hotelService.GetHotelDtoAsync(name);
            if(hotelDto == null)
            {
                return NotFound("Hotel " + name + " not found ");
            }

            return Ok(hotelDto);
        }
    }
}
