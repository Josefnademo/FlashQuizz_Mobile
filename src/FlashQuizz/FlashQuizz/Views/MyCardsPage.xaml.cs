using FlashQuizz.ViewModels;
using FlashQuizz.Services;
using FlashQuizz.Models;
using Microsoft.Extensions.DependencyInjection;

namespace FlashQuizz.Views;

public partial class MyCardsPage : ContentPage
{
    public MyCardsPage()
    {
        InitializeComponent();
        var cardService = App.Current.Handler.MauiContext.Services.GetService<CardService>();
        BindingContext = new MyCardsViewModel(cardService);
    }

    private async void OnCardSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is FlashCard selectedCard)
        {
            var vm = BindingContext as MyCardsViewModel;
            if (vm != null)
                await vm.EditCardCommand.ExecuteAsync(selectedCard);
        }
    }
    /*  protected override void OnAppearing()
      {
          base.OnAppearing();

          if (BindingContext is MainViewModel viewModel)
          {
              viewModel.RefreshCardsCommand.Execute(null);
          }
      }*/

}