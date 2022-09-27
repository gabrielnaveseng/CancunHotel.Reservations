namespace CancunHotel.Reservations.Core.Ports.In.Queries
{
    public interface IGetAllClientReservationsUseCase
    {
        Task<IEnumerable<ReservationDtoResponse>> Execute(Guid clientId);
    }
}
