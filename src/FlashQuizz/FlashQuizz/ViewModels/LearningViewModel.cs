using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlashQuizz.Models;
using FlashQuizz.Services;
using FlashQuizz.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlashQuizz.ViewModels
{
    public partial class LearningViewModel : ObservableObject
    {
        private readonly CardService _cardService;
        private List<FlashCard> _allCards;
        private List<FlashCard> _remainingCards;
        private SessionStats _sessionStats;
        private Random _random = new Random();

        [ObservableProperty]
        private FlashCard currentCard;

        [ObservableProperty]
        private bool isAnswerShown;

        [ObservableProperty]
        private string progressText;

        public LearningViewModel(CardService cardService)
        {
            _cardService = cardService;
            _sessionStats = new SessionStats { StartTime = DateTime.Now };
            Initialize();
        }

        private async void Initialize()
        {
            _allCards = await _cardService.GetAllCardsAsync();
            _remainingCards = new List<FlashCard>(_allCards);
            ShowNextCard();
            UpdateProgress();
        }

        private void ShowNextCard()
        {
            if (_remainingCards.Count == 0)
            {
                EndSession();
                return;
            }

            int index = _random.Next(_remainingCards.Count);
            CurrentCard = _remainingCards[index];
            IsAnswerShown = false;
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            int total = _allCards.Count;
            int remaining = _remainingCards.Count;
            int completed = total - remaining;
            ProgressText = $"Progression: {completed}/{total}";
        }

        [RelayCommand]
        private async Task ShowAnswer()
        {
            if (CurrentCard == null) return;

            IsAnswerShown = true;
            
            // When showing the answer, count it as "don't know"
            CurrentCard.TimesShown++;
            await _cardService.UpdateCardAsync(CurrentCard);

            _sessionStats.CardsStudied.Add(CurrentCard);
            _sessionStats.DifficultCards.Add(CurrentCard);
        }

        [RelayCommand]
        private async Task KnowCard()
        {
            if (CurrentCard == null) return;

            // Since we already counted this as "don't know" when showing the answer,
            // we need to update the stats to reflect that the user actually knew it
            CurrentCard.TimesCorrect++;
            await _cardService.UpdateCardAsync(CurrentCard);

            // Remove from difficult cards since the user actually knew it
            _sessionStats.DifficultCards.RemoveAll(c => c.Id == CurrentCard.Id);
            
            _remainingCards.Remove(CurrentCard);
            ShowNextCard();
        }

        [RelayCommand]
        public async Task OnShakeDetected()
        {
            if (CurrentCard == null || !IsAnswerShown) return;
            
            // The card was already counted as "don't know" when showing the answer,
            // so we just need to move to the next card
            _remainingCards.Remove(CurrentCard);
            ShowNextCard();
        }

        [RelayCommand]
        private async Task StopSession()
        {
            _sessionStats.EndTime = DateTime.Now;
            
            // Navigate to summary page with session stats
            var navigationParameter = new Dictionary<string, object>
            {
                { "SessionStats", _sessionStats }
            };
            
            await Shell.Current.GoToAsync(nameof(SessionSummaryPage), navigationParameter);
        }

        private async void EndSession()
        {
            await StopSession();
        }
    }
}
