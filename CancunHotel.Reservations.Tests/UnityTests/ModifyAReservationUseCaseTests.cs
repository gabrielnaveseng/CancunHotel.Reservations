using CancunHotel.Reservations.Core.Application.UseCases;
using CancunHotel.Reservations.Core.Domain.DomainServices;
using CancunHotel.Reservations.Core.Domain.Entities;
using CancunHotel.Reservations.Core.Ports.In.Commands.ModifyAReservation;
using CancunHotel.Reservations.Core.Ports.Out;
using CancunHotel.Reservations.Core.Utils;
using FluentValidation;
using Moq;

namespace CancunHotel.Reservations.Tests.UnityTests
{
    public class ModifyAReservationUseCaseTests
    {
        private readonly Mock<IValidator<ModifyAReservationCommand>> _validator;
        private readonly Mock<IReservationsRepository> _reservationsRepository;
        private readonly Mock<IRoomService> _roomService;
        private readonly ModifyAReservationUseCase _modifyAReservationUseCase;

        public ModifyAReservationUseCaseTests()
        {
            _validator = new();
            _reservationsRepository = new();
            _roomService = new();
            _modifyAReservationUseCase = new(_validator.Object, _reservationsRepository.Object, _roomService.Object);
        }

        [Fact]
        public async Task Execute_WhenThePeriodIsValid_CreateReservation()
        {
            var availableDays = new HashSet<DateTime>() { DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(3) };
            _roomService.Setup(service => service.GetAllRoomAvailableDays()).ReturnsAsync(availableDays);

            var id = Guid.NewGuid();
            var reservation = new Reservation(1, id, DateTime.Now, DateTime.Now, DateTime.Now.Date.AddDays(3), DateTime.Now.Date.AddDays(4), Guid.NewGuid(), false);
            _reservationsRepository.Setup(repo => repo.GetReservation(It.Is<Guid>(x => x.Equals(id)))).ReturnsAsync(reservation);

            var command = new ModifyAReservationCommand(DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(3));
            await _modifyAReservationUseCase.Execute(command, id);

            _reservationsRepository
                 .Verify(repo => repo.ModifyAsync(It.IsAny<DateTime>(), command.StartReservationPeriod, command.EndReservationPeriod, id), Times.Once);

            _roomService.Verify(service => service.GetAllRoomAvailableDays(), Times.Once);
        }

        [Fact]
        public async Task Execute_WhenThereIsNoReservation_NotFound()
        {
            var id = Guid.NewGuid();
            Reservation? reservation = null;
            _reservationsRepository.Setup(repo => repo.GetReservation(It.Is<Guid>(x => x.Equals(id)))).ReturnsAsync(reservation);

            var command = new ModifyAReservationCommand(DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(3));

            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _modifyAReservationUseCase.Execute(command, id));

            _reservationsRepository
                 .Verify(repo => repo.ModifyAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Guid>()),
                 Times.Never);

            _roomService.Verify(service => service.GetAllRoomAvailableDays(), Times.Never);
        }

        [Fact]
        public async Task Execute_WhenThePeriodIsNotValid_CreateReservation()
        {
            var availableDays = new HashSet<DateTime>() { DateTime.Now.Date.AddDays(12) };
            _roomService.Setup(service => service.GetAllRoomAvailableDays()).ReturnsAsync(availableDays);

            var id = Guid.NewGuid();
            var reservation = new Reservation(1, id, DateTime.Now, DateTime.Now, DateTime.Now.Date.AddDays(3), DateTime.Now.Date.AddDays(4), Guid.NewGuid(), false);
            _reservationsRepository.Setup(repo => repo.GetReservation(It.Is<Guid>(x => x.Equals(id)))).ReturnsAsync(reservation);

            var command = new ModifyAReservationCommand(DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(3));

            await Assert.ThrowsAsync<BadRequestException>(async () => await _modifyAReservationUseCase.Execute(command, id));

            _reservationsRepository
                 .Verify(repo => repo.ModifyAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Guid>()),
                 Times.Never);

            _roomService.Verify(service => service.GetAllRoomAvailableDays(), Times.Once);
        }
    }
}
