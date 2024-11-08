using System.Collections.Generic;
using System.Linq;

namespace Main.Scripts.Game
{
    public class Deck
    {
        public int CardsCount => Cards.Count;
        
        public List<Card> Cards = new();

        public List<Card> DrawAll()
        {
            return Draw(Cards.Count);
        }
        
        public List<Card> Draw(int count)
        {
            var drawnCards = Cards.Take(count).ToList();
            Cards.RemoveRange(0, count);
            return drawnCards;
        }
        
        public void AddCard(List<Card> cards)
        {
            for (var i = 0; i < cards.Count; i++)
            {
                AddCard(cards[i]);
            }
        }
        
        public void AddCard(Card card)
        {
            Cards.Add(card);
        }
        
        public void RemoveCard(Card selectedCard)
        {
            Cards.Remove(selectedCard);
        }
        
        public Card Last()
        {
            if (CardsCount <= 0)
            {
                return null;
            }
            
            return Cards[^1];
        }
        
    }
}
