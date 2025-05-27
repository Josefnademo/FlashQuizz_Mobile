using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlashQuizz.Models;

namespace FlashQuizz.ViewModels
{
    public partial class SessionSummaryViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private string timeSpent;

        [ObservableProperty]
        private string hardestCard;

        [ObservableProperty]
        private string perfectCardsCount;

        [ObservableProperty]
        private double memorizationPercentage;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("SessionStats", out var statsObj) && statsObj is SessionStats stats)
            {
                TimeSpent = stats.TimeSpent;
                HardestCard = stats.HardestCard != null 
                    ? $"{stats.HardestCard.Question} -> {stats.HardestCard.Answer}"
                    : "Aucune carte difficile";
                PerfectCardsCount = $"{stats.PerfectCardsCount} cartes";
                MemorizationPercentage = Math.Round(stats.MemorizationPercentage, 1);
            }
        }

        [RelayCommand]
        private async Task ReturnToMenu()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
