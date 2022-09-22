using DailyCheckinApp.Storage;
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

        private IStore Store;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public UpdateHabitsViewModel(Func<Task> returnNavigationCallback, IStore store)
        {
            this.ReturnNavigationCallback = returnNavigationCallback;
            this.Store = store;
            this.Habits = new ObservableRangeCollection<string>(this.Store.ReadHabits());
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
            this.Store.WriteHabits(this.Habits);
            this.ReturnNavigationCallback();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }
}
