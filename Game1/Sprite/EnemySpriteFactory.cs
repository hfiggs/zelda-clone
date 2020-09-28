using Game1.Sprite;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Enemy
{
    class EnemySpriteFactory
    {
        private Texture2D enemySpritesheet;

        private Texture2D spikeTrapSpritesheet;

        private Texture2D merchantSpritesheet;

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
            merchantSpritesheet = content.Load<Texture2D>("Merchant");
        }

        public ISprite CreateSkeletonSprite()
        {
            return new EnemySprite(enemySpritesheet, 14, 4, 15, 8, 2);
        }

        public ISprite CreateSpikeTrapSprite()
        {
            return new EnemySprite(spikeTrapSpritesheet, 0, 0, 1, 1, 1);
        }

        public ISprite CreateMerchantSprite()
        {
            return new EnemySprite(merchantSpritesheet, 0, 0, 1, 1, 0);
        }
    }
}
