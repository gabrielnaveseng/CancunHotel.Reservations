using CancunHotel.Reservations.Core.Ports.In.Queries;
using CancunHotel.Reservations.Core.Ports.Out;

namespace CancunHotel.Reservations.Core.Application.UseCases
{
    public class GetAllClientReservationsUseCase : IGetAllClientReservationsUseCase
    {
        private readonly IReservationsRepository _reservationsRepository;

        public GetAllClientReservationsUseCase(IReservationsRepository reservationsRepository)
        {
            _reservationsRepository = reservationsRepository;
        }

        public async Task<IEnumerable<ReservationDtoResponse>> Execute(Guid clientId)
        {
            var reservations = await _reservationsRepository.GetAllReservations(clientId);
            return reservations.Select(reservation => new ReservationDtoResponse(reservation));
        }
    }
}
