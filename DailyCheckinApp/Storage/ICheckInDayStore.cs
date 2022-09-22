using DailyCheckinApp.Models;

namespace DailyCheckinApp.Storage
{
    public interface ICheckInDayStore
    {
        public void Write(DateTime dateKey, CheckInDay value);

        public CheckInDay Read(DateTime dateKey);

        public Dictionary<int, CheckInDay> ReadMonth(DateTime dateKey);
    }
}
