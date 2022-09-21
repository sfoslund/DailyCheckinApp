namespace DailyCheckinApp.ViewModels
{
    public class CheckInDay
    {
        public DateTime Date { get; }

        public string Notes { get; set; }

        public Color Color { get; set; }

        public CheckInDay(DateTime date, Color color)
        {
            this.Date = date.Date;
            this.Color = color;
        }
    }
}
