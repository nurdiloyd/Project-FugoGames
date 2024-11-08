using Main.Scripts.General;
using UnityEngine;

namespace Main.Scripts.Game
{
    public class GameManager : IContextUnit
    {
        public bool CanPlay => true;
        private bool HasMove => true;
        public BoardAssets BoardAssets { get; private set; }
        
        private GameUI _gameUI;
        private GameBoardController _gameBoardController;
        
        public void Bind()
        {
            BoardAssets = Resources.Load<BoardAssets>("BoardAssets");
            _gameBoardController = new GameBoardController();
        }
        
        public void SetGameUI(GameUI gameUI)
        {
            _gameUI = gameUI;
        }
        
        public void LoadLevel()
        {
            _gameBoardController.Clear();
            var dataManager = ContextController.Instance.DataManager;
            _gameBoardController.Init();
            _gameUI.SetLevelText(dataManager.User.levelIndex + 1);
        }
        
        public void SelectBlock()
        {
            _gameBoardController.SelectBlock();
        }
        
        public void DeselectBlock()
        {
            _gameBoardController.DeselectBlock();
        }
        
        public void MoveBlock()
        {
            var isMoved = _gameBoardController.TryMoveBlock();
            if (isMoved)
            {
                var isWin = !_gameBoardController.IsThereAnyBlock;
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
