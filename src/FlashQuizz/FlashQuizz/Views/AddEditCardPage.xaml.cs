using System.Web;
using FlashQuizz.Models;
using FlashQuizz.ViewModels;

namespace FlashQuizz.Views;

public partial class AddEditCardPage : ContentPage
{
    public AddEditCardPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

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
    }
}