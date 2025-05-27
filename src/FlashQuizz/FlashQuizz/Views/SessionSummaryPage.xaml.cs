namespace FlashQuizz.Views;
using FlashQuizz.Models;
using FlashQuizz.ViewModels;
using Microsoft.Maui.Controls;
public partial class SessionSummaryPage : ContentPage
{
    public SessionSummaryPage(SessionSummaryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
} 