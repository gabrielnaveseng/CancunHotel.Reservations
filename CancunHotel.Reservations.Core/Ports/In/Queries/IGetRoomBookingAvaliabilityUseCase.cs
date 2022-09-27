namespace CancunHotel.Reservations.Core.Ports.In.Queries
{
    public interface IGetRoomBookingAvaliabilityUseCase
    {
        Task<IEnumerable<DateTime>> Execute();
    }
}
