namespace Main.Scripts.Game
{
    public class Card
    {
        public Suit Suit { get; private set; }
        public Rank Rank { get; private set; }
        
        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }
        
        public override string ToString()
        {
            return $"{(int)Rank}\n{Suit}";
        }
    }
    
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }
    
    public enum Rank
    {
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
    }
}
