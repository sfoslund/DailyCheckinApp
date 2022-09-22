using XCalendar.Core.Models;

namespace DailyCheckinApp.Models
{
    public class UpdatableCalendarDay : CalendarDay
    {
        public CheckInDay CheckInData { get; set; }
    }
}
