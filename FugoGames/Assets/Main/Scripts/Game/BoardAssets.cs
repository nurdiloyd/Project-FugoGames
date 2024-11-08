using UnityEngine;

namespace Main.Scripts.Game
{
    [CreateAssetMenu]
    public class BoardAssets : ScriptableObject
    {
        public Transform boardGround;
        [SerializeField] private BlockView blockPrefab;
        [SerializeField] private Texture2D blockTextureBlueParallel;
        [SerializeField] private Color colorBlue;
        
        public Texture GetBlockTexture()
        {
            return 0 switch
            {
                _ => blockTextureBlueParallel
            };
        }
        
        public Color GetGateColor()
        {
            return 0 switch
            {
                _ => colorBlue
            };
        }
    }
}
