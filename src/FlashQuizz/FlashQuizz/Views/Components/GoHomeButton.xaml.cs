using CommunityToolkit.Mvvm.Input;
using FlashQuizz.ViewModels;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Windows.Input;

namespace FlashQuizz.Views.Components
{
    public partial class GoHomeButton : ContentView
    {
        // Конструктор по умолчанию для XAML
        public GoHomeButton()
        {
            InitializeComponent();
        }

        // Ваш кастомный конструктор для передачи ViewModel вручную
        public GoHomeButton(MainViewModel viewModel) : this()
        {
            BindingContext = viewModel;
        }
    }
}