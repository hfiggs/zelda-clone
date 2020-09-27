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
        }

        public ISprite CreateSkeletonSprite()
        {
            return new EnemySprite(enemySpritesheet, 14, 4, 2);
        }
    }
}
