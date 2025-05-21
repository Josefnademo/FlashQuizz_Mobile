using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FlashQuizz.Models;
using FlashQuizz.Services;
using FlashQuizz.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;

namespace FlashQuizz.ViewModels
{ /// <summary>
  /// Main ViewModel for managing flashcards, saving, navigation, and learning mode.
  /// </summary>
    public partial class MainViewModel : CardsViewModelBase
    {
        [ObservableProperty]
        private FlashCard currentCard;

        /// <summary>
        /// Constructor. Initializes the MainViewModel with the card service.
        /// </summary>
        public MainViewModel(CardService cardService) : base(cardService)
        {
            CurrentCard = new FlashCard();
        }

        /// <summary>
        /// Saves the current card. Adds a new card if Id is 0, otherwise updates the existing card.
        /// Sends a message to update the card list and navigates back to the card list page.
        /// </summary>
        [RelayCommand]
        private async Task SaveCard()
        {
            try
            {
                if (CurrentCard.Id == 0)
                    _cardService.AddCard(CurrentCard);
                else
                    _cardService.UpdateCard(CurrentCard);

                // Send message to update card list
                WeakReferenceMessenger.Default.Send(new CardsChangedMessage());

                // Navigate back to the card list
                await Shell.Current.GoToAsync($"///{nameof(MyCardsPage)}");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to save card: {ex.Message}", "OK");
            }
        }




        /// <summary>
        /// Starts the learning mode if there are any cards.
        /// Passes the card list to the LearningPage as a parameter.
        /// </summary>
        [RelayCommand]
        private async Task StartLearning()
        {
            if (Cards?.Any() == true)
            {
                var cardsParam = Uri.EscapeDataString(JsonSerializer.Serialize(Cards.ToList()));
                await Shell.Current.GoToAsync($"{nameof(LearningPage)}?Cards={cardsParam}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Attention", "Aucune carte disponible", "OK");
            }
        }

        /// <summary>
        /// Navigates back to the previous page.
        /// </summary>
        [RelayCommand]
        public async Task Cancel()
        {
            try
            {
                await Application.Current.MainPage.DisplayAlert("Info", "CancelCommand executed : ", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"FailedCancelCommand: {ex.Message}", "OK");
            }
        }


        /// <summary>
        /// Navigates to the page displaying all flashcards (MyCardsPage).
        /// </summary>
        [RelayCommand]
        private async Task ViewCards()
        {
            try
            {
                //absolute 
                await Shell.Current.GoToAsync($"///{nameof(MyCardsPage)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to navigate", "OK");
            }
        }

       
    }
}