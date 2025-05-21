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
{
    public abstract partial class CardsViewModelBase : ObservableObject
    {
        protected readonly CardService _cardService;

        [ObservableProperty]
        private ObservableCollection<FlashCard> cards;


        [ObservableProperty]
        private FlashCard currentCard;


        public CardsViewModelBase(CardService cardService)
        {
            CurrentCard = new FlashCard();
            _cardService = cardService;
            LoadCardsAsync();
        }


        [RelayCommand]
        private async Task AddCard()
        {
            CurrentCard = new FlashCard();
            await Shell.Current.GoToAsync($"//{nameof(AddCardPage)}");

        }


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

