using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Item
{
	public class Bomb : IItem
	{
        ISprite mySprite;
		public Bomb()
		{
            mySprite = ItemSpriteFactory.Instance.CreateBombSprite();
		}

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            mySprite.Draw(spriteBatch, position, Color.White);
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}