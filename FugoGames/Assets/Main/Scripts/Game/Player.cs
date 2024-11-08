using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Main.Scripts.Game
{
    public class Player
    {
        public List<Card> Hand { get; private set; } = new();
        public List<Card> CollectedCards { get; private set; } = new();
        public bool IsBot { get; private set; }
        private readonly Random _random = new();

        public Player(bool isBot)
        {
            IsBot = isBot;
        }

        public void AddCardsToHand(List<Card> cards)
        {
            Hand.AddRange(cards);
        }

        public Card PlayCard(Card topCard)
        {
            Card selectedCard;

            if (IsBot)
            {
                var choice = _random.Next(Hand.Count);
                selectedCard = Hand[choice];
            }
            else
            {
                Debug.Log("Your hand:");
                for (int i = 0; i < Hand.Count; i++)
                {
                    Debug.Log($"{i + 1}: {Hand[i]}");
                }

                Debug.Log("Select a card to play (1-4): ");
                var choice = _random.Next(Hand.Count);
                selectedCard = Hand[choice];
            }

            Hand.Remove(selectedCard);
            return selectedCard;
        }

        public void CollectCards(List<Card> cards)
        {
            CollectedCards.AddRange(cards);
        }

        public int CalculateScore()
        {
            var score = 0;

            foreach (var card in CollectedCards)
            {
                if (card.Rank == Rank.Ace) score += 1;
                else if (card.Suit == Suit.Clubs && card.Rank == Rank.Two) score += 2;
                else if (card.Suit == Suit.Diamonds && card.Rank == Rank.Ten) score += 3;
                else if (card.Rank == Rank.Jack) score += 1;
            }

            score += CollectedCards.Count > 26 ? 3 : 0;
            return score;
        }
    }
}
