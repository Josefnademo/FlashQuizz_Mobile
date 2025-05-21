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

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (BindingContext is MainViewModel vm)
        {
            if (query.TryGetValue("CardToEdit", out var cardJsonObj) && cardJsonObj is string cardJson)
            {
                var decoded = Uri.UnescapeDataString(cardJson);
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



    /*
	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is MainViewModel viewModel)
		{
			viewModel.RefreshCardsCommand.Execute(null);
		}
	}
	private async void OnCardSelected(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.FirstOrDefault() is FlashCard selectedCard)
		{
			var vm = BindingContext as MainViewModel;
			if (vm != null)
				await vm.EditCardCommand.ExecuteAsync(selectedCard);
		}
	}*/
}