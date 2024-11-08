using System.Collections.Generic;

namespace Main.Scripts.Game
{
    public class Player
    {
        public bool HasCard => DeckOnHand.HasCards;
        public Deck DeckOnHand { get; }
        public Deck DeckCollected { get; }
        public int PistiCount;
        
        public Player()
        {
            DeckOnHand = new Deck();
            DeckCollected = new Deck();
        }
        
        public void GiveCards(List<Card> cards)
        {
            DeckOnHand.AddCards(cards);
        }
        
        public void CollectCards(List<Card> cards)
        {
            DeckCollected.AddCards(cards);
        }
        
        public void AddPisti()
        {
            PistiCount++;
        }
    }
}
