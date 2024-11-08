using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Main.Scripts.Game
{
    public class Table
    {
        private readonly List<Card> _table = new();
        private readonly Deck _deck = new();
        private readonly Player _player1 = new(false);
        private readonly Player _player2 = new(true);
        
        public void StartGame()
        {
            _deck.Shuffle();
            InitialDeal();
            while (_deck.CardsLeft() > 0 || _player1.Hand.Count > 0)
            {
                PlayRound();
                DealNewCards();
            }

            EndGame();
        }
        
        private void InitialDeal()
        {
            _player1.AddCardsToHand(_deck.Draw(4));
            _player2.AddCardsToHand(_deck.Draw(4));
            _table.AddRange(_deck.Draw(3)); // 3 face-down cards
            _table.Add(_deck.Draw(1).First()); // 1 face-up card
            Debug.Log($"Initial table card: {_table.Last()}");
        }
        
        private void PlayRound()
        {
            PlayTurn(_player1);
            PlayTurn(_player2);
        }
        
        private void PlayTurn(Player player)
        {
            var topCard = _table.Last();
            var playedCard = player.PlayCard(topCard);
            Debug.Log($"{(player.IsBot ? "Bot" : "You")} played: {playedCard}");

            if (playedCard.Rank == topCard.Rank)
            {
                // Collect all cards on a match
                Debug.Log($"{(player.IsBot ? "Bot" : "You")} collected the cards!");
                player.CollectCards(_table);
                _table.Clear();
            }
            else if (playedCard.Rank == Rank.Jack)
            {
                // Jack collects all cards regardless
                Debug.Log($"{(player.IsBot ? "Bot" : "You")} played a Jack and collected the cards!");
                player.CollectCards(_table);
                _table.Clear();
            }
            else
            {
                _table.Add(playedCard);
            }
        }
        
        private void DealNewCards()
        {
            if (_deck.CardsLeft() > 0 && _player1.Hand.Count == 0 && _player2.Hand.Count == 0)
            {
                _player1.AddCardsToHand(_deck.Draw(4));
                _player2.AddCardsToHand(_deck.Draw(4));
            }
        }
        
        private void EndGame()
        {
            Debug.Log("Game Over!");
            var player1Score = _player1.CalculateScore();
            var player2Score = _player2.CalculateScore();
            
            Debug.Log($"Your Score: {player1Score}");
            Debug.Log($"Bot's Score: {player2Score}");

            if (player1Score > player2Score)
                Debug.Log("You Win!");
            else if (player1Score < player2Score)
                Debug.Log("Bot Wins!");
            else
                Debug.Log("It's a Tie!");
        }
    }
}
