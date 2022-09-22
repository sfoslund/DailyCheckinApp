namespace DailyCheckinApp.Models
{
    public class CheckInDay
    {
        public DateTime Date { get; }

        public string Notes { get; set; }

        public Color Color { get; set; }

        public Habit[] Habits { get; set; }

        public CheckInDay(DateTime date, Color color = null)
        {
            Date = date.Date;
            Color = color ?? Color.Parse("transparent");
            // TODO
            var habit1 = new Habit();
            habit1.Name = "Work out";
            var habit2 = new Habit();
            habit2.Name = "Read";
            Habits = new Habit[] {
                habit1,
                habit2
            };
        }
    }
}
