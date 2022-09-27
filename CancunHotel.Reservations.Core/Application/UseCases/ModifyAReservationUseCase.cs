using CancunHotel.Reservations.Core.Domain.DomainServices;
using CancunHotel.Reservations.Core.Domain.Entities;
using CancunHotel.Reservations.Core.Ports.In.Commands.ModifyAReservation;
using CancunHotel.Reservations.Core.Ports.Out;
using CancunHotel.Reservations.Core.Utils;
using FluentValidation;

namespace CancunHotel.Reservations.Core.Application.UseCases
{
    public class ModifyAReservationUseCase : IModifyAReservationUseCase
    {
        private readonly IValidator<ModifyAReservationCommand> _validator;
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IRoomService _roomService;

        public ModifyAReservationUseCase(IValidator<ModifyAReservationCommand> validator,
                                         IReservationsRepository reservationsRepository,
                                         IRoomService roomService)
        {
            _validator = validator;
            _reservationsRepository = reservationsRepository;
            _roomService = roomService;
        }

        public async Task Execute(ModifyAReservationCommand command, Guid reservationId)
        {
            _validator.ValidateAndThrow(command);

            var reservation = await _reservationsRepository.GetReservation(reservationId);

            if (reservation is null)
                throw new KeyNotFoundException("Reservation not found");

            if (!await IsResevationPeriodAvailable(command.StartReservationPeriod, command.EndReservationPeriod, reservation))
                throw new BadRequestException($"The requested period of time is not available.");

            reservation.ModifyReservation(command.StartReservationPeriod, command.EndReservationPeriod);

            await _reservationsRepository.ModifyAsync(reservation.UpdatedAt,
                                                      reservation.StartReservationPeriod,
                                                      reservation.EndReservationPeriod,
                                                      reservation.Id);

        }

        private async Task<bool> IsResevationPeriodAvailable(DateTime from, DateTime at, Reservation reservation)
        {
            var availableDays = await _roomService.GetAllRoomAvailableDays();
            var allAvailableDays = availableDays.Concat(reservation.GetReservatedDates()).ToHashSet();
            return from.DaysUntill(at).All(day => allAvailableDays.Contains(day));
        }

    }
}
