using System.Collections.ObjectModel;
using System.Windows.Input;
using FlashQuizz.Models;
using FlashQuizz.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Runtime.CompilerServices;

namespace FlashQuizz.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<FlashCard> cards;

        [ObservableProperty]
        bool isRefreshing;

        public ICommand AddCardCommand { get; }
        public ICommand StartLearningCommand { get; }
        public ICommand RefreshCardsCommand { get; }
        public ICommand EditCardCommand { get; }

        public MainViewModel()
        {
            Cards = new ObservableCollection<FlashCard>();

            // Commands
            AddCardCommand = new AsyncRelayCommand(OnAddCard);
            StartLearningCommand = new AsyncRelayCommand(OnStartLearning, CanStartLearning);
            RefreshCardsCommand = new AsyncRelayCommand(OnRefreshCards);
            EditCardCommand = new AsyncRelayCommand<FlashCard>(OnEditCard);

            LoadDummyData(); // or loading from the database
        }

        void LoadDummyData()
        {
            Cards.Add(new FlashCard { Question = "Quelle est la capitale de la France ?", Answer = "Paris" });
            Cards.Add(new FlashCard { Question = "Combien font 2 + 2 ?", Answer = "4" });
        }

        bool CanStartLearning()
        {
            return Cards != null && Cards.Count > 0;
        }

        [ObservableProperty]
        bool hasCards;

        async Task OnAddCard()
        {
            await Shell.Current.GoToAsync(nameof(AddEditCardPage));
        }

        async Task OnEditCard(FlashCard selectedCard)
        {
            var parameters = new Dictionary<string, object>
            {
                { "CardToEdit", selectedCard }
            };

            await Shell.Current.GoToAsync(nameof(AddEditCardPage), true, parameters);
        }

        async Task OnStartLearning()
        {
            var parameters = new Dictionary<string, object>
            {
                { "Cards", Cards.ToList() }
            };

            await Shell.Current.GoToAsync(nameof(LearningPage), true, parameters);
        }

        async Task OnRefreshCards()
        {
            IsRefreshing = true;

            //  download current data from the DB
            await Task.Delay(1000);

            IsRefreshing = false;
        }

        // Method to remove a card
        public void DeleteCard(FlashCard card)
        {
            if (Cards.Contains(card))
                Cards.Remove(card);

            HasCards = Cards.Count > 0;
        }

        // Method for adding or updating a map
        public void AddOrUpdateCard(FlashCard card, bool isEdit = false)
        {
            if (isEdit)
            {
                var existing = Cards.FirstOrDefault(c => c.Id == card.Id);
                if (existing != null)
                {
                    existing.Question = card.Question;
                    existing.Answer = card.Answer;
                    return;
                }
            }

            Cards.Add(card);
            HasCards = true;
        }
    }
}
