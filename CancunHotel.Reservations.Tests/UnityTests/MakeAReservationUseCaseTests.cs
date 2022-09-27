using CancunHotel.Reservations.Core.Application.UseCases;
using CancunHotel.Reservations.Core.Domain.DomainServices;
using CancunHotel.Reservations.Core.Domain.Entities;
using CancunHotel.Reservations.Core.Ports.In.Commands.MakeAReservation;
using CancunHotel.Reservations.Core.Ports.Out;
using CancunHotel.Reservations.Core.Utils;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Reservations.Tests.UnityTests
{
    public class MakeAReservationUseCaseTests
    {
        private readonly Mock<IValidator<MakeAReservationCommand>> _validator;
        private readonly Mock<IReservationsRepository> _reservationsRepository;
        private readonly Mock<IRoomService> _roomService;
        private readonly MakeAReservationUseCase _makeAReservationUseCase;

        public MakeAReservationUseCaseTests()
        {
            _validator = new();
            _reservationsRepository = new();
            _roomService = new();
            _makeAReservationUseCase = new(_validator.Object, _reservationsRepository.Object, _roomService.Object);
        }

        [Fact]
        public async Task Execute_WhenThePeriodIsValid_CreateReservation()
        {
            var availableDays = new HashSet<DateTime>() { DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(3) };
            _roomService.Setup(service => service.GetAllRoomAvailableDays()).ReturnsAsync(availableDays);

            var key = Guid.NewGuid();
            var command = new MakeAReservationCommand(DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(3), Guid.NewGuid());

            _reservationsRepository
                .Setup(repo => repo.CreateAsync(command.StartReservationPeriod, command.EndReservationPeriod, command.ClientId, command.CreatedAt))
                .ReturnsAsync(key);

            var result = await _makeAReservationUseCase.Execute(command);

            Assert.Equal(result, key);

            _reservationsRepository.Verify(repo => repo.CreateAsync(command.StartReservationPeriod, command.EndReservationPeriod, command.ClientId, command.CreatedAt),
                Times.Once);

            _roomService.Verify(service => service.GetAllRoomAvailableDays(), Times.Once);
        }


        [Fact]
        public async Task Execute_WhenThePeriodIsNotValid_CreateReservation()
        {
            var availableDays = new HashSet<DateTime>() { DateTime.Now.Date.AddDays(1) };
            _roomService.Setup(service => service.GetAllRoomAvailableDays()).ReturnsAsync(availableDays);

            var key = Guid.NewGuid();
            var command = new MakeAReservationCommand(DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(3), Guid.NewGuid());

            _reservationsRepository
                .Setup(repo => repo.CreateAsync(command.StartReservationPeriod, command.EndReservationPeriod, command.ClientId, command.CreatedAt))
                .ReturnsAsync(key);

            await Assert.ThrowsAsync<BadRequestException>(async () => await _makeAReservationUseCase.Execute(command));

            _reservationsRepository.Verify(repo => repo.CreateAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(),
                It.IsAny<Guid>(), It.IsAny<DateTime>()), Times.Never);

            _roomService.Verify(service => service.GetAllRoomAvailableDays(), Times.Once);
        }
    }
}
