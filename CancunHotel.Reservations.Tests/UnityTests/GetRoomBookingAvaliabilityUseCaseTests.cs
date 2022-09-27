using CancunHotel.Reservations.Core.Application.UseCases;
using CancunHotel.Reservations.Core.Domain.DomainServices;
using Moq;

namespace CancunHotel.Reservations.Tests.UnityTests
{
    internal class GetRoomBookingAvaliabilityUseCaseTests
    {
        private readonly GetRoomBookingAvaliabilityUseCase _getRoomBookingAvaliabilityUseCase;
        private readonly Mock<IRoomService> _roomService;

        public GetRoomBookingAvaliabilityUseCaseTests()
        {
            _roomService = new();
            _getRoomBookingAvaliabilityUseCase = new(_roomService.Object);
        }

        public async Task Execute_WhenThereIsReservationWith2ReservatedDays_28DaysAvailable()
        {
            var reservations = new HashSet<DateTime>() { DateTime.Now };

            _roomService.Setup(service => service.GetAllRoomAvailableDays()).ReturnsAsync(reservations);
            var result = await _getRoomBookingAvaliabilityUseCase.Execute();

            Assert.Equal(reservations, result);
        }
    }
}
