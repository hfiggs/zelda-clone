//Authors: Jared Perkins, Hunter Figgs

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game1.Sprite;

namespace Game1.Player
{
    class PlayerSpriteFactory
    {
        private Texture2D linkUpSheet;
        private Texture2D linkItemSheet;
        private Texture2D linkDownSheet;
        private Texture2D linkRightSheet;
        private Texture2D linkLeftSheet;
        private Texture2D linkDeadSheet;
        private const int playerSpriteColumns = 1, playerSpriteRows = 4, startingFrame = 0, walkingMaxRows = 2, idleMaxRows = 1, attackMaxRows = 4, useItemMaxRows = 3, playerDeadMaxRows = 4;
        private const int oneHandRows = 1, oneHandColumns = 2, oneHandMaxRows = 1, twoHandRows = 1, twoHandColumns = 2, twoHandStartingFrame = 1, twoHandMaxRows = 1;
        private const string leftSpriteFilePath = "Images/Player/LinkLeft", rightSpriteFilePath = "Images/Player/LinkRight", upSpriteFilePath = "Images/Player/LinkUp", downSpriteFilePath = "Images/Player/LinkDown", itemSpriteFilePath = "Images/Player/LinkItem", deadSpriteFilePath = "Images/Player/DeadLink";

        private static PlayerSpriteFactory instance = new PlayerSpriteFactory();

        public static PlayerSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private PlayerSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            linkLeftSheet = content.Load<Texture2D>(leftSpriteFilePath);
            linkRightSheet = content.Load<Texture2D>(rightSpriteFilePath);
            linkUpSheet = content.Load<Texture2D>(upSpriteFilePath);
            linkItemSheet = content.Load<Texture2D>(itemSpriteFilePath);
            linkDownSheet = content.Load<Texture2D>(downSpriteFilePath);
            linkDeadSheet = content.Load<Texture2D>(deadSpriteFilePath);
        }
        
        public ISprite CreateWalkLeftSprite()
        {
            return new PlayerSprite(linkLeftSheet, playerSpriteRows, playerSpriteColumns, startingFrame, walkingMaxRows);
        }

        public ISprite CreateWalkRightSprite()
        {
            return new PlayerSprite(linkRightSheet, playerSpriteRows, playerSpriteColumns, startingFrame, walkingMaxRows);
        }

        public ISprite CreateWalkDownSprite()
        {
            return new PlayerSprite(linkDownSheet, playerSpriteRows, playerSpriteColumns, startingFrame, walkingMaxRows);
        }

        public ISprite CreateWalkUpSprite()
        {
            return new PlayerSprite(linkUpSheet, playerSpriteRows, playerSpriteColumns, startingFrame, walkingMaxRows);
        }

        public ISprite CreateIdleLeftSprite()
        {
            return new PlayerSprite(linkLeftSheet, playerSpriteRows, playerSpriteColumns, startingFrame, idleMaxRows);
        }

        public ISprite CreateIdleRightSprite()
        {
            return new PlayerSprite(linkRightSheet, playerSpriteRows, playerSpriteColumns, startingFrame, idleMaxRows);
        }

        public ISprite CreateIdleUpSprite()
        {
            return new PlayerSprite(linkUpSheet, playerSpriteRows, playerSpriteColumns, startingFrame, idleMaxRows);
        }

        public ISprite CreateIdleDownSprite()
        {
            return new PlayerSprite(linkDownSheet, playerSpriteRows, playerSpriteColumns, startingFrame, idleMaxRows);
        }

        public ISprite CreateAttackLeftSprite()
        {
            return new PlayerSprite(linkLeftSheet, playerSpriteRows, playerSpriteColumns, startingFrame, attackMaxRows);
        }

        public ISprite CreateAttackRightSprite()
        {
            return new PlayerSprite(linkRightSheet, playerSpriteRows, playerSpriteColumns, startingFrame, attackMaxRows);
        }

        public ISprite CreateAttackUpSprite()
        {
            return new PlayerSprite(linkUpSheet, playerSpriteRows, playerSpriteColumns, startingFrame, attackMaxRows);
        }

        public ISprite CreateAttackDownSprite()
        {
            return new PlayerSprite(linkDownSheet, playerSpriteRows, playerSpriteColumns, startingFrame, attackMaxRows);
        }

        public ISprite CreateUseItemLeftSprite()
        {
            return new PlayerSprite(linkLeftSheet, playerSpriteRows, playerSpriteColumns, startingFrame, useItemMaxRows);
        }

        public ISprite CreateUseItemRightSprite()
        {
            return new PlayerSprite(linkRightSheet, playerSpriteRows, playerSpriteColumns, startingFrame, useItemMaxRows);
        }

        public ISprite CreateUseItemUpSprite()
        {
            return new PlayerSprite(linkUpSheet, playerSpriteRows, playerSpriteColumns, startingFrame, useItemMaxRows);
        }

        public ISprite CreateUseItemDownSprite()
        {
            return new PlayerSprite(linkDownSheet, playerSpriteRows, playerSpriteColumns, startingFrame, useItemMaxRows);
        }
        
        public ISprite CreateOneHandItemSprite()
        {
            return new PlayerSprite(linkItemSheet, oneHandRows, oneHandColumns, startingFrame, oneHandMaxRows);
        }

        public ISprite CreateTwoHandItemSprite()
        {
            return new PlayerSprite(linkItemSheet, twoHandRows, twoHandColumns, twoHandStartingFrame, twoHandMaxRows);
        }

        public ISprite CreateDeadSprite()
        {
            return new PlayerSprite(linkDeadSheet, playerSpriteRows, playerSpriteColumns, startingFrame, playerDeadMaxRows);
        }
    }
}
