using Main.Scripts.General;
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
            
            SpawnTableGround();
        }
        
        private void SpawnTableGround()
        {
            var tableAssets = ContextController.Instance.GameManager.TableAssets;
            var boardGroundPrefab = tableAssets.boardGround;
            var boardGround = Object.Instantiate(boardGroundPrefab, _tableParent);
            boardGround.localPosition = new Vector3(0, -0.01f, 0f);
            boardGround.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(10, 10);
        }
        
        public void StartGame()
        {
            _table.StartGame();
        }
        
        public void Clear()
        {
            Object.Destroy(_tableParent?.gameObject);
            _tableParent = null;
            _table = null;
        }
    }
}
