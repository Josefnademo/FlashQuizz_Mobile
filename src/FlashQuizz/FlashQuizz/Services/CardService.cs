using FlashQuizz.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace FlashQuizz.Services
{
    public class CardService
    {
        private readonly FlashCardDbContext _dbContext;

        public CardService(FlashCardDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public ObservableCollection<FlashCard> GetAllCards()
        {
            return new ObservableCollection<FlashCard>(_dbContext.FlashCards.ToList());
        }

        public void AddCard(FlashCard card)
        {
            _dbContext.FlashCards.Add(card);
            _dbContext.SaveChanges();
        }

        public void UpdateCard(FlashCard card)
        {
            _dbContext.FlashCards.Update(card);
            _dbContext.SaveChanges();
        }

        public void DeleteCard(FlashCard card)
        {
            _dbContext.FlashCards.Remove(card);
            _dbContext.SaveChanges();
        }
    }
}