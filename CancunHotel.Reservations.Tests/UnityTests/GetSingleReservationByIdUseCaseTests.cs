using CancunHotel.Reservations.Core.Application.UseCases;
using CancunHotel.Reservations.Core.Domain.Entities;
using CancunHotel.Reservations.Core.Ports.Out;
using Moq;

namespace CancunHotel.Reservations.Tests.UnityTests
{
    public class GetSingleReservationByIdUseCaseTests
    {
        private readonly GetSingleReservationByIdUseCase _getSingleReservationByIdUseCase;
        private readonly Mock<IReservationsRepository> _reservationsRepository;

        public GetSingleReservationByIdUseCaseTests()
        {
            _reservationsRepository = new();
            _getSingleReservationByIdUseCase = new(_reservationsRepository.Object);
        }

        [Fact]
        public async Task Execute_WhenThereIsReservation_Ok()
        {
            var clientId = Guid.NewGuid();
            var reservation = new Reservation(DateTime.Now, DateTime.Now, clientId, DateTime.Now);
            _reservationsRepository.Setup(repo => repo.GetReservation(It.Is<Guid>(x => x.Equals(clientId)))).ReturnsAsync(reservation);

            var result = await _getSingleReservationByIdUseCase.Execute(clientId);

            Assert.Equal(reservation.ClientId, result.ClientId);
            _reservationsRepository.Verify(repo => repo.GetReservation(It.Is<Guid>(x => x.Equals(clientId))), Times.Once);
        }

        [Fact]
        public async Task Execute_WhenThereIsNoReservation_NotFound()
        {
            var clientId = Guid.NewGuid();
            Reservation? reservation = null;
            _reservationsRepository.Setup(repo => repo.GetReservation(It.Is<Guid>(x => x.Equals(clientId)))).ReturnsAsync(reservation);

            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _getSingleReservationByIdUseCase.Execute(clientId));
            _reservationsRepository.Verify(repo => repo.GetReservation(It.Is<Guid>(x => x.Equals(clientId))), Times.Once);
        }
    }
}
