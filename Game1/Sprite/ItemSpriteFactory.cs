﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Sprite
{
    class ItemSpriteFactory
    {
        private Texture2D itemSpritesheet;

        private static ItemSpriteFactory instance = new ItemSpriteFactory();

        public static ItemSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private ItemSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            itemSpritesheet = content.Load<Texture2D>("images/items");
        }

        public ISprite CreateCompassSprite()
        {
            return new ItemSprite(itemSpritesheet, 2, 1);
        }

        public ISprite CreateKeySprite()
        {
            return new ItemSprite(itemSpritesheet, 7, 1);
        }

        public ISprite CreateMapSprite()
        {
            return new ItemSprite(itemSpritesheet, 6, 2);
        }

        public ISprite CreateHeartContainerSprite()
        {
            return new ItemSprite(itemSpritesheet, 6, 1);
        }

        public ISprite CreateBombSprite()
        {
            return new ItemSprite(itemSpritesheet, 5, 0);
        }

        public ISprite CreateTriforceSprite()
        {
            return new ItemSprite(itemSpritesheet, 8, 3);
        }

        public ISprite CreateHeartSprite()
        {
            return new ItemSprite(itemSpritesheet, 6, 3);
        }
    }
}
