﻿using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class DoorEOpen : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;
        public DoorEOpen(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorEOpen();
            this.position = position;
        }

        public void BehaviorUpdate()
        {
            throw new NotImplementedException("For later collision mechanics");
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }
    }
}
