using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashQuizz.Models;
using FlashQuizz.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;
using FlashQuizz.Views;


namespace FlashQuizz.ViewModels
{
    public partial class MyCardsViewModel : CardsViewModelBase
    {
        [ObservableProperty]
        private FlashCard selectedCard;

        public MyCardsViewModel(CardService cardService) : base(cardService)
        {
            SubscribeToCardEvents();
        }

        partial void OnSelectedCardChanged(FlashCard value)
        {
            if (value != null)
            {
                EditCardCommand.Execute(value);
                SelectedCard = null;
            }
        }

        private void SubscribeToCardEvents()
        {
            MessagingCenter.Subscribe<object>(this, "CardsChanged", (sender) => LoadCards());
        }

        [RelayCommand]
        public async Task EditCard(FlashCard card)
        {
            if (card == null) return;
            var navParams = new Dictionary<string, object>
            {
                { "CardToEdit", System.Text.Json.JsonSerializer.Serialize(card) }
            };
            await Shell.Current.GoToAsync(nameof(AddEditCardPage), navParams);
        }
        [RelayCommand]
        private void DeleteCard(FlashCard card)
        {
            if (card == null) return;
            _cardService.DeleteCard(card);
            MessagingCenter.Send(this, "CardsChanged");
        }
    }
}