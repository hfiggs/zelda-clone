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

        public ISprite CreateWalkLeftSprite(bool damaged)
        {
            return new PlayerSprite(linkLeftSheet,damaged,4,1,0,2);
        }

        public ISprite CreateWalkRightSprite(bool damaged)
        {
            return new PlayerSprite(linkRightSheet,false,4,1,0,2);
        }

        public ISprite CreateWalkDownSprite(bool damaged)
        {
            return new PlayerSprite(linkDownSheet, false, 4, 1,0,2);
        }

        public ISprite CreateWalkUpSprite(bool damaged)
        {
            return new PlayerSprite(linkUpSheet,damaged,4,1,0,2);
        }

        public ISprite CreateIdleLeftSprite(bool damaged)
        {
            return new PlayerSprite(linkLeftSheet,damaged,4,1,0,1);
        }

        public ISprite CreateIdleRightSprite(bool damaged)
        {
            return new PlayerSprite(linkRightSheet,damaged,4,1,0,1);
        }

        public ISprite CreateIdleUpSprite(bool damaged)
        {
            return new PlayerSprite(linkUpSheet,damaged,4,1,0,1);
        }

        public ISprite CreateIdleDownSprite(bool damaged)
        {
            return new PlayerSprite(linkDownSheet,damaged,4,1,0,1);
        }

        public ISprite CreateAttackLeftSprite(bool damaged)
        {
            return new PlayerSprite(linkLeftSheet, damaged, 4, 1, 0, 4);
        }

        public ISprite CreateAttackRightSprite(bool damaged)
        {
            return new PlayerSprite(linkRightSheet,damaged,4,1,0,4);
        }

        public ISprite CreateAttackUpSprite(bool damaged)
        {
            return new PlayerSprite(linkUpSheet,damaged,4,1,0,4);
        }

        public ISprite CreateAttackDownSprite(bool damaged)
        {
            return new PlayerSprite(linkDownSheet,damaged,4,1,0,4);
        }

        public ISprite CreateUseItemLeftSprite(bool damaged)
        {
            return new PlayerSprite(linkLeftSheet,damaged,4,1,0,3);
        }

        public ISprite CreateUseItemRightSprite(bool damaged)
        {
            return new PlayerSprite(linkRightSheet,damaged,4,1,0,3);
        }

        public ISprite CreateUseItemUpSprite(bool damaged)
        {
            return new PlayerSprite(linkUpSheet,damaged,4,1,0,3);
        }

        public ISprite CreateUseItemDownSprite(bool damaged)
        {
            return new PlayerSprite(linkDownSheet,damaged,4,1,0,3);
        }

        public ISprite CreateOneHandItemSprite()
        {
            return new PlayerSprite(linkItemSheet,false,1,2,0,1);
        }

        public ISprite CreateTwoHandItemSprite()
        {
            return new PlayerSprite(linkItemSheet,false,1,2,1,1);
        }
    }
}
