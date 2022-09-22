using DailyCheckinApp.Storage;
using DailyCheckinApp.ViewModels;

namespace DailyCheckinApp;

public partial class CalendarView : ContentPage
{
    private readonly IStore Store;

    public CalendarView()
	{
        InitializeComponent();
        this.Store = App.Current.Handler.MauiContext.Services.GetService<IStore>();
        BindingContext = new CalendarViewModel(this.OpenNewEditView, this.Store);
	}

    private async Task OpenNewEditView(DateTime targetDate)
    {
        await Navigation.PushAsync(new EditCheckInView(targetDate, this.Store));
    }

    private async Task OpenNewViewHabitsView()
    {
        await Navigation.PushAsync(new ViewHabitsView(this.Store));
    }

    private async Task OpenNewUpdateHabitsView()
    {
        await Navigation.PushAsync(new UpdateHabitsView(this.Store));
    }

    private async void OnEditCurrentCheckInClicked(object sender, EventArgs e)
    {
        await OpenNewEditView(DateTime.Today);
    }

    private async void OnViewHabitsClicked(object sender, EventArgs e)
    {
        await OpenNewViewHabitsView();
    }

    private async void OnUpdateHabitsClicked(object sender, EventArgs e)
    {
        await OpenNewUpdateHabitsView();
    }
}

