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
{ /// <summary>
  /// ViewModel for the "My Cards" page.
  /// Handles editing, deleting, and updating cards, and responds to card change messages.
  /// </summary>
    public partial class MyCardsViewModel : MainViewModel, IRecipient<CardsChangedMessage> 
    {
        [ObservableProperty]
        private FlashCard selectedCard;

        /// <summary>
        /// Constructor. Registers to receive card change messages.
        /// </summary>
        public MyCardsViewModel(CardService cardService) : base(cardService)
        {
            // Subscribe to card change messages
            WeakReferenceMessenger.Default.Register<CardsChangedMessage>(this);
        }


        /// <summary>
        /// Handles when the selected card changes.
        /// Opens the edit page for the selected card.
        /// </summary>
        partial void OnSelectedCardChanged(FlashCard value)
        {
            if (value != null)
            {
                EditCardCommand.Execute(value);
                SelectedCard = null;
            }
        }

        /// <summary>
        /// Handles receiving a CardsChangedMessage and reloads the card list.
        /// </summary>
        public void Receive(CardsChangedMessage message)
        {
            _ = LoadCardsAsync();
        }

        /// <summary>
        /// Navigates to the edit page for the given card.
        /// </summary>
        [RelayCommand]
        public async Task EditCard(FlashCard card)
        {
            if (card == null)
                return;

            string serializedCard = System.Text.Json.JsonSerializer.Serialize(card);
            // Encode to avoid URI character issues
            string encodedCard = Uri.EscapeDataString(serializedCard);

            // Absolute route with query parameter
            string route = $"//{nameof(EditCardPage)}?CardToEdit={encodedCard}";

            await Shell.Current.GoToAsync(route);
        }


        /// <summary>
        /// Deletes the given card and sends a message to update the card list in other views.
        /// </summary>
        /// <param name=
        [RelayCommand]
        public void DeleteCard(FlashCard card)
        {
            if (card == null)
                return;

            _cardService.DeleteCard(card);

            // Send a message to update card lists
            WeakReferenceMessenger.Default.Send(new CardsChangedMessage());
        }
    }
    /// <summary>
    /// Message for notifying that the card list has changed.
    /// </summary>
    public class CardsChangedMessage : ValueChangedMessage<bool>
    {
        public CardsChangedMessage() : base(true) { }
    }
}
