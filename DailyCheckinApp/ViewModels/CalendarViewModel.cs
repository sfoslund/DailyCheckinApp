using System.Windows.Input;
using XCalendar.Core.Models;

namespace DailyCheckinApp.ViewModels
{
    internal class CalendarViewModel
    {
        #region Properties
        public Calendar<CalendarDay> Calendar { get; set; } = new Calendar<CalendarDay>()
        {
            SelectionType = XCalendar.Core.Enums.SelectionType.Single,
            SelectionAction = XCalendar.Core.Enums.SelectionAction.Replace,
            SelectedDates = new ObservableRangeCollection<DateTime>() { DateTime.Today },
        };

        private Func<DateTime, Task> OpenNewEditView;
        #endregion

        #region Commands
        public ICommand NavigateCalendarCommand { get; set; }
        public ICommand ChangeDateSelectionCommand { get; set; }
        #endregion

        #region Constructors
        public CalendarViewModel(Func<DateTime, Task> openNewEditView)
        {
            this.OpenNewEditView = openNewEditView;
            NavigateCalendarCommand = new Command<int>(NavigateCalendar);
            ChangeDateSelectionCommand = new Command<DateTime>(ChangeDateSelection);
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
        #endregion
    }
}
