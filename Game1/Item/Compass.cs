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
    class Compass : IItem
    {
        private ISprite sprite;

        private Vector2 position;

        public Compass(Vector2 position)
        {
            sprite = ItemSpriteFactory.Instance.CreateCompassSprite();

            this.position = position;
        }
        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, position, Color.White);
        }
    }
}
