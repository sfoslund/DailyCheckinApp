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
        this.ViewModel = new EditCheckInViewModel(targetDate, this.Store);
        BindingContext = this.ViewModel;
    }

    private void OnTextChanged(object sender, EventArgs e)
    {
        this.ViewModel.UpdateNotes(e.ToString()); // TODO test
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        this.ViewModel.SaveCheckIn();
    }
}

