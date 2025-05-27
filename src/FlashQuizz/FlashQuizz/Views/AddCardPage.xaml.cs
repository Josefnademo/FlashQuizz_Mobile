using System.Web;
using FlashQuizz.Models;
using FlashQuizz.ViewModels;

namespace FlashQuizz.Views;

public partial class AddCardPage : ContentPage
{
	private readonly MainViewModel _viewModel;

	public AddCardPage(MainViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    /// <summary>
    /// Called when the page appears.
    /// Resets the CurrentCard in the ViewModel to a new instance to clear the form.
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.CurrentCard = new FlashCard();
    }

}