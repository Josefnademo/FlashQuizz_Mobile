using FlashQuizz.Views;
using Microsoft.Maui.Controls;

namespace FlashQuizz
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(AddCardPage), typeof(AddCardPage));
            Routing.RegisterRoute(nameof(MyCardsPage), typeof(MyCardsPage));
            Routing.RegisterRoute(nameof(EditCardPage), typeof(EditCardPage));
            Routing.RegisterRoute(nameof(LearningPage), typeof(LearningPage));
            Routing.RegisterRoute(nameof(SessionSummaryPage), typeof(SessionSummaryPage));

        }
    }
}
