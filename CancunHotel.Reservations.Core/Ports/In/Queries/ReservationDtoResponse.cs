using CancunHotel.Reservations.Core.Domain.Entities;

namespace CancunHotel.Reservations.Core.Ports.In.Queries
{
    public class ReservationDtoResponse
    {
        public ReservationDtoResponse(Reservation reservation)
        {
            CreatingRoomReservationDateTime = reservation.CreatedAt;
            LastModifiedReservationDateTime = reservation.UpdatedAt;
            StartReservationPeriod = reservation.StartReservationPeriod;
            EndReservationPeriod = reservation.EndReservationPeriod;
            ClientId = reservation.ClientId;
            Id = reservation.Id;
            SequentialId = reservation.SequentialId;
        }

        public DateTime CreatingRoomReservationDateTime { get; private set; }
        public DateTime LastModifiedReservationDateTime { get; private set; }
        public DateTime StartReservationPeriod { get; private set; }
        public DateTime EndReservationPeriod { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid Id { get; private set; }
        public int SequentialId { get; private set; }

    }
}
