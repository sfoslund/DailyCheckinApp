using DailyCheckinApp.ViewModels;

namespace DailyCheckinApp;

public partial class CalendarView : ContentPage
{
    public CalendarView()
	{
		InitializeComponent();
        BindingContext = new CalendarViewModel();
	}

    private async void OnEditCurrentCheckInClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditCheckInView());
    }
}

