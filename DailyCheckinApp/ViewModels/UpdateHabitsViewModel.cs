using PropertyChanged;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using XCalendar.Core.Models;

namespace DailyCheckinApp.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    internal class UpdateHabitsViewModel : INotifyPropertyChanged
    {
        #region Properties
        public ObservableRangeCollection<string> Habits { get; set; }

        private Func<Task> ReturnNavigationCallback;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public UpdateHabitsViewModel(Func<Task> returnNavigationCallback)
        {
            this.ReturnNavigationCallback = returnNavigationCallback;
            this.Habits = new ObservableRangeCollection<string>() { "test1", "test2" }; // TODO load from store
        }
        #endregion

        #region methods
        internal void DeleteHabit(string habit)
        {
            this.Habits.Remove(habit);
        }

        internal void AddHabit()
        {
            this.Habits.Add(string.Empty);
        }

        internal void UpdateHabit(string oldHabit, string newHabit)
        {
            var index = this.Habits.IndexOf(oldHabit);
            this.Habits[index] = newHabit;
        }

        internal void SaveHabits()
        {
            // TODO save to store
            this.ReturnNavigationCallback();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }
}
