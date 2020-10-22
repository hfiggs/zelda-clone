//Authors: Jared Perkins, Hunter Figgs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Sprite;
using System.Reflection;

namespace Game1.Player
{
    class PlayerSpriteFactory
    {
        private Texture2D linkUpSheet;
        private Texture2D linkItemSheet;
        private Texture2D linkDownSheet;
        private Texture2D linkRightSheet;
        private Texture2D linkLeftSheet;

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
            linkLeftSheet = content.Load<Texture2D>("Images/LinkLeftNew");
            linkRightSheet = content.Load<Texture2D>("Images/LinkRightNew");
            linkUpSheet = content.Load<Texture2D>("Images/LinkUpNew");
            linkItemSheet = content.Load<Texture2D>("Images/LinkItemNew");
            linkDownSheet = content.Load<Texture2D>("Images/LinkDownNew");
        }

        public ISprite CreateWalkLeftSprite()
        {
            return new PlayerSprite(linkLeftSheet, 4, 1, 0, 2);
        }

        public ISprite CreateWalkRightSprite()
        {
            return new PlayerSprite(linkRightSheet, 4, 1, 0, 2);
        }

        public ISprite CreateWalkDownSprite()
        {
            return new PlayerSprite(linkDownSheet, 4, 1, 0, 2);
        }

        public ISprite CreateWalkUpSprite()
        {
            return new PlayerSprite(linkUpSheet, 4, 1, 0, 2);
        }

        public ISprite CreateIdleLeftSprite()
        {
            return new PlayerSprite(linkLeftSheet, 4, 1, 0, 1);
        }

        public ISprite CreateIdleRightSprite()
        {
            return new PlayerSprite(linkRightSheet, 4, 1, 0, 1);
        }

        public ISprite CreateIdleUpSprite()
        {
            return new PlayerSprite(linkUpSheet, 4, 1, 0, 1);
        }

        public ISprite CreateIdleDownSprite()
        {
            return new PlayerSprite(linkDownSheet, 4, 1, 0, 1);
        }

        public ISprite CreateAttackLeftSprite()
        {
            return new PlayerSprite(linkLeftSheet, 4, 1, 0, 4);
        }

        public ISprite CreateAttackRightSprite()
        {
            return new PlayerSprite(linkRightSheet, 4, 1, 0, 4);
        }

        public ISprite CreateAttackUpSprite()
        {
            return new PlayerSprite(linkUpSheet, 4, 1, 0, 4);
        }

        public ISprite CreateAttackDownSprite()
        {
            return new PlayerSprite(linkDownSheet, 4, 1, 0, 4);
        }

        public ISprite CreateUseItemLeftSprite()
        {
            return new PlayerSprite(linkLeftSheet, 4, 1, 0, 3);
        }

        public ISprite CreateUseItemRightSprite()
        {
            return new PlayerSprite(linkRightSheet, 4, 1, 0, 3);
        }

        public ISprite CreateUseItemUpSprite()
        {
            return new PlayerSprite(linkUpSheet, 4, 1, 0, 3);
        }

        public ISprite CreateUseItemDownSprite()
        {
            return new PlayerSprite(linkDownSheet, 4, 1, 0, 3);
        }

        public ISprite CreateOneHandItemSprite()
        {
            return new PlayerSprite(linkItemSheet, 1, 2, 0, 1);
        }

        public ISprite CreateTwoHandItemSprite()
        {
            return new PlayerSprite(linkItemSheet, 1, 2, 1, 1);
        }
    }
}
