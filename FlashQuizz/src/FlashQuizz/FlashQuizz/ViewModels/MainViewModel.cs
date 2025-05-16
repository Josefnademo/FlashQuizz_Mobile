using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace FlashQuizz.ViewModels;


    public class MainViewModel : INotifyPropertyChanged
    {
        // Card management
        public ObservableCollection<FlashCard> Cards { get; } = new();
        public FlashCard CurrentCard { get; set; }
        public bool IsEditing { get; set; }

        // Learning mode
        public bool IsQuestionShowing { get; set; } = true;
        public bool IsAnswerShowing { get; set; }
        public string ProgressText { get; set; }

        // Commands
        public ICommand AddCardCommand { get; }
        public ICommand EditCardCommand { get; }
        public ICommand SaveCardCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand StartLearningCommand { get; }
        public ICommand ShowAnswerCommand { get; }
        public ICommand KnowCardCommand { get; }
        public ICommand DontKnowCardCommand { get; }
        public ICommand ReturnToMenuCommand { get; }

        public MainViewModel()
        {
            // Initialize commands
            AddCardCommand = new Command(OnAddCard);
            EditCardCommand = new Command<FlashCard>(OnEditCard);
            SaveCardCommand = new Command(OnSaveCard);
            CancelCommand = new Command(OnCancel);
            StartLearningCommand = new Command(OnStartLearning);
            ShowAnswerCommand = new Command(() =>
            {
                IsQuestionShowing = false;
                IsAnswerShowing = true;
            });
            KnowCardCommand = new Command(OnKnowCard);
            DontKnowCardCommand = new Command(OnDontKnowCard);
            ReturnToMenuCommand = new Command(OnReturnToMenu);
        }

        private void OnAddCard()
        {
            CurrentCard = new FlashCard();
            IsEditing = false;
            // Navigation to Add/Edit page
        }

        private void OnEditCard(FlashCard card)
        {
            CurrentCard = card;
            IsEditing = true;
            // Navigation to Add/Edit page
        }

        private void OnSaveCard()
        {
            if (!IsEditing)
            {
                Cards.Add(CurrentCard);
            }
            // Navigation back
        }

        private void OnStartLearning()
        {
            if (Cards.Any())
            {
                ShuffleCards();
                CurrentCard = Cards.First();
                // Navigation to Learning page
            }
        }

        private void ShuffleCards()
        {
            var rng = new Random();
            Cards = new ObservableCollection<FlashCard>(Cards.OrderBy(x => rng.Next()));
        }

    }
