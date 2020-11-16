﻿using Game1.Sprite;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Audio;

namespace Game1.Environment
{
    class DoorSLocked : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;

        private const int widthAndHeight = 32;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);
        private const int width = 8, height = 32, xDiff = 24;
        private Rectangle hitboxOpen1 = new Rectangle(0, 0, width, height);
        private Rectangle hitboxOpen2 = new Rectangle(xDiff, 0, width, height);
        private List<Rectangle> hitboxes = new List<Rectangle>();
        private float timeTillOpen;
        public int open; // 0 = locked, 1 = opening, 2 = open
        private const int openDoor = 2;
        private const float openTime = 250f;

        public DoorSLocked(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorSLocked();
            this.position = position;
            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
            timeTillOpen = -1;
            open = 0;

            hitboxOpen1.Location += position.ToPoint();
            hitboxOpen2.Location += position.ToPoint();
        }

        public void Update(GameTime gameTime)
        {
            if (open == 1)
            {
                timeTillOpen -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timeTillOpen <= 0)
                {
                    sprite = EnvironmentSpriteFactory.instance.createDoorSOpen();
                    const int opened = 2;
                    open = opened;

                    hitboxes = new List<Rectangle>()
                    {
                        hitboxOpen1,
                        hitboxOpen2
                    };
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

        public void Open(bool shouldInstantOpen)
        {
            // Normal unlock
            if (!shouldInstantOpen)
            {
                open = 1;
                timeTillOpen = openTime;
                AudioManager.PlayFireForget("doorLock");
            }
            // Instant unlock
            else
            {
                open = openDoor;
                timeTillOpen = 0;

                sprite = EnvironmentSpriteFactory.instance.createDoorSOpen();

                hitboxes = new List<Rectangle>()
                {
                    hitboxOpen1,
                    hitboxOpen2
                };
            }
        }
    }
}
