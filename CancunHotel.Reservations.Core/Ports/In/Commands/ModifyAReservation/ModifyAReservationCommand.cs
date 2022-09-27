namespace CancunHotel.Reservations.Core.Ports.In.Commands.ModifyAReservation
{
    public class ModifyAReservationCommand
    {
        public ModifyAReservationCommand(DateTime startReservationPeriod, DateTime endReservationPeriod)
        {
            StartReservationPeriod = startReservationPeriod.Date;
            EndReservationPeriod = endReservationPeriod.Date;
            UpdatedAt = DateTime.Now;
        }

        public DateTime UpdatedAt { get; private set; }
        public DateTime StartReservationPeriod { get; private set; }
        public DateTime EndReservationPeriod { get; private set; }
    }
}
