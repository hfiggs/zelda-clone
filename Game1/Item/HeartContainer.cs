using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Item
{
    class HeartContainer : IItem
    {
        private ISprite sprite;
        public Vector2 Position { get; set; }
        public HeartContainer(Vector2 Position)
        {
            sprite = ItemSpriteFactory.Instance.CreateHeartContainerSprite();

            this.Position = Position;
        }
        public void Update(GameTime gameTime)
        {
            //Do Nothing
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, Position, color);
        }

        public Rectangle GetHitbox()
        {
            const int xAndYDiff = 10, widthAndHeight = 20;
            return new Rectangle((int)Position.X + xAndYDiff, (int)Position.Y + xAndYDiff, widthAndHeight, widthAndHeight);
        }

        public bool ShouldDelete { get; set; } = false;
    }
}
