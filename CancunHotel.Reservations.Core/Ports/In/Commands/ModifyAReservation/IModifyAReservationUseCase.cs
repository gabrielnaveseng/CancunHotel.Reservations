namespace CancunHotel.Reservations.Core.Ports.In.Commands.ModifyAReservation
{
    public interface IModifyAReservationUseCase
    {
        Task Execute(ModifyAReservationCommand command, Guid reservationId);
    }
}
