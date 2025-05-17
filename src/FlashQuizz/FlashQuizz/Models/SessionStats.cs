using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FlashQuizz.Models
{
    public class SessionStats
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<FlashCard> CardsStudied { get; set; } = new();
        public List<FlashCard> DifficultCards { get; set; } = new();

        public string TimeSpent => (EndTime - StartTime).ToString(@"hh\:mm\:ss");
        public FlashCard HardestCard => DifficultCards
            .GroupBy(c => c)
            .OrderByDescending(g => g.Count())
            .FirstOrDefault()?.Key;

        public int PerfectCardsCount => CardsStudied
            .Count(c => c.TimesShown > 0 && c.TimesCorrect == c.TimesShown);

        public double MemorizationPercentage => CardsStudied.Count == 0 ? 0 :
            (double)PerfectCardsCount / CardsStudied.Count * 100;
    }
}