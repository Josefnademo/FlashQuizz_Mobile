using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using FlashQuizz.Models;
using FlashQuizz.Services;
using FlashQuizz.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FlashQuizz.ViewModels
{
    public partial class MyCardsViewModel : MainViewModel, IRecipient<CardsChangedMessage> 
    {
        [ObservableProperty]
        private FlashCard selectedCard;

        public MyCardsViewModel(CardService cardService) : base(cardService)
        {
            // Подписка на сообщения о изменении карточек
            WeakReferenceMessenger.Default.Register<CardsChangedMessage>(this);
        }

        partial void OnSelectedCardChanged(FlashCard value)
        {
            if (value != null)
            {
                EditCardCommand.Execute(value);
                SelectedCard = null;
            }
        }

        public void Receive(CardsChangedMessage message)
        {
            _ = LoadCardsAsync();
        }

        [RelayCommand]
        public async Task EditCard(FlashCard card)
        {
            if (card == null)
                return;

            string serializedCard = System.Text.Json.JsonSerializer.Serialize(card);
            // Кодируем, чтобы не было проблем с символами в URI
            string encodedCard = Uri.EscapeDataString(serializedCard);

            // Абсолютный маршрут с query-параметром
            string route = $"//{nameof(EditCardPage)}?CardToEdit={encodedCard}";

            await Shell.Current.GoToAsync(route);
        }



        [RelayCommand]
        public void DeleteCard(FlashCard card)
        {
            if (card == null)
                return;

            _cardService.DeleteCard(card);

            // Отправляем сообщение, чтобы обновить списки
            WeakReferenceMessenger.Default.Send(new CardsChangedMessage());
        }
    }

    public class CardsChangedMessage : ValueChangedMessage<bool>
    {
        public CardsChangedMessage() : base(true) { }
    }
}
