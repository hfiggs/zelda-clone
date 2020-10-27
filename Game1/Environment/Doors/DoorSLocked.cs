﻿using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D11;

namespace Game1.Environment
{
    class DoorSLocked : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;

        private Rectangle hitbox1 = new Rectangle(0, 0, 32, 32);
       // private Rectangle hitbox2 = new Rectangle(0, 22, 32, 10);
        private List<Rectangle> hitboxes = new List<Rectangle>();
        private float timeTillOpen;
        public int open; // 0 = locked, 1 = opening, 2 = open

        public DoorSLocked(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorSLocked();
            this.position = position;
            hitbox1.Location += position.ToPoint();
         //   hitbox2.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
         //   hitboxes.Add(hitbox2);
            timeTillOpen = -1;
            open = 0;
        }

        public void BehaviorUpdate(GameTime gameTime)
        {
            if (open == 1)
            {
                timeTillOpen -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timeTillOpen <= 0)
                {
                    sprite = EnvironmentSpriteFactory.instance.createDoorSOpen();
                    hitboxes.Remove(hitbox1);
                    open = 2;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public List<Rectangle> GetHitboxes()
        {
            return hitboxes;
        }

        public void Open()
        {
            open = 1;
            timeTillOpen = 250f;
        }
    }
}
