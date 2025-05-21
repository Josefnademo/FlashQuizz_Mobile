using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlashQuizz.Models;
using FlashQuizz.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashQuizz.Views;
using System.Diagnostics;

namespace FlashQuizz.ViewModels
{  /// <summary>
   /// Base ViewModel for managing flash cards.
   /// Provides common logic for loading, adding, and tracking cards.
   /// </summary>
    public abstract partial class CardsViewModelBase : ObservableObject
    {
        /// <summary>
        /// Service for managing flash cards in the database.
        /// </summary>
        protected readonly CardService _cardService;

        /// <summary>
        /// The collection of flash cards displayed in the UI.
        /// </summary
        [ObservableProperty]
        private ObservableCollection<FlashCard> cards;

        /// <summary>
        /// The currently selected or edited flash card.
        /// </summary>
        [ObservableProperty]
        private FlashCard currentCard;

        /// <summary>
        /// Constructor. Initializes the card service and loads cards from the database.
        /// </summary>
        public CardsViewModelBase(CardService cardService)
        {
            CurrentCard = new FlashCard();
            _cardService = cardService;
            LoadCardsAsync();
        }

        /// <summary>
        /// Navigates to the AddCardPage to add a new flash card.
        /// </summary>
        [RelayCommand]
        private async Task AddCard()
        {
            CurrentCard = new FlashCard();
            await Shell.Current.GoToAsync($"//{nameof(AddCardPage)}");

        }

        /// <summary>
        /// Loads all flash cards from the database and updates the Cards collection.
        /// </summary>
        protected async Task LoadCardsAsync()
        {
            var cardsFromDb = await _cardService.GetAllCardsAsync();

            if (Cards == null)
                Cards = new ObservableCollection<FlashCard>(cardsFromDb);
            else
            {
                Cards.Clear();
                foreach (var card in cardsFromDb)
                    Cards.Add(card);
            }
        }

    }
}

