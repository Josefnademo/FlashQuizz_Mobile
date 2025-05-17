namespace FlashQuizz.Views;

using FlashQuizz.ViewModels;

    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel) //Dependency Injection(via constructor), in a place of Directly instantiating a ViewModel in XAML"<ContentPage.BindingContext> <ViewModels:MainViewModel/> </ContentPage.BindingContext>"
    {
            InitializeComponent();
             BindingContext = viewModel;// Setting up ViewModel
        }

    }

