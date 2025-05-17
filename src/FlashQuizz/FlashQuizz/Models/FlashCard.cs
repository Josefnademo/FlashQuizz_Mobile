using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashQuizz.Models
{
    public class FlashCard
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int TimesShown { get; set; }
        public int TimesCorrect { get; set; }

        public double SuccessRate => TimesShown == 0 ? 0 : (double)TimesCorrect / TimesShown;
    }
}
