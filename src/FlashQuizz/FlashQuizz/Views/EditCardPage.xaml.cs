using FlashQuizz.Models;
using FlashQuizz.ViewModels;

namespace FlashQuizz.Views;

public partial class EditCardPage : ContentPage, IQueryAttributable
{
    public EditCardPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    /// <summary>
    /// Receives query parameters when navigating to this page.
    /// Deserializes the card data from the query and sets it as the current card in the ViewModel.
    /// If no card is provided, initializes a new card.
    /// </summary>
    /// <param name="query">Dictionary of query parameters passed during navigation.</param>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (BindingContext is MainViewModel vm)
        {
            if (query.TryGetValue("CardToEdit", out var cardJsonObj) && cardJsonObj is string cardJson)
            {
                var decoded = Uri.UnescapeDataString(cardJson);
                //Deserialize is a process in programming where data in a specific format (usually a string,
                //such as JSON or XML) is converted back into an object or data structure in your code.
                var card = System.Text.Json.JsonSerializer.Deserialize<FlashCard>(decoded);
                if (card != null)
                {
                    vm.CurrentCard = card;
                }
            }
            else
            {
                vm.CurrentCard = new FlashCard();
            }
        }
    }
}