using Game1.Sprite;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class EnemySpriteFactory
    {
        private Texture2D enemySpritesheet;

        private Texture2D spikeTrapSpritesheet;

        private Texture2D oldManSpritesheet;

        private Texture2D merchantSpritesheet;

        private Texture2D batSpritesheet;

        private Texture2D snakeSpriteSheet;

        private static EnemySpriteFactory instance = new EnemySpriteFactory();


        public static EnemySpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private EnemySpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            enemySpritesheet = content.Load<Texture2D>("images/enemies");
            spikeTrapSpritesheet = content.Load<Texture2D>("images/spiketrap");
            oldManSpritesheet = content.Load<Texture2D>("images/oldman");
            merchantSpritesheet = content.Load<Texture2D>("Merchant");
            batSpritesheet = content.Load<Texture2D>("images/bat");
            snakeSpriteSheet = content.Load<Texture2D>("images/snake");
        }

        public ISprite CreateSkeletonSprite()
        {
            return new EnemySprite(enemySpritesheet, 14, 4, 15, 8, 2);
        }

        public ISprite CreateSpikeTrapSprite()
        {
            return new EnemySprite(spikeTrapSpritesheet, 0, 0, 1, 1, 1);
        }


        public ISprite CreateOldManSprite()
        {
            return new EnemySprite(oldManSpritesheet, 0, 0, 1, 1, 1);
        }

        public ISprite CreateMerchantSprite()
        {
            return new EnemySprite(merchantSpritesheet, 0, 0, 1, 1, 0);
        }

        public ISprite CreateJellySprite()
        {
            return new EnemySprite(enemySpritesheet, 14, 6, 15, 8, 2);
        }

        public ISprite CreateBatSprite()
        {
            return new EnemySprite(batSpritesheet, 0, 0, 1, 2, 2);
        }

        public ISprite CreateHandSprite()
        {
            return new EnemySprite(enemySpritesheet, 8, 0, 15, 8, 2);
        }

        public ISprite CreateGoriyaDownSprite()
        {
            return new EnemySprite(enemySpritesheet, 0, 2, 15, 8, 2);
        }

        public ISprite CreateGoriyaLeftSprite()
        {
            return new EnemySprite(enemySpritesheet, 1, 2, 15, 8, 2);
        }

        public ISprite CreateGoriyaRightSprite()
        {
            return new EnemySprite(enemySpritesheet, 3, 2, 15, 8, 2);
        }

        public ISprite CreateGoriyaUpSprite()
        {
            return new EnemySprite(enemySpritesheet, 2, 2, 15, 8, 2);
        }

        public ISprite CreateSnakeLeftSprite()
        {
            return new EnemySprite(snakeSpriteSheet, 1, 0, 2, 2, 2);
        }

        public ISprite CreateSnakeRightSprite()
        {
            return new EnemySprite(snakeSpriteSheet, 0, 0, 2, 2, 2);
        }
    }
}
