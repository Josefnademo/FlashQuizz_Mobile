using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashQuizz.ViewModels
{
    public partial class Mvvm1ViewModel : ObservableObject
    {
        [ObservableProperty]
        private int counter = 0;

        [RelayCommand]
        private void Increment(int incrementValue)
        {
            Counter += incrementValue;
        }
    }
}
