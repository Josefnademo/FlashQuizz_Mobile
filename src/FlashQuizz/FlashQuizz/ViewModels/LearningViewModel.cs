using FlashQuizz.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;
using FlashQuizz.Views;



namespace FlashQuizz.ViewModels
{
    public partial class LearningViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<FlashCard> _cards;

        [ObservableProperty]
        private FlashCard _currentCard;

        [ObservableProperty]
        private bool _isQuestionShowing = true;

        [ObservableProperty]
        private string _progressText;

        private List<FlashCard> _sessionCards;
        private DateTime _sessionStartTime;
        private readonly List<FlashCard> _difficultCards = new();

        public LearningViewModel(IEnumerable<FlashCard> cards)
        {
            Cards = new ObservableCollection<FlashCard>(cards);
            StartSession();
        }

        private void StartSession()
        {
            _sessionStartTime = DateTime.Now;
            _sessionCards = new List<FlashCard>(Cards);
            ShuffleCards();
            CurrentCard = _sessionCards.First();
            ProgressText = $"Carte 1/{_sessionCards.Count}";
        }

        private void ShuffleCards()
        {
            var rng = new Random();
            _sessionCards = _sessionCards.OrderBy(x => rng.Next()).ToList();
        }

        [RelayCommand]
        private void ShowAnswer()
        {
            IsQuestionShowing = false;
        }

        [RelayCommand]
        private void KnowCard()
        {
            CurrentCard.TimesShown++;
            CurrentCard.TimesCorrect++;
            NextCard();
        }

        [RelayCommand]
        private void DontKnowCard()
        {
            CurrentCard.TimesShown++;
            _difficultCards.Add(CurrentCard);
            NextCard();
        }

        private void NextCard()
        {
            var currentIndex = _sessionCards.IndexOf(CurrentCard);
            if (currentIndex < _sessionCards.Count - 1)
            {
                CurrentCard = _sessionCards[currentIndex + 1];
                ProgressText = $"Carte {currentIndex + 2}/{_sessionCards.Count}";
                IsQuestionShowing = true;
            }
            else
            {
                EndSession();
            }
        }

        private void EndSession()
        {
            var sessionStats = new SessionStats
            {
                StartTime = _sessionStartTime,
                EndTime = DateTime.Now,
                CardsStudied = _sessionCards,
                DifficultCards = _difficultCards
            };

            Shell.Current.GoToAsync($"///{nameof(SessionSummaryPage)}?SessionStats={Uri.EscapeDataString(JsonSerializer.Serialize(sessionStats))}");

        }
    }
}