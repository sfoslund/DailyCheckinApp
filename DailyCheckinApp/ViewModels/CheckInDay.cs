namespace DailyCheckinApp.ViewModels
{
    internal class CheckInDay
    {
        public CheckInDay(DateTime date)
        {
            this.Date = date;
        }

        public DateTime Date { get; }

        public string Notes { get; set; }
    }
}
