using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class DoorSClosed : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;

        const int widthAndHeight = 32;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);
        private List<Rectangle> hitboxes = new List<Rectangle>();
        public int open; // 0 = locked, 1 = opening, 2 = open
        private float timeTillOpen;
        public DoorSClosed(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.CreateDoorSClosed();
            this.position = position;
            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
            open = 0;
            timeTillOpen = -1;
        }

public void Update(GameTime gameTime)
        {
            {
                if (open == 1)
                {
                    timeTillOpen -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (timeTillOpen <= 0)
                    {
                        sprite = EnvironmentSpriteFactory.instance.CreateDoorSOpen();
                        hitboxes.Remove(hitbox1);
                        open = 2;
                    }
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
    }
}
