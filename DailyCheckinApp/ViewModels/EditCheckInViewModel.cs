using DailyCheckinApp.Storage;

namespace DailyCheckinApp.ViewModels
{
    internal class EditCheckInViewModel
    {
        #region Properties
        public CheckInDay CheckInDay { get; set; }

        public ColorOption[] Colors { get; }

        public ColorOption SelectedColor { get; set; }

        private readonly ICheckInDayStore Store;

        private Func<Task> ReturnNavigationCallback;
        #endregion

        #region Constructors
        public EditCheckInViewModel(DateTime targetDate, ICheckInDayStore store, Func<Task> returnNavigationCallback)
        {
            this.Store = store;
            this.ReturnNavigationCallback = returnNavigationCallback;
            this.Colors = new ColorOption[] {
                new ColorOption("Purple", Color.FromArgb("#d9d2e9")),
                new ColorOption("Blue", Color.FromArgb("#9fc5e8")),
                new ColorOption("Green", Color.FromArgb("#b1cba6")),
                new ColorOption("Yellow", Color.FromArgb("#ffe599")),
                new ColorOption("Orange", Color.FromArgb("#f9cb9c")),
                new ColorOption("Red", Color.FromArgb("#ea9999")),
            };
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
            this.CheckInDay.Color = SelectedColor?.Color ?? Color.Parse("transparent");
            this.Store.Write(this.CheckInDay.Date, this.CheckInDay);
            this.ReturnNavigationCallback();
        }
        #endregion
    }
}
