using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlashQuizz.Models;
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
        private List<FlashCard> _cards = new List<FlashCard>();
        private int _currentIndex = 0;

        [ObservableProperty]
        private FlashCard currentCard;

        [ObservableProperty]
        private string displayedText;

        [ObservableProperty]
        private bool isAnswerShown = false;

        [ObservableProperty]
        private string progressText;

        private SessionStats _sessionStats = new SessionStats();

        // Инициализация карт
        public void Initialize(List<FlashCard> cards)
        {
            _cards = cards ?? new List<FlashCard>();
            _currentIndex = 0;

            if (_cards.Any())
            {
                SetCurrentCard(_cards[_currentIndex]);
            }
            else
            {
                displayedText = "Нет доступных карт";
                currentCard = null;
                progressText = string.Empty;
            }
        }

        private void SetCurrentCard(FlashCard card)
        {
            currentCard = card;
            displayedText = card.Question;
            isAnswerShown = false;
            UpdateProgressText();
        }

        // При тапе на карту показываем ответ и сразу считаем как "не знаю"
        [RelayCommand]
        public async Task ShowAnswer()
        {
            if (currentCard == null || isAnswerShown)
                return;

            displayedText = currentCard.Answer;
            isAnswerShown = true;

            // Подождать секунду, чтобы пользователь увидел ответ
            await Task.Delay(1000);

            // Считаем карту "не знаю" и идём дальше
            await DontKnowCard();
        }

        // Кнопка "Знаю" - засчитываем карту и идём дальше
        [RelayCommand]
        public async Task KnowCard()
        {
            if (currentCard == null)
                return;

            _sessionStats.CardsStudied.Add(currentCard);
            currentCard.TimesShown++;
            currentCard.TimesCorrect++;

            await MoveToNextCard();
        }

        // Встряска устройства — считаем "не знаю" и идём дальше
        public async Task OnShakeDetected()
        {
            await DontKnowCard();
        }

        // Логика для "не знаю"
        private async Task DontKnowCard()
        {
            if (currentCard == null)
                return;

            _sessionStats.CardsStudied.Add(currentCard);
            _sessionStats.DifficultCards.Add(currentCard);
            currentCard.TimesShown++;

            await MoveToNextCard();
        }

        // Переход к следующей карте или завершение сессии
        private async Task MoveToNextCard()
        {
            _currentIndex++;

            if (_currentIndex < _cards.Count)
            {
                SetCurrentCard(_cards[_currentIndex]);
            }
            else
            {
                displayedText = "Все карты изучены!";
                isAnswerShown = false;
                currentCard = null;
                progressText = string.Empty;

                await StopSession();
            }
        }

        private void UpdateProgressText()
        {
            progressText = $"Карта {_currentIndex + 1} из {_cards.Count}";
        }

        [RelayCommand]
        private async Task StopSession()
        {
            _sessionStats.EndTime = DateTime.Now;

            var navigationParameter = new Dictionary<string, object>
            {
                { "SessionStats", _sessionStats },
                { "Cards", _cards }
            };

            await Shell.Current.GoToAsync($"///{nameof(SessionSummaryPage)}", navigationParameter);
        }
    }

    public class SessionStats
    {
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; }
        public List<FlashCard> CardsStudied { get; set; } = new();
        public List<FlashCard> DifficultCards { get; set; } = new();
    }
}
