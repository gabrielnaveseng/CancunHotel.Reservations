using CancunHotel.Reservations.Core.Domain.DomainServices;
using CancunHotel.Reservations.Core.Ports.In.Queries;

namespace CancunHotel.Reservations.Core.Application.UseCases
{
    public class GetRoomBookingAvaliabilityUseCase : IGetRoomBookingAvaliabilityUseCase
    {
        private readonly IRoomService _roomService;

        public GetRoomBookingAvaliabilityUseCase(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<IEnumerable<DateTime>> Execute()
        {
            return await _roomService.GetAllRoomAvailableDays();
        }
    }
}
