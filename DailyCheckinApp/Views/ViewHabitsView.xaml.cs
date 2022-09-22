using DailyCheckinApp.Storage;
using DailyCheckinApp.ViewModels;

namespace DailyCheckinApp;

public partial class ViewHabitsView : ContentPage
{
    private readonly IStore Store;
    private readonly ViewHabitsViewModel ViewModel;

    public ViewHabitsView(IStore store)
	{
        InitializeComponent();
        this.Store = store;
        this.ViewModel = new ViewHabitsViewModel(this.Store);
        BindingContext = this.ViewModel;
	}

    private void OnHabitChanged(object sender, EventArgs e)
    {
        this.ViewModel.UpdateHabit();
    }
}

