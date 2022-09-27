using CancunHotel.Reservations.Core.Application.UseCases;
using CancunHotel.Reservations.Core.Ports.Out;
using Moq;

namespace CancunHotel.Reservations.Tests.UnityTests
{
    public class CancelAReservationUseCaseTests
    {
        private readonly CancelAReservationUseCase _cancelAReservationUseCase;
        private readonly Mock<IReservationsRepository> _reservationsRepository;

        public CancelAReservationUseCaseTests()
        {
            _reservationsRepository = new();
            _cancelAReservationUseCase = new(_reservationsRepository.Object);
        }

        [Fact]
        public async Task Execute_WhenThereIsReservationWith2ReservatedDays_28DaysAvailable()
        {
            var key = Guid.NewGuid();
            await _cancelAReservationUseCase.Execute(key);
            _reservationsRepository.Verify(repo => repo.DeleteAsync(It.Is<Guid>(x => x.Equals(key))), Times.Once);
        }
    }
}
