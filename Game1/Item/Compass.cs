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
        ISprite mySprite;

        public Compass()
        {
            mySprite = ItemSpriteFactory.Instance.CreateCompassSprite();
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            mySprite.Draw(spriteBatch, position, Color.White);
        }
    }
}
