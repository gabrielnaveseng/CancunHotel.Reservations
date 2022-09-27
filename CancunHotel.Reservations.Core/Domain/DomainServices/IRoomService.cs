namespace CancunHotel.Reservations.Core.Domain.DomainServices
{
    public interface IRoomService
    {
        Task<HashSet<DateTime>> GetAllRoomAvailableDays();
    }
}
