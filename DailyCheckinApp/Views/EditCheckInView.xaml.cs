using DailyCheckinApp.ViewModels;

namespace DailyCheckinApp;

public partial class EditCheckInView : ContentPage
{
    private readonly EditCheckInViewModel ViewModel;

	public EditCheckInView(DateTime targetDate)
	{
		InitializeComponent();
        this.ViewModel = new EditCheckInViewModel(targetDate);
        BindingContext = this.ViewModel;
    }

    private void OnTextChanged(object sender, EventArgs e)
    {
        this.ViewModel.UpdateNotes(e.ToString()); // TODO test
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        // TODO add storage
    }
}

