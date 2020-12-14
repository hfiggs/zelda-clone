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

        private List<Rectangle> hitboxes = new List<Rectangle>();

        public DoorBlank(Vector2 position, CompassDirection direction)
        {
            this.position = position;

            sprite = DoorUtil.GetBlankDoorSprite(direction);

            hitboxes.Add(DoorUtil.GetBlankDoorHitbox(direction, position));
        }

        public void Update(GameTime gameTime) { }

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
