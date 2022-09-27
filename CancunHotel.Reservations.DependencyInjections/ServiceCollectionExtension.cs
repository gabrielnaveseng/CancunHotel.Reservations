using CancunHotel.Reservations.Core.Application.UseCases;
using CancunHotel.Reservations.Core.Application.Validators;
using CancunHotel.Reservations.Core.Domain.DomainServices;
using CancunHotel.Reservations.Core.Ports.In.Commands.CancelAReservation;
using CancunHotel.Reservations.Core.Ports.In.Commands.MakeAReservation;
using CancunHotel.Reservations.Core.Ports.In.Commands.ModifyAReservation;
using CancunHotel.Reservations.Core.Ports.In.Queries;
using CancunHotel.Reservations.Core.Ports.Out;
using CancunHotel.Reservations.DapperSqlAdapter;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CancunHotel.Reservations.DependencyInjections
{
    public static class ServiceCollectionExtension
    {
        public static void InjectAllServices(this IServiceCollection services)
        {
            services.AddSingleton<IReservationsRepository, ReservationsRepositoryAdapter>();
            services.AddSingleton<IValidator<ModifyAReservationCommand>, ModifyAReservationCommandValidator>();
            services.AddSingleton<IValidator<MakeAReservationCommand>, MakeAReservationCommandValidator>();
            services.AddSingleton<IRoomService, RoomService>();
            services.AddSingleton<IGetAllClientReservationsUseCase, GetAllClientReservationsUseCase>();
            services.AddSingleton<IGetAllClientReservationsUseCase, GetAllClientReservationsUseCase>();
            services.AddSingleton<IGetRoomBookingAvaliabilityUseCase, GetRoomBookingAvaliabilityUseCase>();
            services.AddSingleton<IGetSingleReservationByIdUseCase, GetSingleReservationByIdUseCase>();
            services.AddSingleton<IMakeAReservationUseCase, MakeAReservationUseCase>();
            services.AddSingleton<IModifyAReservationUseCase, ModifyAReservationUseCase>();
            services.AddSingleton<ICancelAReservationUseCase, CancelAReservationUseCase>();
        }
    }
}
