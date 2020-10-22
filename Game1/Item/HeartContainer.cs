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

        private Vector2 position;

        public HeartContainer(Vector2 position)
        {
            sprite = ItemSpriteFactory.Instance.CreateHeartContainerSprite();

            this.position = position;
        }
        public void Update(GameTime gameTime)
        {
            //Do Nothing
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)position.X + 10, (int)position.Y + 10, 20, 20);
        }
    }
}
