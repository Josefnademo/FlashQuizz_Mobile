
using FlashQuizz.ViewModels;
namespace FlashQuizz.Views;

public partial class MyCardsPage : ContentPage
{
    public MyCardsPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}