namespace DailyCheckinApp.ViewModels
{
    internal class EditCheckInViewModel
    {
        #region Properties
        public CheckInDay CheckInDay { get; set; }
        #endregion

        #region Constructors
        public EditCheckInViewModel(DateTime targetDate)
        {
            this.CheckInDay = new CheckInDay(targetDate);
        }
        #endregion

        internal void UpdateNotes(string notes)
        {
            CheckInDay.Notes = notes;
        }
    }
}
