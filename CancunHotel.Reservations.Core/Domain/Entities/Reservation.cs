using CancunHotel.Reservations.Core.Utils;

namespace CancunHotel.Reservations.Core.Domain.Entities
{
    public class Reservation
    {
        public Reservation(DateTime startReservationPeriod,
                           DateTime endReservationPeriod,
                           Guid clientId, 
                           DateTime requestDate)
        {
            StartReservationPeriod = startReservationPeriod;
            EndReservationPeriod = endReservationPeriod;
            ClientId = clientId;
            CreatedAt = requestDate;
            UpdatedAt = CreatedAt;
            IdcDelete = false;
        }

        public Reservation(int sequentialId,
                           Guid id,
                           DateTime createdAt,
                           DateTime updatedAt,
                           DateTime startReservationPeriod,
                           DateTime endReservationPeriod,
                           Guid clientId,
                           bool idcDelete)
        {
            SequentialId = sequentialId;
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            StartReservationPeriod = startReservationPeriod;
            EndReservationPeriod = endReservationPeriod;
            ClientId = clientId;
            IdcDelete = idcDelete;
        }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime StartReservationPeriod { get; private set; }
        public DateTime EndReservationPeriod { get; private set; }
        public Guid ClientId { get; private set; }
        public bool IdcDelete { get; private set; }
        public int SequentialId { get; }
        public Guid Id { get; }

        internal HashSet<DateTime> GetReservatedDates()
        {
            return StartReservationPeriod.DaysUntill(EndReservationPeriod);
        }

        internal void ModifyReservation(DateTime startReservationPeriod, DateTime endReservationPeriod)
        {
            StartReservationPeriod = startReservationPeriod;
            EndReservationPeriod = endReservationPeriod;
            UpdatedAt = DateTime.Now;
        }

        internal void Delete()
        {
            IdcDelete = true;
        }

        internal bool ContainsPeriod(DateTime from, DateTime at)
        {
            return from.DaysUntill(at).All(day => GetReservatedDates().Contains(day));
        }
    }
}
