using Main.Scripts.General;
using UnityEngine;

namespace Main.Scripts.Game
{
    public class GameManager : IContextUnit
    {
        public bool CanPlay => true;
        private bool HasMove => true;
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
            _tableController.Init();
            _gameUI.SetLevelText(dataManager.User.levelIndex + 1);
        }
        
        public void SelectCard()
        {
            _tableController.SelectCard();
        }
        
        public void DeselectCard()
        {
            _tableController.DeselectCard();
        }
        
        public void MoveCard()
        {
            var isMoved = _tableController.TryMoveCard();
            if (isMoved)
            {
                var isWin = !_tableController.IsThereAnyCard;
                var isLose = !HasMove;
                if (isWin)
                {
                    ContextController.Instance.DataManager.IncreaseCurrentLevelIndex();
                    _gameUI.ShowLevelWinDialog();
                }
                else if (isLose)
                {
                    _gameUI.ShowLevelLoseDialog();
                }
            }
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
