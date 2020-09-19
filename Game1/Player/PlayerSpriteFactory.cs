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

        public ISprite CreateWalkLeftSprite(bool damaged, Vector2 start, Vector2 end)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,1,2,start,end);
        }

        public ISprite CreateWalkRightSprite(bool damaged, Vector2 start, Vector2 end)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,3,2,start,end);
        }

        public ISprite CreateWalkDownSprite(bool damaged, Vector2 start, Vector2 end)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,0,2,start,end);
        }

        public ISprite CreateWalkUpSprite(bool damaged, Vector2 start, Vector2 end)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,2,2,start,end);
        }

        public ISprite CreateIdleLeftSprite(bool damaged, Vector2 start, Vector2 end)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,1,1,start,end);
        }

        public ISprite CreateIdleRightSprite(bool damaged, Vector2 start, Vector2 end)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,3,1,start,end);
        }

        public ISprite CreateIdleUpSprite(bool damaged, Vector2 start, Vector2 end)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,2,1,start,end);
        }

        public ISprite CreateIdleDownSprite(bool damaged, Vector2 start, Vector2 end)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,0,1,start,end);
        }

        public ISprite CreateAttackLeftSprite(bool damaged, Vector2 start, Vector2 end)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,1,4,start,end);
        }

        public ISprite CreateAttackRightSprite(bool damaged, Vector2 start, Vector2 end)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,3,4,start,end);
        }

        public ISprite CreateAttackUpSprite(bool damaged, Vector2 start, Vector2 end)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,2,4,start,end);
        }

        public ISprite CreateAttackDownSprite(bool damaged, Vector2 start, Vector2 end)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,0,4,start,end);
        }

        public ISprite CreateUseItemLeftSprite(bool damaged, Vector2 start, Vector2 end)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,1,3,start,end);
        }

        public ISprite CreateUseItemRightSprite(bool damaged, Vector2 start, Vector2 end)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,3,3,start,end);
        }

        public ISprite CreateUseItemUpSprite(bool damaged, Vector2 start, Vector2 end)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,2,3,start,end);
        }

        public ISprite CreateUseItemDownSprite(bool damaged, Vector2 start, Vector2 end)
        {
            return new PlayerSprite(linkSpritesheet,damaged,4,4,0,3,start,end);
        }

        public ISprite CreateOneHandItemSprite(Vector2 location)
        {
            return new PlayerSprite(linkItemSheet,false,1,2,0,1,location,location);
        }

        public ISprite CreateTwoHandItemSprite(Vector2 location)
        {
            return new PlayerSprite(linkItemSheet,false,1,2,1,1,location,location);
        }
    }
}
