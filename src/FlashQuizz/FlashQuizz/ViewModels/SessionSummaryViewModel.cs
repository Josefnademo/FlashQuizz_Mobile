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

namespace FlashQuizz.ViewModels
{
    public partial class SessionSummaryViewModel : ObservableObject
    {
        [ObservableProperty]
        private SessionStats _sessionStats;

        public string TimeSpent => SessionStats?.TimeSpent ?? "00:00:00";
        public string HardestCard => SessionStats?.HardestCard?.Question ?? "N/A";
        public int PerfectCardsCount => SessionStats?.PerfectCardsCount ?? 0;
        public double MemorizationPercentage => SessionStats?.MemorizationPercentage ?? 0;

        public SessionSummaryViewModel()
        {
            if (Shell.Current.CurrentState.Location.OriginalString.Contains("SessionStats"))
            {
                var statsParam = Shell.Current.CurrentState.Location.OriginalString.Split('=')[1];
                SessionStats = JsonSerializer.Deserialize<SessionStats>(Uri.UnescapeDataString(statsParam));
            }
        }

        [RelayCommand]
        private async Task ReturnToMenu()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
