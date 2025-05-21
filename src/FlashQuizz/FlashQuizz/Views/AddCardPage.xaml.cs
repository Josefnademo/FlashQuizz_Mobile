using System.Web;
using FlashQuizz.Models;
using FlashQuizz.ViewModels;

namespace FlashQuizz.Views;

public partial class AddCardPage : ContentPage
{
	public AddCardPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    /// <summary>
    /// Called when the page appears.
    /// Resets the CurrentCard in the ViewModel to a new instance to clear the form.
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is MainViewModel vm)
        {
            // IMPORTANT: create a new instance so the form is empty
            vm.CurrentCard = new FlashCard();
        }
    }

}