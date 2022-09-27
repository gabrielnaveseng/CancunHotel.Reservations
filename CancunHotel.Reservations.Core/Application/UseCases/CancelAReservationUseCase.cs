using CancunHotel.Reservations.Core.Ports.In.Commands.CancelAReservation;
using CancunHotel.Reservations.Core.Ports.Out;

namespace CancunHotel.Reservations.Core.Application.UseCases
{
    public class CancelAReservationUseCase : ICancelAReservationUseCase
    {
        private readonly IReservationsRepository _reservationsRepository;

        public CancelAReservationUseCase(IReservationsRepository reservationsRepository)
        {
            _reservationsRepository = reservationsRepository;
        }

        public async Task Execute(Guid id)
        {
            await _reservationsRepository.DeleteAsync(id);
        }
    }
}
