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

    /// <summary>
    /// Handles the event when a card is selected from the list.
    /// Executes the EditCardCommand for the selected card.
    /// </summary>
    /// <param name="sender">The sender of the event (the collection view).</param>
    /// <param name="e">The selection changed event arguments.</param>
    private async void OnCardSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is FlashCard selectedCard)
        {
            var vm = BindingContext as MyCardsViewModel;
            if (vm != null)
                await vm.EditCardCommand.ExecuteAsync(selectedCard);
        }
    }
}