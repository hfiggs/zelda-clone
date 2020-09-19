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
        private Texture2D linkSpritesheet;
        private Texture2D linkItemSheet;

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
            linkSpritesheet = content.Load<Texture2D>("Link");
            linkItemSheet = content.Load<Texture2D>("LinkItem");
        }

        public ISprite CreateWalkLeftSprite(bool damaged)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,1,2);
        }

        public ISprite CreateWalkRightSprite(bool damaged)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,3,2);
        }

        public ISprite CreateWalkDownSprite(bool damaged)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,0,2);
        }

        public ISprite CreateWalkUpSprite(bool damaged)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,2,2);
        }

        public ISprite CreateIdleLeftSprite(bool damaged)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,1,1);
        }

        public ISprite CreateIdleRightSprite(bool damaged)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,3,1);
        }

        public ISprite CreateIdleUpSprite(bool damaged)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,2,1);
        }

        public ISprite CreateIdleDownSprite(bool damaged)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,0,1);
        }

        public ISprite CreateAttackLeftSprite(bool damaged)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,1,4);
        }

        public ISprite CreateAttackRightSprite(bool damaged)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,3,4);
        }

        public ISprite CreateAttackUpSprite(bool damaged)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,2,4);
        }

        public ISprite CreateAttackDownSprite(bool damaged)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,0,4);
        }

        public ISprite CreateUseItemLeftSprite(bool damaged)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,1,3);
        }

        public ISprite CreateUseItemRightSprite(bool damaged)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,3,3);
        }

        public ISprite CreateUseItemUpSprite(bool damaged)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,2,3);
        }

        public ISprite CreateUseItemDownSprite(bool damaged)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,0,3);
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
