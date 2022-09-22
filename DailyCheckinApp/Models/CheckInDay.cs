namespace DailyCheckinApp.Models
{
    public class CheckInDay
    {
        public DateTime Date { get; }

        public string Notes { get; set; }

        public Color Color { get; set; }

        public Dictionary<string, bool> Habits { get; set; }

        public CheckInDay(DateTime date, Color color = null)
        {
            Date = date.Date;
            Color = color ?? Color.Parse("transparent");
            // TODO
            Habits = new Dictionary<string, bool>() {
                {"Work out", false },
                {"Read", false}
            };
        }
    }
}
