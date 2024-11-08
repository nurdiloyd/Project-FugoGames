using System;
using System.Collections.Generic;
using System.Linq;

namespace Main.Scripts.Game
{
    public class Deck
    {
        private List<Card> _cards = new();
        private readonly Random _random = new();
        
        public Deck()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    _cards.Add(new Card(suit, rank));
                }
            }
        }
        
        public void Shuffle()
        {
            _cards = _cards.OrderBy(x => _random.Next()).ToList();
        }
        
        public List<Card> Draw(int count)
        {
            var drawnCards = _cards.Take(count).ToList();
            _cards.RemoveRange(0, count);
            return drawnCards;
        }

        public int CardsLeft()
        {
            return _cards.Count;
        }
    }
}
