using DailyCheckinApp.Storage;
using DailyCheckinApp.ViewModels;

namespace DailyCheckinApp;

public partial class EditCheckInView : ContentPage
{
    private readonly EditCheckInViewModel ViewModel;
    private readonly ICheckInDayStore Store;

	public EditCheckInView(DateTime targetDate, ICheckInDayStore store)
	{
		InitializeComponent();
        this.Store = store;
        this.ViewModel = new EditCheckInViewModel(targetDate, this.Store, this.ReturnNavigation);
        BindingContext = this.ViewModel;
    }

    private void OnTextChanged(object sender, EventArgs e)
    {
        this.ViewModel.UpdateNotes((e as TextChangedEventArgs).NewTextValue);
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        this.ViewModel.SaveCheckIn();
    }

    private async Task ReturnNavigation()
    {
        await Navigation.PopAsync();
    }
}

