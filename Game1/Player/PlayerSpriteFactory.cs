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
            linkLeftSheet = content.Load<Texture2D>("Link Left");
            linkRightSheet = content.Load<Texture2D>("Link Right");
            linkUpSheet = content.Load<Texture2D>("Link Up");
            linkItemSheet = content.Load<Texture2D>("LinkItem");
            linkDownSheet = content.Load<Texture2D>("Link Down");
        }

        public ISprite CreateWalkLeftSprite(Color color)
        {
            return new PlayerSprite(linkLeftSheet,color,4,1,0,2);
        }

        public ISprite CreateWalkRightSprite(Color color)
        {
            return new PlayerSprite(linkRightSheet, color,4,1,0,2);
        }

        public ISprite CreateWalkDownSprite(Color color)
        {
            return new PlayerSprite(linkDownSheet, color, 4, 1,0,2);
        }

        public ISprite CreateWalkUpSprite(Color color)
        {
            return new PlayerSprite(linkUpSheet,color,4,1,0,2);
        }

        public ISprite CreateIdleLeftSprite(Color color)
        {
            return new PlayerSprite(linkLeftSheet,color,4,1,0,1);
        }

        public ISprite CreateIdleRightSprite(Color color)
        {
            return new PlayerSprite(linkRightSheet,color,4,1,0,1);
        }

        public ISprite CreateIdleUpSprite(Color color)
        {
            return new PlayerSprite(linkUpSheet,color,4,1,0,1);
        }

        public ISprite CreateIdleDownSprite(Color color)
        {
            return new PlayerSprite(linkDownSheet,color,4,1,0,1);
        }

        public ISprite CreateAttackLeftSprite(Color color)
        {
            return new PlayerSprite(linkLeftSheet, color, 4, 1, 0, 4);
        }

        public ISprite CreateAttackRightSprite(Color color)
        {
            return new PlayerSprite(linkRightSheet,color,4,1,0,4);
        }

        public ISprite CreateAttackUpSprite(Color color)
        {
            return new PlayerSprite(linkUpSheet,color,4,1,0,4);
        }

        public ISprite CreateAttackDownSprite(Color color)
        {
            return new PlayerSprite(linkDownSheet,color,4,1,0,4);
        }

        public ISprite CreateUseItemLeftSprite(Color color)
        {
            return new PlayerSprite(linkLeftSheet,color,4,1,0,3);
        }

        public ISprite CreateUseItemRightSprite(Color color)
        {
            return new PlayerSprite(linkRightSheet,color,4,1,0,3);
        }

        public ISprite CreateUseItemUpSprite(Color color)
        {
            return new PlayerSprite(linkUpSheet,color,4,1,0,3);
        }

        public ISprite CreateUseItemDownSprite(Color color)
        {
            return new PlayerSprite(linkDownSheet,color,4,1,0,3);
        }

        public ISprite CreateOneHandItemSprite(Color color)
        {
            return new PlayerSprite(linkItemSheet,color,1,2,0,1);
        }

        public ISprite CreateTwoHandItemSprite(Color color)
        {
            return new PlayerSprite(linkItemSheet,color,1,2,1,1);
        }
    }
}
