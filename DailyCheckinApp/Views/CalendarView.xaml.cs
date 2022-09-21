using DailyCheckinApp.Storage;
using DailyCheckinApp.ViewModels;

namespace DailyCheckinApp;

public partial class CalendarView : ContentPage
{
    private readonly ICheckInDayStore Store;

    public CalendarView()
	{
        InitializeComponent();
        this.Store = App.Current.Handler.MauiContext.Services.GetService<ICheckInDayStore>();
        BindingContext = new CalendarViewModel(this.OpenNewEditView, this.Store);
	}

    private async Task OpenNewEditView(DateTime targetDate)
    {
        await Navigation.PushAsync(new EditCheckInView(targetDate, this.Store));
    }

    private async void OnEditCurrentCheckInClicked(object sender, EventArgs e)
    {
        await OpenNewEditView(DateTime.Today);
    }
}

