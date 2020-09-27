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
        ISprite mySprite;

        public HeartContainer()
        {
            mySprite = ItemSpriteFactory.Instance.CreateHeartContainerSprite();
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
