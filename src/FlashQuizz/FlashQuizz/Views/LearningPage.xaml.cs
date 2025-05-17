using Microsoft.Maui.Devices.Sensors;
using FlashQuizz.ViewModels;

namespace FlashQuizz.Views
{
    public partial class LearningPage : ContentPage
    {
        public LearningPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Accelerometer.ShakeDetected += OnShakeDetected;
            Accelerometer.Start(SensorSpeed.UI);
        }

        protected override void OnDisappearing()
        {
            Accelerometer.ShakeDetected -= OnShakeDetected;
            Accelerometer.Stop();
            base.OnDisappearing();
        }

        private void OnShakeDetected(object sender, EventArgs e)
        {
            if (BindingContext is LearningViewModel viewModel && !viewModel.IsQuestionShowing)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    viewModel.DontKnowCardCommand.Execute(null);
                });
            }
        }
    }
}