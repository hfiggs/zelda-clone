using Game1.Sprite;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Environment.EnvironmentUtil;
using Game1.Util;

namespace Game1.Environment
{
    class DoorBlank : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;

        const int widthAndHeight = 32;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public DoorBlank(Vector2 position, CompassDirection direction)
        {
            this.position = position;

            sprite = DoorUtil.GetBlankDoorSprite(direction);
            
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
