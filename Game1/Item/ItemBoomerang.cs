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
    class ItemBoomerang : IItem
    {
        private ISprite sprite;
        public Vector2 Position { get; set; }
        public ItemBoomerang(Vector2 Position)
        {
            sprite = ItemSpriteFactory.Instance.CreateBoomerangSprite();

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
            const int xOffset = 17, yOffset = 15, width = 8, height = 10;
            return new Rectangle((int)Position.X + xOffset, (int)Position.Y + yOffset, width, height);
        }

        public bool ShouldDelete { get; set; } = false;
    }
}
