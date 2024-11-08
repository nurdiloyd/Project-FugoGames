using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Main.Scripts.Game
{
    public class Player
    {
        public Deck DeckOnHand { get; private set; } = new();
        public Deck DeckCollected { get; private set; } = new();
        public bool IsBot { get; private set; }
        private readonly Random _random = new();
        
        public Player(bool isBot)
        {
            IsBot = isBot;
        }
        
        public void AddCardsToHand(List<Card> cards)
        {
            DeckOnHand.AddCard(cards);
        }
        
        public Card PlayCard(Card topCard)
        {
            Card selectedCard;

            if (IsBot)
            {
                var choice = _random.Next(DeckOnHand.CardsCount);
                selectedCard = DeckOnHand.Cards[choice];
            }
            else
            {
                Debug.Log("Your hand:");
                for (int i = 0; i < DeckOnHand.CardsCount; i++)
                {
                    Debug.Log($"{i + 1}: {DeckOnHand.Cards[i]}");
                }

                Debug.Log("Select a card to play (1-4): ");
                var choice = _random.Next(DeckOnHand.CardsCount);
                selectedCard = DeckOnHand.Cards[choice];
            }
            
            DeckOnHand.RemoveCard(selectedCard);
            
            return selectedCard;
        }
        
        public void CollectCards(List<Card> cards)
        {
            DeckCollected.AddCard(cards);
        }
    }
}
