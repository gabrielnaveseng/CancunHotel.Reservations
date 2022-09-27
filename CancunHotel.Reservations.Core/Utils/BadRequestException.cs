namespace CancunHotel.Reservations.Core.Utils
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        { }
    }
}
