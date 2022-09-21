using DailyCheckinApp.ViewModels;

namespace DailyCheckinApp.Storage
{
    public interface ICheckInDayStore
    {
        public void Write(DateTime dateKey, CheckInDay value);

        public CheckInDay Read(DateTime dateKey);

        public int[] GetDaysWithData(DateTime month);
    }
}
