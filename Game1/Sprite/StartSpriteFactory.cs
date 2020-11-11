using Game1.Sprite;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class StartSpriteFactory
    {
        private Texture2D startBgSpriteSheet;

        private static StartSpriteFactory instance = new StartSpriteFactory();

        public static StartSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private StartSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            startBgSpriteSheet = content.Load<Texture2D>("images/Start/StartBackground");
        }

        public ISprite CreateStartBackgroundSprite()
        {
            return new StartSprite(startBgSpriteSheet, 0, 0, 1, 1, 1);
        }
    }
}
