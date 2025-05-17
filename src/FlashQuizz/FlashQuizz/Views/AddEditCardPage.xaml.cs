namespace FlashQuizz.Views;
using FlashQuizz.ViewModels;

public partial class AddEditCardPage : ContentPage
{
    public AddEditCardPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}