using CancunHotel.Reservations.Core.Ports.In.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CancunHotel.Reservations.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IGetAllClientReservationsUseCase _getAllClientReservationsUseCase;

        public ClientsController(IGetAllClientReservationsUseCase getAllClientReservationsUseCase)
        {
            _getAllClientReservationsUseCase = getAllClientReservationsUseCase;
        }

        [HttpGet]
        [Route("{clientId}/reservations")]
        public async Task<ActionResult<IEnumerable<ReservationDtoResponse>>> GetAllClientReservations(Guid clientId)
        {
            return Ok(await _getAllClientReservationsUseCase.Execute(clientId));
        }

    }
}
