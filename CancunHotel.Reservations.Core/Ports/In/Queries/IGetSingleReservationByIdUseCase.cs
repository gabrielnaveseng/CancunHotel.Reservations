namespace CancunHotel.Reservations.Core.Ports.In.Queries
{
    public interface IGetSingleReservationByIdUseCase
    {
        Task<ReservationDtoResponse> Execute(Guid reservationId);
    }
}
