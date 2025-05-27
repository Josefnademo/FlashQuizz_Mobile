using Microsoft.Maui.Devices.Sensors;
using FlashQuizz.ViewModels;
using FlashQuizz.Services;
using System.Text.Json;
using FlashQuizz.Models;
using Microsoft.Maui.Controls.Shapes;

namespace FlashQuizz.Views
{
    public partial class LearningPage : ContentPage
    {
        private LearningViewModel _viewModel;
        private const uint AnimationDuration = 500;

        public LearningPage()
        {
            InitializeComponent();
            var cardService = App.Current.Handler.MauiContext.Services.GetService<CardService>();
            _viewModel = new LearningViewModel(cardService);
            BindingContext = _viewModel;

            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            Accelerometer.Start(SensorSpeed.UI);
        }

        private async void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LearningViewModel.IsAnswerShown))
            {
                if (_viewModel.IsAnswerShown)
                {
                    await FlipToBack();
                }
                else
                {
                    await FlipToFront();
                }
            }
        }

        private async Task FlipToBack()
        {
            // Start with front card visible and back card invisible
            FrontCard.IsVisible = true;
            BackCard.IsVisible = true;
            BackCard.Opacity = 0;

            // Animate front card out
            await FrontCard.RotateYTo(90, AnimationDuration / 2);
            FrontCard.IsVisible = false;

            // Animate back card in
            BackCard.RotationY = -90;
            await Task.WhenAll(
                BackCard.RotateYTo(0, AnimationDuration / 2),
                BackCard.FadeTo(1, AnimationDuration / 2)
            );
        }

        private async Task FlipToFront()
        {
            // Start with back card visible and front card invisible
            BackCard.IsVisible = true;
            FrontCard.IsVisible = true;
            FrontCard.Opacity = 0;

            // Animate back card out
            await BackCard.RotateYTo(-90, AnimationDuration / 2);
            BackCard.IsVisible = false;

            // Animate front card in
            FrontCard.RotationY = 90;
            await Task.WhenAll(
                FrontCard.RotateYTo(0, AnimationDuration / 2),
                FrontCard.FadeTo(1, AnimationDuration / 2)
            );
        }

        private async void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            await _viewModel.OnShakeDetected();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
            Accelerometer.ShakeDetected -= Accelerometer_ShakeDetected;
            Accelerometer.Stop();
        }
    }


}