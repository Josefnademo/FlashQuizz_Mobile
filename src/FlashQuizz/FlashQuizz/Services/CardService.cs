using FlashQuizz.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace FlashQuizz.Services
{
    /// <summary>
    /// Service for managing flash cards in the database.
    /// Provides methods to get, add, update, and delete cards.
    /// </summary>
    public class CardService
    {
        private readonly FlashCardDbContext _dbContext;


        /// <summary>
        /// Initializes the CardService with the specified database context.
        /// Ensures the database is created.
        /// </summary>
        public CardService(FlashCardDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Retrieves all flash cards from the database.
        /// </summary>
        public async Task<List<FlashCard>> GetAllCardsAsync()
        {
            return await _dbContext.FlashCards.ToListAsync();
        }


        /// <summary>
        /// Adds a new flash card to the database.
        /// </summary>
        public void AddCard(FlashCard card)
        {
            _dbContext.FlashCards.Add(card);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Updates an existing flash card in the database.
        /// </summary>
        public void UpdateCard(FlashCard card)
        {
            _dbContext.FlashCards.Update(card);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes a flash card from the database.
        /// </summary>
        public void DeleteCard(FlashCard card)
        {
            _dbContext.FlashCards.Remove(card);
            _dbContext.SaveChanges();
        }
    }
}