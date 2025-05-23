using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System;

namespace FlashQuizz.ViewModels
{
    public class SessionSummaryViewModel : INotifyPropertyChanged
    {
        public int TotalItems { get; }
        public int ItemsCompleted { get; }
        public TimeSpan SessionDuration { get; }

        public string SummaryText => $"Vous avez appris {ItemsCompleted} sur {TotalItems} items.";
        public string DurationText => $"Durée de la session : {SessionDuration.Minutes} min {SessionDuration.Seconds} s";

        public ICommand BackToHomeCommand { get; }

        public SessionSummaryViewModel(int totalItems, int itemsCompleted, TimeSpan sessionDuration)
        {
            TotalItems = totalItems;
            ItemsCompleted = itemsCompleted;
            SessionDuration = sessionDuration;

            BackToHomeCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
