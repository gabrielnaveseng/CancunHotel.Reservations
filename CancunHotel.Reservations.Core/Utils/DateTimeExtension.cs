namespace CancunHotel.Reservations.Core.Utils
{
    public static class DateTimeExtension
    {
        public static HashSet<DateTime> DaysUntill(this DateTime startPeriod, DateTime endPeriod)
        {
            return Enumerable.Range(0, (int)(endPeriod - startPeriod).TotalDays)
                .Select(days => startPeriod.AddDays(days))
                .ToHashSet();
        }
    }
}
