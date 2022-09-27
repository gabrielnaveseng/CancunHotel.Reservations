using CancunHotel.Reservations.Core.Domain.Entities;

namespace CancunHotel.Reservations.Core.Ports.Out
{
    public interface IReservationsRepository
    {
        Task<Guid> CreateAsync(DateTime startReservationPeriod, DateTime endReservationPeriod, Guid clientId, DateTime requestDate);
        Task<Reservation?> GetReservation(Guid reservationId);
        Task<IEnumerable<Reservation>> GetAllReservations(Guid clientId);
        Task ModifyAsync(DateTime updatedAt, DateTime startReservationPeriod, DateTime endReservationPeriod, Guid id);
        Task<IEnumerable<Reservation>> GetAllReservations(DateTime startPeriod, DateTime endPeriod);
        Task DeleteAsync(Guid id);
    }
}
