namespace CancunHotel.Reservations.Core.Ports.In.Commands.MakeAReservation
{
    public interface IMakeAReservationUseCase
    {
        Task<Guid> Execute(MakeAReservationCommand command);
    }
}
