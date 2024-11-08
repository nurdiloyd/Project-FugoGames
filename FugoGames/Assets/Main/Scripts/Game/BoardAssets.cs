using UnityEngine;

namespace Main.Scripts.Game
{
    [CreateAssetMenu]
    public class BoardAssets : ScriptableObject
    {
        public Transform boardGround;
        public GateView gatePrefab;
        [SerializeField] private BlockView block1Prefab;
        [SerializeField] private BlockView block2Prefab;
        [SerializeField] private Texture2D block1TextureBlueParallel;
        [SerializeField] private Texture2D block1TextureBlueUp;
        [SerializeField] private Texture2D block1TextureGreenParallel;
        [SerializeField] private Texture2D block1TextureGreenUp;
        [SerializeField] private Texture2D block1TexturePurpleParallel;
        [SerializeField] private Texture2D block1TexturePurpleUp;
        [SerializeField] private Texture2D block1TextureRedParallel;
        [SerializeField] private Texture2D block1TextureRedUp;
        [SerializeField] private Texture2D block1TextureYellowParallel;
        [SerializeField] private Texture2D block1TextureYellowUp;
        [SerializeField] private Texture2D block2TextureBlueParallel;
        [SerializeField] private Texture2D block2TextureBlueUp;
        [SerializeField] private Texture2D block2TextureGreenParallel;
        [SerializeField] private Texture2D block2TextureGreenUp;
        [SerializeField] private Texture2D block2TexturePurpleParallel;
        [SerializeField] private Texture2D block2TexturePurpleUp;
        [SerializeField] private Texture2D block2TextureRedParallel;
        [SerializeField] private Texture2D block2TextureRedUp;
        [SerializeField] private Texture2D block2TextureYellowParallel;
        [SerializeField] private Texture2D block2TextureYellowUp;
        [SerializeField] private Color colorBlue;
        [SerializeField] private Color colorGreen;
        [SerializeField] private Color colorPurple;
        [SerializeField] private Color colorRed;
        [SerializeField] private Color colorYellow;
        
        public BlockView GetBlockPrefab(int length)
        {
            return length switch
            {
                1 => block1Prefab,
                2 => block2Prefab,
                _ => block1Prefab,
            };
        }
        
        public Texture GetBlockTexture(int length, BlockColor color, bool isHorizontal)
        {
            return length switch
            {
                1 => color switch
                {
                    BlockColor.Red => isHorizontal ? block1TextureRedParallel : block1TextureRedUp,
                    BlockColor.Green => isHorizontal ? block1TextureGreenParallel : block1TextureGreenUp,
                    BlockColor.Blue => isHorizontal ? block1TextureBlueParallel : block1TextureBlueUp,
                    BlockColor.Yellow => isHorizontal ? block1TextureYellowParallel : block1TextureYellowUp,
                    BlockColor.Purple => isHorizontal ? block1TexturePurpleParallel : block1TexturePurpleUp,
                    _ => null
                },
                2 => color switch
                {
                    BlockColor.Red => isHorizontal ? block2TextureRedParallel : block2TextureRedUp,
                    BlockColor.Green => isHorizontal ? block2TextureGreenParallel : block2TextureGreenUp,
                    BlockColor.Blue => isHorizontal ? block2TextureBlueParallel : block2TextureBlueUp,
                    BlockColor.Yellow => isHorizontal ? block2TextureYellowParallel : block2TextureYellowUp,
                    BlockColor.Purple => isHorizontal ? block2TexturePurpleParallel : block2TexturePurpleUp,
                    _ => null
                },
                _ => null
            };
        }
        
        public Color GetGateColor(BlockColor color)
        {
            return color switch
            {
                BlockColor.Red => colorRed,
                BlockColor.Green => colorGreen,
                BlockColor.Blue => colorBlue,
                BlockColor.Yellow => colorYellow,
                BlockColor.Purple => colorPurple,
                _ => Color.white
            };
        }
    }
}
