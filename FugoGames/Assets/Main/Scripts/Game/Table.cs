using System;
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
            Player1 = new Player();
            Player2 = new Player();
            
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
            Debug.Log(DeckRemaining.CardsCount);
            var cardsP1 = DeckRemaining.Draw(4);
            Player1.GiveCards(cardsP1);
            
            var cardsP2 = DeckRemaining.Draw(4);
            Player2.GiveCards(cardsP2);
            
            var cards = DeckRemaining.Draw(4); 
            DeckOnCenter.AddCards(cards);
        }
        
        public void StartGame()
        {
            while (DeckRemaining.HasCards || Player1.HasCard || Player2.HasCard)
            {
                PlayTurn(Player1);
                PlayTurn(Player2);
                
                if (DeckRemaining.HasCards && !Player1.DeckOnHand.HasCards && !Player2.DeckOnHand.HasCards)
                {
                    var cardsP1 = DeckRemaining.Draw(4);
                    Player1.GiveCards(cardsP1);
                    
                    var cardsP2 = DeckRemaining.Draw(4);
                    Player2.GiveCards(cardsP2);
                }
            }
            
            var player1Score = CalculateScore(Player1, Player2.DeckCollected.CardsCount);
            var player2Score = CalculateScore(Player2, Player1.DeckCollected.CardsCount);
            
            Debug.Log("Game Over!");
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
        
        private void PlayTurn(Player player)
        {
            if (!player.HasCard)
            {
                return;
            }
            
            var topCard = DeckOnCenter.Last();
            var choice = _random.Next(player.DeckOnHand.CardsCount);
            var playedCard = player.DeckOnHand.Cards[choice];
            player.DeckOnHand.RemoveCard(playedCard);
            DeckOnCenter.AddCards(playedCard);
            
            if (topCard != null)
            {
                if (playedCard.Rank == topCard.Rank)
                {
                    if (DeckOnCenter.CardsCount == 2)
                    {
                        player.AddPisti();
                    }
                    
                    var collectedCards = DeckOnCenter.DrawAll();
                    player.CollectCards(collectedCards);
                }
                else if (playedCard.Rank == Rank.Jack)
                {
                    var collectedCards = DeckOnCenter.DrawAll();
                    player.CollectCards(collectedCards);
                }
            }
        }
        
        private int CalculateScore(Player player, int otherPlayerCardsCount)
        {
            var cards = player.DeckCollected.Cards;
            var score = 0;
            
            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
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
            
            score += cards.Count > otherPlayerCardsCount ? 3 : 0;
            score += player.PistiCount * 10;
            
            return score;
        }
    }
}
