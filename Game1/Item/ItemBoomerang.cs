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

        private Vector2 position;

        public ItemBoomerang(Vector2 position)
        {
            sprite = ItemSpriteFactory.Instance.CreateBoomerangSprite();

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
    }
}
