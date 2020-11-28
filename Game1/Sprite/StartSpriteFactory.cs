using Game1.Sprite;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class StartSpriteFactory
    {
        private Texture2D startBgSpriteSheet;
        private Texture2D options;
        private Texture2D cursorSheet;

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
            const string startBackgroundFilePath = "images/Start/StartBackground";
            const string optionsPath = "images/Start/TitleOptions";
            const string cursorSheetPath = "images/Projectile/projectiles";
            startBgSpriteSheet = content.Load<Texture2D>(startBackgroundFilePath);
            options = content.Load<Texture2D>(optionsPath);
            cursorSheet = content.Load<Texture2D>(cursorSheetPath);
        }

        public ISprite CreateStartBackgroundSprite()
        {
            return new StartSprite(startBgSpriteSheet, 0, 0, 1, 1, 1);
        }

        public ISprite CreateOption(int optionNum)
        {
            return new StartSprite(options, 0, optionNum, 1, 3, 1);
        }

        public ISprite CreateCursor()
        {
            return new StartSprite(cursorSheet, 2, 3, 6, 4, 2);
        }
    }
}
