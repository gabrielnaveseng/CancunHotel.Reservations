using CancunHotel.Reservations.Core.Domain.DomainServices;
using CancunHotel.Reservations.Core.Ports.In.Commands.MakeAReservation;
using CancunHotel.Reservations.Core.Ports.Out;
using CancunHotel.Reservations.Core.Utils;
using FluentValidation;

namespace CancunHotel.Reservations.Core.Application.UseCases
{
    public class MakeAReservationUseCase : IMakeAReservationUseCase
    {
        private readonly IValidator<MakeAReservationCommand> _validator;
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IRoomService _roomService;

        public MakeAReservationUseCase(IValidator<MakeAReservationCommand> validator,
                                       IReservationsRepository reservationsRepository,
                                       IRoomService roomService)
        {
            _validator = validator;
            _reservationsRepository = reservationsRepository;
            _roomService = roomService;
        }

        public async Task<Guid> Execute(MakeAReservationCommand command)
        {
            _validator.ValidateAndThrow(command);

            if (!await IsResevationPeriodAvailable(command.StartReservationPeriod, command.EndReservationPeriod))
                throw new BadRequestException($"The requested period of time is not available.");

            return await _reservationsRepository.CreateAsync(command.StartReservationPeriod,
                                                             command.EndReservationPeriod,
                                                             command.ClientId,
                                                             command.CreatedAt);
        }

        private async Task<bool> IsResevationPeriodAvailable(DateTime from, DateTime at)
        {
            var allAvailableDays = await _roomService.GetAllRoomAvailableDays();

            return from.DaysUntill(at)
                .All(day => allAvailableDays.Contains(day));
        }
    }
}
