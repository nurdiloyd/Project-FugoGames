using Main.Scripts.General;
using UnityEngine;

namespace Main.Scripts.Game
{
    public class GameManager : IContextUnit
    {
        public bool CanPlay => true;
        public TableAssets TableAssets { get; private set; }
        
        private GameUI _gameUI;
        private TableController _tableController;
        
        public void Bind()
        {
            TableAssets = Resources.Load<TableAssets>("TableAssets");
            _tableController = new TableController();
        }
        
        public void SetGameUI(GameUI gameUI)
        {
            _gameUI = gameUI;
        }
        
        public void LoadLevel()
        {
            _tableController.Clear();
            
            var dataManager = ContextController.Instance.DataManager;
            _gameUI.SetLevelText(dataManager.User.levelIndex + 1);
            
            _tableController.Init();
            _tableController.StartGame();
        }
        
        public void RestartLevel()
        {
            if (!CanPlay)
            {
                return;
            }
            
            LoadLevel();
        }
    }
}
