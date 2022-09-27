namespace CancunHotel.Reservations.Core.Ports.In.Commands.MakeAReservation
{
    public class MakeAReservationCommand
    {
        public MakeAReservationCommand(DateTime startReservationPeriod, DateTime endReservationPeriod, Guid clientId)
        {
            StartReservationPeriod = startReservationPeriod.Date;
            EndReservationPeriod = endReservationPeriod.Date;
            ClientId = clientId;
            CreatedAt = DateTime.Now;
        }
        public DateTime StartReservationPeriod { get; }
        public DateTime EndReservationPeriod { get; }
        public Guid ClientId { get; }
        public DateTime CreatedAt { get; }
    }
}
