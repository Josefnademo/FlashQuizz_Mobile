namespace FlashQuizz
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var navPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Colors.DeepSkyBlue,
                BarTextColor = Colors.White
            };

            MainPage = navPage;
        }
    }
}
