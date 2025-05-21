using CommunityToolkit.Mvvm.Input;
using FlashQuizz.ViewModels;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Windows.Input;

namespace FlashQuizz.Views.Components
{
    public partial class GoHomeButton : ContentView
    {
        public ICommand GoHomeCommand { get; }

        public GoHomeButton()
        {
            InitializeComponent();
            GoHomeCommand = new AsyncRelayCommand(NavigateHomeAsync);
            BindingContext = this;
        }

        /// <summary>
        /// Navigates to the Begining page (MainPage).
        /// </summary>
        private async Task NavigateHomeAsync()
        {
            try
            {
                await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
            }
        }
    }

}