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
        #endregion

        #region Constructors
        public EditCheckInViewModel(DateTime targetDate, ICheckInDayStore store)
        {
            this.Store = store;
            this.Colors = new ColorOption[] {
                new ColorOption("Purple", Color.Parse("purple")),
                new ColorOption("Blue", Color.Parse("blue")),
                new ColorOption("Green", Color.Parse("green")),
                new ColorOption("Yellow", Color.Parse("yellow")),
                new ColorOption("Orange", Color.Parse("orange")),
                new ColorOption("Red", Color.Parse("red")),
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
        }
        #endregion
    }
}
