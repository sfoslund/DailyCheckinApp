namespace DailyCheckinApp.ViewModels
{
    public class CheckInDay
    {
        public DateTime Date { get; }

        public string Notes { get; set; }

        public CheckInDay(DateTime date)
        {
            this.Date = date.Date;
        }
    }
}
