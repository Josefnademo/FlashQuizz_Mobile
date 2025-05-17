namespace FlashQuizz.Models;

public class FlashCard : ContentPage
{
    public class FlashCard
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int TimesShown { get; set; }
        public int TimesCorrect { get; set; }

        public double SuccessRate => TimesShown == 0 ? 0 : (double)TimesCorrect / TimesShown;
    }
}