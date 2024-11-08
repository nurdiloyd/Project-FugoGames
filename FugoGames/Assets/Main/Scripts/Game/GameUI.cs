using Main.Scripts.General;
using TMPro;
using UnityEngine;

namespace Main.Scripts.Game
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        
        public void SetLevelText(int level)
        {
            levelText.text = $"Level {level}";
        }
        
        public void ShowLevelWinDialog()
        {
            var dialogPrefab = Resources.Load<LevelWinDialog>("LevelWinDialogPrefab");
            var dialog = Instantiate(dialogPrefab, transform);
            dialog.Show();
        }
        
        public void ShowLevelLoseDialog()
        {
            var dialogPrefab = Resources.Load<LevelLoseDialog>("LevelLoseDialogPrefab");
            var dialog = Instantiate(dialogPrefab, transform);
            dialog.Show();
        }
        
        public void RestartLevel()
        {
            ContextController.Instance.GameManager.RestartLevel();
        }
    }
}
