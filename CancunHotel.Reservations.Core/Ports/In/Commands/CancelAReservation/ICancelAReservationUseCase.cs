namespace CancunHotel.Reservations.Core.Ports.In.Commands.CancelAReservation
{
    public interface ICancelAReservationUseCase
    {
        Task Execute(Guid id);
    }
}
