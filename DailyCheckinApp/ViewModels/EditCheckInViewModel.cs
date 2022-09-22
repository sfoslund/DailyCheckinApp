using DailyCheckinApp.Models;
using DailyCheckinApp.Storage;
using PropertyChanged;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DailyCheckinApp.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    internal class EditCheckInViewModel : INotifyPropertyChanged
    {
        #region Properties
        public CheckInDay CheckInDay { get; set; }

        public ColorOption[] Colors { get; }

        public ColorOption SelectedColor { get; set; }

        private readonly ICheckInDayStore Store;

        private Func<Task> ReturnNavigationCallback;

        public event PropertyChangedEventHandler PropertyChanged;
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
                SelectedColor = this.GetSelectedColor(storeResult.Color);
                return storeResult;
            }
        }
        #endregion

        #region methods
        internal void UpdateNotes(string notes)
        {
            CheckInDay.Notes = notes;
        }

        internal void UpdateHabit(string habit, bool value)
        {
            this.CheckInDay.Habits.Where(h => h.Name.Equals(habit)).First().Done = value;
        }

        internal void SaveCheckIn()
        {
            this.CheckInDay.Color = SelectedColor?.Color ?? Color.Parse("transparent");
            this.Store.Write(this.CheckInDay.Date, this.CheckInDay);
            this.ReturnNavigationCallback();
        }

        private ColorOption GetSelectedColor(Color color)
        {
            return this.Colors.Where(c => c.Color.Equals(color)).FirstOrDefault();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }
}
