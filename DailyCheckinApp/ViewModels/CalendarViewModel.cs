using DailyCheckinApp.Models;
using DailyCheckinApp.Storage;
using PropertyChanged;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using XCalendar.Core.Models;

namespace DailyCheckinApp.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    internal class CalendarViewModel: INotifyPropertyChanged
    {
        #region Properties
        public Calendar<UpdatableCalendarDay> Calendar { get; set; } = new Calendar<UpdatableCalendarDay>()
        {
            SelectionType = XCalendar.Core.Enums.SelectionType.Single,
            SelectionAction = XCalendar.Core.Enums.SelectionAction.Replace,
            SelectedDates = new ObservableRangeCollection<DateTime>() { DateTime.Today }
        };

        private Func<DateTime, Task> OpenNewEditView;
        private IStore Store;

        private static readonly Color SelectedColor = Color.FromArgb("#757575");
        private static readonly Color NoDataColor = Color.Parse("transparent");

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands
        public ICommand NavigateCalendarCommand { get; set; }
        public ICommand ChangeDateSelectionCommand { get; set; }
        #endregion

        #region Constructors
        public CalendarViewModel(Func<DateTime, Task> openNewEditView, IStore store)
        {
            this.OpenNewEditView = openNewEditView;
            this.Store = store;
            NavigateCalendarCommand = new Command<int>(NavigateCalendar);
            ChangeDateSelectionCommand = new Command<DateTime>(ChangeDateSelection);
            this.Calendar.DaysUpdated += this.SetDailyBackgroundColor;

            this.SetDailyBackgroundColor();
        }

        private void SetDailyBackgroundColor(object sender = null, EventArgs e = null)
        {
            var storeData = this.Store.ReadMonthCheckIns(Calendar.NavigatedDate);
            var habits = this.Store.ReadHabits();
            foreach (var d in this.Calendar.Days)
            {
                if (storeData.ContainsKey(d.DateTime.Day))
                {
                    d.CheckInData = storeData[d.DateTime.Day];
                }
                else
                {
                    d.CheckInData = new CheckInDay(d.DateTime, habits.Select(h => new Habit(h, false)), d.IsSelected ? SelectedColor : NoDataColor);
                }
            }
        }
        #endregion

        #region Methods
        public void NavigateCalendar(int Amount)
        {
            Calendar?.NavigateCalendar(Amount);
        }

        public void ChangeDateSelection(DateTime date)
        {
            Calendar?.ChangeDateSelection(date);
            this.OpenNewEditView(date);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }
}
