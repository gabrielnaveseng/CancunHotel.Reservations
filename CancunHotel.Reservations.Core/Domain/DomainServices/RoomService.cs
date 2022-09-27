using CancunHotel.Reservations.Core.Ports.Out;

namespace CancunHotel.Reservations.Core.Domain.DomainServices
{
    public class RoomService : IRoomService
    {
        private const int MAX_RESERVATION_PERIOD_IN_DAYS = 30;
        private const int MIN_RESERVATION_PERIOD_IN_DAYS = 1;
        private readonly IReservationsRepository _reservationsRepository;

        public RoomService(IReservationsRepository reservationsRepository)
        {
            _reservationsRepository = reservationsRepository;
        }

        public async Task<HashSet<DateTime>> GetAllRoomAvailableDays()
        {
            var allDatesPossibilities = Enumerable.Range(MIN_RESERVATION_PERIOD_IN_DAYS, MAX_RESERVATION_PERIOD_IN_DAYS)
                .Select(day => DateTime.Now.Date.AddDays(day));

            var allReservatedDays = await GetAllReservatedDates();

            return allDatesPossibilities.Except(allReservatedDays).ToHashSet();
        }

        private async Task<IEnumerable<DateTime>> GetAllReservatedDates()
        {
            var allNextActiveReservations = await _reservationsRepository.GetAllReservations(DateTime.Now.Date,
                DateTime.Now.Date.AddDays(MAX_RESERVATION_PERIOD_IN_DAYS));

            var allReservatedDays = allNextActiveReservations.SelectMany(reservation => reservation.GetReservatedDates());
            return allReservatedDays;
        }
    }
}
