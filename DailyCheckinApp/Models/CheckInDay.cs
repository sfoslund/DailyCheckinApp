namespace DailyCheckinApp.Models
{
    public class CheckInDay
    {
        public DateTime Date { get; }

        public string Notes { get; set; }

        public Color Color { get; set; }

        public IEnumerable<Habit> Habits { get; set; }

        public CheckInDay(DateTime date, IEnumerable<Habit> habits, Color color = null)
        {
            Date = date.Date;
            Habits = habits;
            Color = color ?? Color.Parse("transparent");
        }
    }
}
