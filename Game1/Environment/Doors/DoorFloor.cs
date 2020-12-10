using Game1.Sprite;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Util;
using Game1.Environment.EnvironmentUtil;

namespace Game1.Environment
{
    class DoorFloor : IEnvironment
    {
        private readonly ISprite sprite;
        private Vector2 position;

        private const int widthAndHeight = 16;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);
        private readonly List<Rectangle> hitboxes = new List<Rectangle>();

        public CompassDirection Direction { get; private set; }

        public DoorFloor(Vector2 position, CompassDirection direction)
        {
            sprite = DoorUtil.GetDoorFloorSprite(direction);

            this.position = Vector2.Add(position, DoorUtil.GetDoorFloorOffset(direction));
            Direction = direction;

            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
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
