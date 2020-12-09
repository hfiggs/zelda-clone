using Game1.Sprite;
using Game1.Util;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Audio;
using Game1.Environment.EnvironmentUtil;

namespace Game1.Environment
{
    class DoorClosed : IEnvironment
    {
        private ISprite spriteBelow;
        private ISprite spriteAbove;
        private Vector2 position;

        private const int widthAndHeight = 32;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);
        private Rectangle hitboxOpen1;
        private Rectangle hitboxOpen2;
        private List<Rectangle> hitboxes = new List<Rectangle>();
        private float timeTillOpen;
        public int open; // 0 = locked, 1 = opening, 2 = open
        private const int openDoor = 2;
        private const float openTime = 250f;

        public CompassDirection direction;

        public DoorClosed(Vector2 position, CompassDirection direction)
        {
            this.position = position;
            this.direction = direction;

            DoorUtil.SetClosedDoorSprites(out spriteBelow, out spriteAbove, direction);

            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);

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
                    Open(true);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBelow.Draw(spriteBatch, position, color, SpriteLayerUtil.envBelowPlayerLayer2);
            spriteAbove.Draw(spriteBatch, position, color, SpriteLayerUtil.envAbovePlayerLayer);
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

                DoorUtil.SetOpenDoorSprites(out spriteBelow, out spriteAbove, direction);

                DoorUtil.SetOpenDoorHitboxes(out hitboxOpen1, out hitboxOpen2, direction, position);

                hitboxes = new List<Rectangle>()
                {
                    hitboxOpen1,
                    hitboxOpen2
                };
            }
        }
    }
}
