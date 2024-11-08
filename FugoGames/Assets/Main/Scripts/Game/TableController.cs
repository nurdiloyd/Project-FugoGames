﻿using Main.Scripts.General;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Main.Scripts.Game
{
    public class TableController
    {
        private Table _table;
        private Transform _tableParent;
        
        public void Init()
        {
            _table = new Table();
            _tableParent = new GameObject("Table").transform;
            
            SpawnDeckRemaining();
            SpawnDeckOnCenter();
        }
        
        private void SpawnDeckRemaining()
        {
            var tableAssets = ContextController.Instance.GameManager.TableAssets;
            var cards = _table.DeckRemaining.Cards;
            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var pos = new Vector3(-2f, i * 0.1f, 0);
                var rot = Quaternion.Euler(90, 0, 0);
                var cardView = Object.Instantiate(tableAssets.cardPrefab, pos, rot, _tableParent);
                cardView.Init(card);
            }
        }
        
        private void SpawnDeckOnCenter()
        {
            var tableAssets = ContextController.Instance.GameManager.TableAssets;
            var cards = _table.DeckOnCenter.Cards;
            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var pos = new Vector3(0f, i * 0.1f, 0);
                var rot = Quaternion.Euler(90, 0, 0);
                var cardView = Object.Instantiate(tableAssets.cardPrefab, pos, rot, _tableParent);
                cardView.Init(card);
            }
        }
        
        public void StartGame()
        {
        }
        
        public void Clear()
        {
            Object.Destroy(_tableParent?.gameObject);
            _tableParent = null;
            _table = null;
        }
    }
}
