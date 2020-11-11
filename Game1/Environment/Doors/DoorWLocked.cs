using Game1.Sprite;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Audio;

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
        private const int openDoor = 2;
        private const float openTime = 250f;

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

                sprite = EnvironmentSpriteFactory.instance.createDoorWOpen();
                hitboxes.Remove(hitbox1);
            }
        }
    }
}
