using DailyCheckinApp.Models;
using DailyCheckinApp.ViewModels;

namespace DailyCheckinApp;

public partial class UpdateHabitsView : ContentPage
{
    private readonly UpdateHabitsViewModel ViewModel;

	public UpdateHabitsView()
	{
		InitializeComponent();
        this.ViewModel = new UpdateHabitsViewModel(this.ReturnNavigation);
        BindingContext = this.ViewModel;
    }

    private void OnTextChanged(object sender, EventArgs e)
    {
        var entry = sender as Entry;
        var oldHabit = (string)entry.BindingContext;
        var newHabit = entry.Text;
        this.ViewModel.UpdateHabit(oldHabit, newHabit);
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var habit = (string)button.BindingContext;
        this.ViewModel.DeleteHabit(habit);
    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        this.ViewModel.AddHabit();
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        this.ViewModel.SaveHabits();
    }

    private async Task ReturnNavigation()
    {
        await Navigation.PopAsync();
    }
}

