using DailyCheckinApp.Storage;
using System.Text.Json;

namespace DailyCheckinApp.ViewModels
{
    internal class EditCheckInViewModel
    {
        #region Properties
        public CheckInDay CheckInDay { get; set; }

        private readonly ICheckInDayStore Store;
        #endregion

        #region Constructors
        public EditCheckInViewModel(DateTime targetDate, ICheckInDayStore store)
        {
            this.Store = store;
            this.CheckInDay = this.LoadCheckInDay(targetDate);
        }

        public CheckInDay LoadCheckInDay(DateTime date)
        {
            var storeResult = this.Store.Read(date);
            if (storeResult == null)
            {
                return new CheckInDay(date);
            }
            else
            {
                return storeResult;
            }
        }
        #endregion

        #region methods
        internal void UpdateNotes(string notes)
        {
            CheckInDay.Notes = notes;
        }

        internal void SaveCheckIn()
        {
            this.Store.Write(this.CheckInDay.Date, this.CheckInDay);
        }
        #endregion
    }
}
