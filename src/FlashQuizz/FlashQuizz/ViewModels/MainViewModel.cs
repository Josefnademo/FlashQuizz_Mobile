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
    public partial class MainViewModel : ObservableObject
    {
        private readonly CardService _cardService;

        [ObservableProperty]
        private ObservableCollection<FlashCard> _cards;

        [ObservableProperty]
        private FlashCard _currentCard;

        public MainViewModel(CardService cardService)
        {
            _cardService = cardService;
            CurrentCard = new FlashCard(); // inticialisation by default 
            LoadCards();
        }

        private void LoadCards()
        {
            Cards = _cardService.GetAllCards();
        }

        [RelayCommand]
        private async Task AddCard()
        {
            CurrentCard = new FlashCard();
            await Shell.Current.GoToAsync(nameof(AddEditCardPage));
        }

        [RelayCommand]
        private async Task EditCard(FlashCard card)
        {
            if (card == null) return;

            CurrentCard = new FlashCard // Create a copy for editing
            {
                Id = card.Id,
                Question = card.Question,
                Answer = card.Answer,
                TimesShown = card.TimesShown,
                TimesCorrect = card.TimesCorrect
            };

            await Shell.Current.GoToAsync(nameof(AddEditCardPage));
        }

        [RelayCommand]
        private void DeleteCard(FlashCard card)
        {
            _cardService.DeleteCard(card);
            LoadCards();
        }

        [RelayCommand]
        private async Task SaveCard()
        {
            try
            {
                if (CurrentCard == null)
                {
                    CurrentCard = new FlashCard(); // Create a new card if null
                }

                if (CurrentCard.Id == 0)
                {
                    _cardService.AddCard(CurrentCard);
                }
                else
                {
                    _cardService.UpdateCard(CurrentCard);
                }

                LoadCards();
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while saving: {ex.Message}");
                
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to save card", "OK");
            }
        }

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