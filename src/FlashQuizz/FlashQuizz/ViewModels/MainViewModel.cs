using FlashQuizz.Models;
using FlashQuizz.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;
using FlashQuizz.Views;
using System.Diagnostics;

namespace FlashQuizz.ViewModels
{
    public partial class MainViewModel : CardsViewModelBase
    {
        [ObservableProperty]
        private FlashCard currentCard;

        public MainViewModel(CardService cardService) : base(cardService)
        {
            CurrentCard = new FlashCard();
        }

        [RelayCommand]
        private async Task AddCard()
        {
            CurrentCard = new FlashCard();
            await Shell.Current.GoToAsync(nameof(AddEditCardPage));
        }

        [RelayCommand]
        private async Task SaveCard()
        {
            try
            {
                if (CurrentCard.Id == 0)
                    _cardService.AddCard(CurrentCard);
                else
                    _cardService.UpdateCard(CurrentCard);

                LoadCards();
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to save card", "OK");
            }
        }




        /// <summary>
        /// Starts the learning mode if there are any cards.
        /// Passes the card list to the LearningPage.
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
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
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

        /// <summary>
        /// Navigates to the Begining page (MainPage).
        /// </summary>
        public async Task GoHomeCommand()
        {
            try
            {
                //absolute 
                await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
            }
        }
    }
}