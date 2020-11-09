using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class DoorNLocked : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;
        private Rectangle hitbox1 = new Rectangle(0, 0, 32, 32);
      //  private Rectangle hitbox2 = new Rectangle(0, 0, 32, 10);
        private List<Rectangle> hitboxes = new List<Rectangle>();
        private float timeTillOpen;
        public int open; // 0 = locked, 1 = opening, 2 = open

        public DoorNLocked(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorNLocked();
            this.position = position;
            hitbox1.Location += position.ToPoint();
         //   hitbox2.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
        //    hitboxes.Add(hitbox2);
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
                    sprite = EnvironmentSpriteFactory.instance.createDoorNOpen();
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
            AudioManager.PlayFireForget("doorLock");
        }
    }
}
