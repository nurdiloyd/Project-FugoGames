using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Main.Scripts.Game
{
    public class Table
    {
        private readonly Random _random = new();
        public readonly Deck DeckRemaining;
        public readonly Deck DeckOnCenter;
        public readonly Player Player1;
        public readonly Player Player2;
        
        public Table()
        {
            DeckRemaining = new Deck();
            DeckOnCenter = new Deck();
            Player1 = new Player(false);
            Player2 = new Player(true);
            
            FillDeckRemaining();
            InitialDeal();
        }
        
        private void FillDeckRemaining()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    DeckRemaining.Cards.Add(new Card(suit, rank));
                }
            }
            
            DeckRemaining.Cards = DeckRemaining.Cards.OrderBy(_ => _random.Next()).ToList();
        }
        
        private void InitialDeal()
        {
            var cardsP1 = DeckRemaining.Draw(4);
            Player1.AddCardsToHand(cardsP1);
            
            var cardsP2 = DeckRemaining.Draw(4);
            Player2.AddCardsToHand(cardsP2);
            
            var cardsHidden = DeckRemaining.Draw(3); 
            DeckOnCenter.AddCard(cardsHidden);
            
            var cardVisible = DeckRemaining.Draw(1);
            DeckOnCenter.AddCard(cardVisible);
            
            Debug.Log($"Initial table card: {DeckOnCenter.Last()}");
        }
        
        public void StartGame()
        {
            while (DeckRemaining.CardsCount > 0 || Player1.DeckOnHand.CardsCount > 0)
            {
                PlayTurn(Player1);
                PlayTurn(Player2);
                DealNewCards();
            }
            
            EndGame();
        }
        
        private void PlayTurn(Player player)
        {
            var topCard = DeckOnCenter.Last();
            var playedCard = player.PlayCard(topCard);
            Debug.Log($"{(player.IsBot ? "Bot" : "You")} played: {playedCard}");
            
            if (playedCard.Rank == topCard.Rank)
            {
                // Collect all cards on a match
                Debug.Log($"{(player.IsBot ? "Bot" : "You")} collected the cards!");
                var collectedCards = DeckOnCenter.DrawAll();
                player.CollectCards(collectedCards);
            }
            else if (playedCard.Rank == Rank.Jack)
            {
                // Jack collects all cards regardless
                Debug.Log($"{(player.IsBot ? "Bot" : "You")} played a Jack and collected the cards!");
                var collectedCards = DeckOnCenter.DrawAll();
                player.CollectCards(collectedCards);
            }
            else
            {
                DeckOnCenter.AddCard(playedCard);
            }
        }
        
        private void DealNewCards()
        {
            if (DeckRemaining.CardsCount > 0 && Player1.DeckOnHand.CardsCount == 0 && Player2.DeckOnHand.CardsCount == 0)
            {
                var cardsP1 = DeckRemaining.Draw(4);
                Player1.AddCardsToHand(cardsP1);
            
                var cardsP2 = DeckRemaining.Draw(4);
                Player2.AddCardsToHand(cardsP2);
            }
        }
        
        private void EndGame()
        {
            Debug.Log("Game Over!");
            
            var player1Score = CalculateScore(Player1.DeckCollected.Cards);
            var player2Score = CalculateScore(Player2.DeckCollected.Cards);
            
            Debug.Log($"Your Score: {player1Score}");
            Debug.Log($"Bot's Score: {player2Score}");
            
            if (player1Score > player2Score)
            {
                Debug.Log("You Win!");
            }
            else if (player1Score < player2Score)
            {
                Debug.Log("Bot Wins!");
            }
            else
            {
                Debug.Log("It's a Tie!");
            }
        }
        
        private int CalculateScore(List<Card> cards)
        {
            var score = 0;
            
            foreach (var card in cards)
            {
                if (card.Rank == Rank.Ace)
                {
                    score += 1;
                }
                else if (card.Suit == Suit.Clubs && card.Rank == Rank.Two)
                {
                    score += 2;
                }
                else if (card.Suit == Suit.Diamonds && card.Rank == Rank.Ten)
                {
                    score += 3;
                }
                else if (card.Rank == Rank.Jack)
                {
                    score += 1;
                }
            }
            
            score += cards.Count > 26 ? 3 : 0;
            
            return score;
        }
    }
}
