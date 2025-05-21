using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using FlashQuizz.Models;
using FlashQuizz.Services;
using System.Collections.ObjectModel;

namespace FlashQuizz.ViewModels
{
    public abstract partial class CardsViewModelBase : ObservableObject
    {
        protected readonly CardService _cardService;

        [ObservableProperty]
        private ObservableCollection<FlashCard> cards;

        public CardsViewModelBase(CardService cardService)
        {
            _cardService = cardService;
            LoadCards();
        }

        protected void LoadCards()
        {
            var cardsFromDb = _cardService.GetAllCards();

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
