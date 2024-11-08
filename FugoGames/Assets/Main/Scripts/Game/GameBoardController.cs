using Main.Scripts.General;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Main.Scripts.Game
{
    public class GameBoardController
    {
        public bool IsThereAnyBlock => _board.IsThereAnyBlock;
        
        private GameManager _gameManager;
        private Board _board;
        private Transform _boardParent;
        
        public void Init()
        {
            _board = new Board();
            _gameManager = ContextController.Instance.GameManager;
            _boardParent = new GameObject("Table").transform;
            
            SpawnBoardGround();
        }
        
        private void SpawnBoardGround()
        {
            var boardGroundPrefab = _gameManager.BoardAssets.boardGround;
            var boardGround = Object.Instantiate(boardGroundPrefab, _boardParent);
            boardGround.position = new Vector3(0, -0.01f, 0f);
            var tiling = new Vector2(10, 10);
            boardGround.GetComponent<MeshRenderer>().material.mainTextureScale = tiling;
        }
        
        public void SelectBlock()
        {
        }
        
        public void DeselectBlock()
        {
        }
        
        public bool TryMoveBlock()
        {
            return true;
        }
        
        public void Clear()
        {
            Object.Destroy(_boardParent?.gameObject);
            _boardParent = null;
            _board = null;
        }
    }
}
