

using XCalendar.Core.Models;

namespace DailyCheckinApp.ViewModels
{
    internal class CalendarViewModel
    {
        public Calendar<CalendarDay> Calendar { get; set; } = new Calendar<CalendarDay>();
    }
}
