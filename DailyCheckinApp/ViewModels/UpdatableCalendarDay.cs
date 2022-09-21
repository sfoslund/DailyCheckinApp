using XCalendar.Core.Models;

namespace DailyCheckinApp.ViewModels
{
    public class UpdatableCalendarDay : CalendarDay
    {
        public CheckInDay CheckInData { get; set; }
    }
}
