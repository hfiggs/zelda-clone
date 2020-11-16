using Game1.Sprite;
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
        // private Rectangle hitbox2 = new Rectangle(0, 22, 32, 10);
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
         //   hitbox2.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
         //   hitboxes.Add(hitbox2);
            timeTillOpen = -1;
            open = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (open == 1)
            {
                timeTillOpen -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timeTillOpen <= 0)
                {
                    sprite = EnvironmentSpriteFactory.instance.createDoorSOpen();
                    hitboxes.Remove(hitbox1);
                    const int setToOpen = 2;
                    open = setToOpen;
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
            const string doorLockAudio = "doorLock";

            // Normal unlock
            if (!shouldInstantOpen)
            {
                open = 1;
                timeTillOpen = openTime;
                AudioManager.PlayFireForget(doorLockAudio);
            }
            // Instant unlock
            else
            {
                open = openDoor;
                timeTillOpen = 0;

                sprite = EnvironmentSpriteFactory.instance.createDoorSOpen();
                hitboxes.Remove(hitbox1);
            }
        }
    }
}
