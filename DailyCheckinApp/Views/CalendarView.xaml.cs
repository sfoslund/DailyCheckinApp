using DailyCheckinApp.ViewModels;

namespace DailyCheckinApp;

public partial class CalendarView : ContentPage
{
    public CalendarView()
	{
		InitializeComponent();
        BindingContext = new CalendarViewModel(this.OpenNewEditView);
	}

    private async Task OpenNewEditView(DateTime targetDate)
    {
        await Navigation.PushAsync(new EditCheckInView(targetDate));
    }

    private async void OnEditCurrentCheckInClicked(object sender, EventArgs e)
    {
        await OpenNewEditView(DateTime.Today);
    }
}

