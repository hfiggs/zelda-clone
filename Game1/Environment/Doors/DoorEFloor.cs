﻿using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class DoorEFloor : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;
        
        public DoorEFloor(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorEFloor();
            this.position = position + new Vector2(0.0f, 8.0f);
        }

        public void BehaviorUpdate(GameTime gameTime)
        {
            //throw new NotImplementedException("For later collision mechanics");
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }
    }
}
