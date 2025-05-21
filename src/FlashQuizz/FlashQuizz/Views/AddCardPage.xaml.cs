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
    /*
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (BindingContext is MainViewModel vm)
        {
            // Проверяем, передана ли карточка для редактирования
            var queryParameters = HttpUtility.ParseQueryString(Shell.Current.CurrentState.Location.Query);
            if (queryParameters["CardToEdit"] is string cardJson)
            {
                // Десериализуем карточку из строки
                var card = System.Text.Json.JsonSerializer.Deserialize<FlashCard>(cardJson);
                if (card != null)
                {
                    vm.CurrentCard = card;
                }
            }
            else
            {
                // Если параметра нет — создаём новую карточку
                vm.CurrentCard = new FlashCard();
            }
        }
    }*/



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