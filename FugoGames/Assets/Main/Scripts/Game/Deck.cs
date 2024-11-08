using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Main.Scripts.Game
{
    public class Deck
    {
        public bool HasCards => CardsCount > 0;
        public int CardsCount => Cards.Count;
        
        public List<Card> Cards = new();

        public List<Card> DrawAll()
        {
            return Draw(Cards.Count);
        }
        
        public List<Card> Draw(int count)
        {
            var cardsCount = Math.Min(count, Cards.Count);
            if (cardsCount <= 0)
            {
                return new List<Card>();
            }
            
            var drawnCards = Cards.Take(cardsCount).ToList();
            Cards.RemoveRange(0, count);
            
            return drawnCards;
        }
        
        public void AddCards(List<Card> cards)
        {
            for (var i = 0; i < cards.Count; i++)
            {
                AddCards(cards[i]);
            }
        }
        
        public void AddCards(Card card)
        {
            Cards.Add(card);
        }
        
        public void RemoveCard(Card selectedCard)
        {
            Cards.Remove(selectedCard);
        }
        
        [CanBeNull]
        public Card Last()
        {
            return !HasCards ? null : Cards[^1];
        }
        
    }
}
