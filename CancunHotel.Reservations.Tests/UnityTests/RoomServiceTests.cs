using CancunHotel.Reservations.Core.Domain.DomainServices;
using CancunHotel.Reservations.Core.Domain.Entities;
using CancunHotel.Reservations.Core.Ports.Out;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Reservations.Tests.UnityTests
{
    public class RoomServiceTests
    {
        private readonly RoomService _roomService;
        private readonly Mock<IReservationsRepository> _reservationsRepository;

        public RoomServiceTests()
        {
            _reservationsRepository = new();
            _roomService = new(_reservationsRepository.Object);
        }

        [Fact]
        public async Task Execute_WhenThereIsReservationWith2ReservatedDays_28DaysAvailable()
        {
            var clientId = Guid.NewGuid();
            var from = DateTime.Now.Date.AddDays(2);
            var at = from.AddDays(2);
            var reservations = new List<Reservation>() { new Reservation(from, at, clientId, DateTime.Now) };

            _reservationsRepository.Setup(repo => repo.GetAllReservations(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(reservations);

            var result = await _roomService.GetAllRoomAvailableDays();

            Assert.True(result.Count() == 28);
            Assert.DoesNotContain(result, x => x.Equals(from));
            Assert.DoesNotContain(result, x => x.Equals(from.AddDays(1)));
            _reservationsRepository.Verify(repo => repo.GetAllReservations(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }
    }
}
