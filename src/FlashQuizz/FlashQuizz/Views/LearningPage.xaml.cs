using Microsoft.Maui.Devices.Sensors;
using FlashQuizz.ViewModels;
using FlashQuizz.Services;
using System.Text.Json;
using FlashQuizz.Models;

namespace FlashQuizz.Views
{
    public partial class LearningPage : ContentPage
    {
        private LearningViewModel _viewModel;

        public LearningPage()
        {
            InitializeComponent();
            var cardService = App.Current.Handler.MauiContext.Services.GetService<CardService>();
            _viewModel = new LearningViewModel(cardService);
            BindingContext = _viewModel;

            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            Accelerometer.Start(SensorSpeed.UI);
        }

        private async void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            await _viewModel.OnShakeDetected();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Accelerometer.ShakeDetected -= Accelerometer_ShakeDetected;
            Accelerometer.Stop();
        }
    }


}
/* private void InitializeViewModelFromNavigation()
        {
            if (BindingContext is LearningViewModel vm)
            {
                var navUri = Shell.Current.CurrentState.Location.OriginalString;
                if (NavigationUtil.TryGetParameter(navUri, "Cards", out List<FlashCard> cards))
                {
                    vm.Initialize(cards);
                }
            }
        }

        private void OnShakeDetected(object sender, EventArgs e)
        {
            if (BindingContext is LearningViewModel vm && vm.IsAnswerShown)
            {
                if (vm.DontKnowCardCommand.CanExecute(null))
                {
                    MainThread.BeginInvokeOnMainThread(() => vm.DontKnowCardCommand.Execute(null));
                }
            }
        }

        protected override void OnDisappearing()
        {
            Accelerometer.ShakeDetected -= OnShakeDetected;
            Accelerometer.Stop();
            base.OnDisappearing();
        }*/