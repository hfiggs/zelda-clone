﻿using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class DoorNClosed : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;
        public DoorNClosed(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorNClosed();
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
