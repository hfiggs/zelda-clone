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

        const int widthAndHeight = 16;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public DoorNFloor(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.CreateDoorNFloor();
            const float x = 8f, y = 17f;
            this.position = position + new Vector2(x, y);
            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
        }

public void Update(GameTime gameTime)
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
