﻿using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class DoorWLocked : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;
        private const int widthAndHeight = 32;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);
        //private Rectangle hitbox2 = new Rectangle(0, 0, 10, 32);
        private List<Rectangle> hitboxes = new List<Rectangle>();
        private float timeTillOpen;
        public int open; // 0 = locked, 1 = opening, 2 = open

        public DoorWLocked(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorWLocked();
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
                    sprite = EnvironmentSpriteFactory.instance.createDoorWOpen();
                    hitboxes.Remove(hitbox1);
                    const int opened = 2;
                    open = opened;
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
            const float openingTimeDelay = 250f;
            timeTillOpen = openingTimeDelay;
            AudioManager.PlayFireForget("doorLock");
        }
    }
}
