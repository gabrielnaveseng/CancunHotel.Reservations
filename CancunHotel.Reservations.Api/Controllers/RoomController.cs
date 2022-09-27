using CancunHotel.Reservations.Core.Ports.In.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CancunHotel.Reservations.Api.Controllers
{
    [Route("api/room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IGetRoomBookingAvaliabilityUseCase _getRoomBookingAvaliabilityUseCase;

        public RoomController(IGetRoomBookingAvaliabilityUseCase getRoomBookingAvaliabilityUseCase)
        {
            _getRoomBookingAvaliabilityUseCase = getRoomBookingAvaliabilityUseCase;
        }

        [HttpGet]
        [Route("avaliability")]
        public async Task<ActionResult<IEnumerable<DateTime>>> GetRoomAvailableDays()
        {
            return Ok(await _getRoomBookingAvaliabilityUseCase.Execute());
        }
    }
}
