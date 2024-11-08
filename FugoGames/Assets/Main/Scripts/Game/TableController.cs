using Main.Scripts.General;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Main.Scripts.Game
{
    public class TableController
    {
        public bool IsThereAnyCard => true;
        
        private GameManager _gameManager;
        private Table _table;
        private Transform _tableParent;
        
        public void Init()
        {
            _table = new Table();
            _gameManager = ContextController.Instance.GameManager;
            _tableParent = new GameObject("Table").transform;
            
            SpawnTableGround();
        }
        
        private void SpawnTableGround()
        {
            var boardGroundPrefab = _gameManager.TableAssets.boardGround;
            var boardGround = Object.Instantiate(boardGroundPrefab, _tableParent);
            boardGround.localPosition = new Vector3(0, -0.01f, 0f);
            boardGround.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(10, 10);
        }
        
        public void SelectCard()
        {
        }
        
        public void DeselectCard()
        {
        }
        
        public bool TryMoveCard()
        {
            return true;
        }
        
        public void Clear()
        {
            Object.Destroy(_tableParent?.gameObject);
            _tableParent = null;
            _table = null;
        }
    }
}
