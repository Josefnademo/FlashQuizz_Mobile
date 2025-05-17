using System.Windows.Input;

namespace FlashQuizz.Views.Components
{
    public partial class GoHomeButton : ContentView
    {
        public GoHomeButton()
        {
            InitializeComponent();
            BindingContext = this;

            GoHomeCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("///MainPage");
            });
        }

        public ICommand GoHomeCommand { get; }
    }
}
