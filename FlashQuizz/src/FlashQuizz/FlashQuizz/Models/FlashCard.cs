using System.ComponentModel.DataAnnotations;

namespace FlashQuizz.Models;

public class FlashCard
{
    [Key]
    public int Id { get; set; }

    public string Question { get; set; }
    public string Answer { get; set; }
    public int TimesShown { get; set; }
    public int TimesCorrect { get; set; }

    public double SuccessRate => TimesShown == 0 ? 0 : (double)TimesCorrect / TimesShown;
}
