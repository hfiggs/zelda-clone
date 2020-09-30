﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Environment
{
    class Block : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;
        public Block(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createBlock();
            this.position = position;
        }

        public void BehaviorUpdate()
        {
            throw new NotImplementedException("For later collision mechanics");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, position, Color.White);
        }
    }
}
