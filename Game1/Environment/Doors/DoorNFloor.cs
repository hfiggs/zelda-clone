using System;
using Game1.Sprite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    class DoorNFloor : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;

        private Rectangle hitbox1 = new Rectangle(0, 0, 16, 16);
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public DoorNFloor(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createDoorNFloor();
            this.position = position + new Vector2(8.0f, 17.0f);
            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
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
