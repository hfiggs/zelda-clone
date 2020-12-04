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
        private Texture2D linkPortalBlueSheet;
        private Texture2D linkPortalOrangeSheet;
        private Texture2D zeldaPortalBlueSheet;
        private Texture2D zeldaPortalOrangeSheet;
        private Texture2D linkDeadSheet;
        private Texture2D ZeldaUpSheet;
        private Texture2D ZeldaItemSheet;
        private Texture2D ZeldaDownSheet;
        private Texture2D ZeldaRightSheet;
        private Texture2D ZeldaLeftSheet;
        private Texture2D ZeldaDeadSheet;
        private const int playerSpriteColumns = 1, playerSpriteRows = 4, startingFrame = 0, walkingMaxRows = 2, idleMaxRows = 1, attackMaxRows = 4, useItemMaxRows = 3, playerDeadMaxRows = 4;
        private const int oneHandRows = 1, oneHandColumns = 2, oneHandMaxRows = 1, twoHandRows = 1, twoHandColumns = 2, twoHandStartingFrame = 1, twoHandMaxRows = 1;
        private const string leftSpriteFilePath = "Images/Player/LinkLeft", rightSpriteFilePath = "Images/Player/LinkRight", upSpriteFilePath = "Images/Player/LinkUp", downSpriteFilePath = "Images/Player/LinkDown", itemSpriteFilePath = "Images/Player/LinkItem", deadSpriteFilePath = "Images/Player/DeadLink";
        private const string linkPortalBluePath = "Images/Player/Portal/LinkPortalBlue", linkPortalOrangePath = "Images/Player/Portal/LinkPortalOrange", zeldaPortalBluePath = "Images/Player/Portal/ZeldaPortalBlue", zeldaPortalOrangePath = "Images/Player/Portal/ZeldaPortalOrange";
        private const string leftZeldaSpriteFilePath = "Images/Player/ZeldaLeft", rightZeldaSpriteFilePath = "Images/Player/ZeldaRight", upZeldaSpriteFilePath = "Images/Player/ZeldaUp", downZeldaSpriteFilePath = "Images/Player/ZeldaDown", itemZeldaSpriteFilePath = "Images/Player/ZeldaItem", deadZeldaSpriteFilePath = "Images/Player/DeadZelda";

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

            linkPortalBlueSheet = content.Load<Texture2D>(linkPortalBluePath);
            linkPortalOrangeSheet = content.Load<Texture2D>(linkPortalOrangePath);
            zeldaPortalBlueSheet = content.Load<Texture2D>(zeldaPortalBluePath);
            zeldaPortalOrangeSheet = content.Load<Texture2D>(zeldaPortalOrangePath);

            linkDeadSheet = content.Load<Texture2D>(deadSpriteFilePath);
            ZeldaLeftSheet = content.Load<Texture2D>(leftZeldaSpriteFilePath);
            ZeldaRightSheet = content.Load<Texture2D>(rightZeldaSpriteFilePath);
            ZeldaUpSheet = content.Load<Texture2D>(upZeldaSpriteFilePath);
            ZeldaItemSheet = content.Load<Texture2D>(itemZeldaSpriteFilePath);
            ZeldaDownSheet = content.Load<Texture2D>(downZeldaSpriteFilePath);
            ZeldaDeadSheet = content.Load<Texture2D>(deadZeldaSpriteFilePath);
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
        // TODO
        public ISprite CreateZeldaWalkLeftSprite()
        {
            return new PlayerSprite(ZeldaLeftSheet, playerSpriteRows, playerSpriteColumns, startingFrame, walkingMaxRows);
        }

        public ISprite CreateZeldaWalkRightSprite()
        {
            return new PlayerSprite(ZeldaRightSheet, playerSpriteRows, playerSpriteColumns, startingFrame, walkingMaxRows);
        }

        public ISprite CreateZeldaWalkDownSprite()
        {
            return new PlayerSprite(ZeldaDownSheet, playerSpriteRows, playerSpriteColumns, startingFrame, walkingMaxRows);
        }

        public ISprite CreateZeldaWalkUpSprite()
        {
            return new PlayerSprite(ZeldaUpSheet, playerSpriteRows, playerSpriteColumns, startingFrame, walkingMaxRows);
        }

        public ISprite CreateZeldaIdleLeftSprite()
        {
            return new PlayerSprite(ZeldaLeftSheet, playerSpriteRows, playerSpriteColumns, startingFrame, idleMaxRows);
        }

        public ISprite CreateZeldaIdleRightSprite()
        {
            return new PlayerSprite(ZeldaRightSheet, playerSpriteRows, playerSpriteColumns, startingFrame, idleMaxRows);
        }

        public ISprite CreateZeldaIdleUpSprite()
        {
            return new PlayerSprite(ZeldaUpSheet, playerSpriteRows, playerSpriteColumns, startingFrame, idleMaxRows);
        }

        public ISprite CreateZeldaIdleDownSprite()
        {
            return new PlayerSprite(ZeldaDownSheet, playerSpriteRows, playerSpriteColumns, startingFrame, idleMaxRows);
        }

        public ISprite CreateZeldaAttackLeftSprite()
        {
            return new PlayerSprite(ZeldaLeftSheet, playerSpriteRows, playerSpriteColumns, startingFrame, attackMaxRows);
        }

        public ISprite CreateZeldaAttackRightSprite()
        {
            return new PlayerSprite(ZeldaRightSheet, playerSpriteRows, playerSpriteColumns, startingFrame, attackMaxRows);
        }

        public ISprite CreateZeldaAttackUpSprite()
        {
            return new PlayerSprite(ZeldaUpSheet, playerSpriteRows, playerSpriteColumns, startingFrame, attackMaxRows);
        }

        public ISprite CreateZeldaAttackDownSprite()
        {
            return new PlayerSprite(ZeldaDownSheet, playerSpriteRows, playerSpriteColumns, startingFrame, attackMaxRows);
        }

        public ISprite CreateZeldaUseItemLeftSprite()
        {
            return new PlayerSprite(ZeldaLeftSheet, playerSpriteRows, playerSpriteColumns, startingFrame, useItemMaxRows);
        }

        public ISprite CreateZeldaUseItemRightSprite()
        {
            return new PlayerSprite(ZeldaRightSheet, playerSpriteRows, playerSpriteColumns, startingFrame, useItemMaxRows);
        }

        public ISprite CreateZeldaUseItemUpSprite()
        {
            return new PlayerSprite(ZeldaUpSheet, playerSpriteRows, playerSpriteColumns, startingFrame, useItemMaxRows);
        }

        public ISprite CreateZeldaUseItemDownSprite()
        {
            return new PlayerSprite(ZeldaDownSheet, playerSpriteRows, playerSpriteColumns, startingFrame, useItemMaxRows);
        }
        
        public ISprite CreateZeldaOneHandItemSprite()
        {
            return new PlayerSprite(ZeldaItemSheet, oneHandRows, oneHandColumns, startingFrame, oneHandMaxRows);
        }

        public ISprite CreateZeldaTwoHandItemSprite()
        {
            return new PlayerSprite(ZeldaItemSheet, twoHandRows, twoHandColumns, twoHandStartingFrame, twoHandMaxRows);
        }

        public ISprite CreateZeldaDeadSprite()
        {
            return new PlayerSprite(ZeldaDeadSheet, playerSpriteRows, playerSpriteColumns, startingFrame, playerDeadMaxRows);
        }

        #region Portal Sprites

        // Link Portal Blue

        public ISprite CreateLinkPortalBlueDownSprite()
        {
            return new PlayerSprite(linkPortalBlueSheet, 2, 4, 0, 2);
        }

        public ISprite CreateLinkPortalBlueLeftSprite()
        {
            return new PlayerSprite(linkPortalBlueSheet, 2, 4, 1, 2);
        }

        public ISprite CreateLinkPortalBlueRightSprite()
        {
            return new PlayerSprite(linkPortalBlueSheet, 2, 4, 2, 2);
        }

        public ISprite CreateLinkPortalBlueUpSprite()
        {
            return new PlayerSprite(linkPortalBlueSheet, 2, 4, 3, 2);
        }

        // Link Portal Orange

        public ISprite CreateLinkPortalOrangeDownSprite()
        {
            return new PlayerSprite(linkPortalOrangeSheet, 2, 4, 0, 2);
        }

        public ISprite CreateLinkPortalOrangeLeftSprite()
        {
            return new PlayerSprite(linkPortalOrangeSheet, 2, 4, 1, 2);
        }

        public ISprite CreateLinkPortalOrangeRightSprite()
        {
            return new PlayerSprite(linkPortalOrangeSheet, 2, 4, 2, 2);
        }

        public ISprite CreateLinkPortalOrangeUpSprite()
        {
            return new PlayerSprite(linkPortalOrangeSheet, 2, 4, 3, 2);
        }

        // Zelda Portal Blue

        public ISprite CreateZeldaPortalBlueDownSprite()
        {
            return new PlayerSprite(zeldaPortalBlueSheet, 2, 4, 0, 2);
        }

        public ISprite CreateZeldaPortalBlueLeftSprite()
        {
            return new PlayerSprite(zeldaPortalBlueSheet, 2, 4, 1, 2);
        }

        public ISprite CreateZeldaPortalBlueRightSprite()
        {
            return new PlayerSprite(zeldaPortalBlueSheet, 2, 4, 2, 2);
        }

        public ISprite CreateZeldaPortalBlueUpSprite()
        {
            return new PlayerSprite(zeldaPortalBlueSheet, 2, 4, 3, 2);
        }

        // Zelda Portal Orange

        public ISprite CreateZeldaPortalOrangeDownSprite()
        {
            return new PlayerSprite(zeldaPortalOrangeSheet, 2, 4, 0, 2);
        }

        public ISprite CreateZeldaPortalOrangeLeftSprite()
        {
            return new PlayerSprite(zeldaPortalOrangeSheet, 2, 4, 1, 2);
        }

        public ISprite CreateZeldaPortalOrangeRightSprite()
        {
            return new PlayerSprite(zeldaPortalOrangeSheet, 2, 4, 2, 2);
        }

        public ISprite CreateZeldaPortalOrangeUpSprite()
        {
            return new PlayerSprite(zeldaPortalOrangeSheet, 2, 4, 3, 2);
        }

        #endregion
    }
}
