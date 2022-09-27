using CancunHotel.Reservations.Core.Ports.In.Queries;
using CancunHotel.Reservations.Core.Ports.Out;

namespace CancunHotel.Reservations.Core.Application.UseCases
{
    public class GetSingleReservationByIdUseCase : IGetSingleReservationByIdUseCase
    {
        private readonly IReservationsRepository _reservationsRepository;

        public GetSingleReservationByIdUseCase(IReservationsRepository reservationsRepository)
        {
            _reservationsRepository = reservationsRepository;
        }

        public async Task<ReservationDtoResponse> Execute(Guid reservationId)
        {
            var reservation = await _reservationsRepository.GetReservation(reservationId);

            if (reservation is null)
                throw new KeyNotFoundException("Reservation not found");

            return new ReservationDtoResponse(reservation);
        }
    }
}
