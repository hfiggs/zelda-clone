using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Item
{
	public class RupeeBlue : IItem
	{
        ISprite mySprite;

		public RupeeBlue()
		{
            mySprite = ItemSpriteFactory.Instance.CreateBlueRupeeSprite();
		}

        public void Update(GameTime gameTime)
        {
            //Do Nothing
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            mySprite.Draw(spriteBatch, position, Color.White);
        }
    }
}