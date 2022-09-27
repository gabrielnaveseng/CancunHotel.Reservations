using CancunHotel.Reservations.Core.Ports.In.Commands.CancelAReservation;
using CancunHotel.Reservations.Core.Ports.In.Commands.MakeAReservation;
using CancunHotel.Reservations.Core.Ports.In.Commands.ModifyAReservation;
using CancunHotel.Reservations.Core.Ports.In.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CancunHotel.Reservations.Api.Controllers
{
    [Route("api/v1/reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IGetSingleReservationByIdUseCase _getSingleReservationByIdUseCase;
        private readonly IMakeAReservationUseCase _makeAReservationUseCase;
        private readonly IModifyAReservationUseCase _modifyAReservationUseCase;
        private readonly ICancelAReservationUseCase _cancelAReservationUseCase;

        public ReservationsController(IGetSingleReservationByIdUseCase getSingleReservationByIdUseCase,
                                      IMakeAReservationUseCase makeAReservationUseCase,
                                      IModifyAReservationUseCase modifyAReservationUseCase,
                                      ICancelAReservationUseCase cancelAReservationUseCase)
        {
            _getSingleReservationByIdUseCase = getSingleReservationByIdUseCase;
            _makeAReservationUseCase = makeAReservationUseCase;
            _modifyAReservationUseCase = modifyAReservationUseCase;
            _cancelAReservationUseCase = cancelAReservationUseCase;
        }

        [HttpGet]
        [Route("{reservationId}")]
        public async Task<ActionResult<IEnumerable<ReservationDtoResponse>>> GetReservation(Guid reservationId)
        {
            return Ok(await _getSingleReservationByIdUseCase.Execute(reservationId));
        }

        [HttpPost]
        public async Task<ActionResult> MakeAReservation(MakeAReservationCommand command)
        {
            var id = await _makeAReservationUseCase.Execute(command);
            return Created($"api/v1/reservations/{id}", command);
        }

        [HttpPut]
        [Route("{reservationId}")]
        public async Task<ActionResult> ModifyAReservation(Guid reservationId, ModifyAReservationCommand command)
        {
            await _modifyAReservationUseCase.Execute(command, reservationId);
            return Ok(command);
        }

        [HttpDelete]
        [Route("{reservationId}")]
        public async Task<ActionResult> CancelAReservation(Guid reservationId)
        {
            await _cancelAReservationUseCase.Execute(reservationId);
            return Ok(reservationId);
        }
    }
}
