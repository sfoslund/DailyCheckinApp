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
    internal class ViewHabitsViewModel: INotifyPropertyChanged
    {
        #region Properties
        public Calendar<HabitTrackingCalendarDay> Calendar { get; set; } = new Calendar<HabitTrackingCalendarDay>()
        {
            SelectionType = XCalendar.Core.Enums.SelectionType.Single,
            SelectionAction = XCalendar.Core.Enums.SelectionAction.Replace,
        };
        
        public string SelectedHabit { get; set; }

        public ObservableRangeCollection<string> Habits { get; set; }

        private IStore Store;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands
        public ICommand NavigateCalendarCommand { get; set; }
        #endregion

        #region Constructors
        public ViewHabitsViewModel(IStore store)
        {
            this.Store = store;
            this.Habits = new ObservableRangeCollection<string>(this.Store.ReadHabits());
            NavigateCalendarCommand = new Command<int>(NavigateCalendar);
            this.Calendar.DaysUpdated += this.SetDailyBackgroundColor;

            this.SetDailyBackgroundColor();
        }
        #endregion

        #region Methods
        private void SetDailyBackgroundColor(object sender = null, EventArgs e = null)
        {
            var storeData = this.Store.ReadMonthCheckIns(Calendar.NavigatedDate);
            foreach (var d in this.Calendar.Days)
            {
                if (this.SelectedHabit != null && storeData.ContainsKey(d.DateTime.Day) && this.HabitDoneOnDay(storeData[d.DateTime.Day], this.SelectedHabit))
                {
                    d.Color = Color.FromArgb("#757575");
                }
                else
                {
                    d.Color = Color.Parse("transparent");
                }
            }
        }

        private bool HabitDoneOnDay(CheckInDay day, string habit)
        {
            var habits = day.Habits.Where(h => habit.Equals(h.Name));
            return habits.Count() == 1 && habits.First().Done;
        }

        public void NavigateCalendar(int Amount)
        {
            Calendar?.NavigateCalendar(Amount);
        }

        public void UpdateHabit()
        {
            this.SetDailyBackgroundColor();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }
}
