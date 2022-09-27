using CancunHotel.Reservations.Core.Application.UseCases;
using CancunHotel.Reservations.Core.Domain.Entities;
using CancunHotel.Reservations.Core.Ports.Out;
using Moq;

namespace CancunHotel.Reservations.Tests.UnityTests
{
    public class GetAllClientReservationsTests
    {
        private readonly GetAllClientReservationsUseCase _getAllClientReservationsUseCase;
        private readonly Mock<IReservationsRepository> _reservationsRepository;

        public GetAllClientReservationsTests()
        {
            _reservationsRepository = new();
            _getAllClientReservationsUseCase = new(_reservationsRepository.Object);
        }

        [Fact]
        public async Task Execute_WhenThereIsReservations_Ok()
        {
            var clientId = Guid.NewGuid();
            var reservations = new List<Reservation>() { new Reservation(DateTime.Now, DateTime.Now, clientId, DateTime.Now) };
            _reservationsRepository.Setup(repo => repo.GetAllReservations(It.Is<Guid>(x => x.Equals(clientId)))).ReturnsAsync(reservations);

            var result = await _getAllClientReservationsUseCase.Execute(clientId);

            Assert.Equal(reservations.First().ClientId, result.First().ClientId);
            Assert.Single(reservations);
            _reservationsRepository.Verify(repo => repo.GetAllReservations(It.Is<Guid>(x => x.Equals(clientId))), Times.Once);
        }
    }
}