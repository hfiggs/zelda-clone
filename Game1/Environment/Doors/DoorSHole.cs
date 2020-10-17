using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class DoorSHole : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;

        private Rectangle hitbox1 = new Rectangle(0, 0, 8, 32);
        private Rectangle hitbox2 = new Rectangle(24, 0, 8, 32);
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public DoorSHole(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorSHole();
            this.position = position;
            hitboxes.Add(hitbox1);
            hitboxes.Add(hitbox2);
        }

        public void BehaviorUpdate(GameTime gameTime)
        {
            //throw new NotImplementedException("For later collision mechanics");
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
