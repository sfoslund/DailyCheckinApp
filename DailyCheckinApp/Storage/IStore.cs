using DailyCheckinApp.Models;

namespace DailyCheckinApp.Storage
{
    public interface IStore
    {
        public void WriteCheckIn(DateTime dateKey, CheckInDay value);

        public CheckInDay ReadCheckIn(DateTime dateKey);

        public Dictionary<int, CheckInDay> ReadMonthCheckIns(DateTime dateKey);

        public void WriteHabits(IEnumerable<string> habits);

        public IEnumerable<string> ReadHabits();
    }
}
