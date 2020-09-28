using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Item
{

	public class Clock : IItem
	{
		ISprite mySprite;
        int timeTillSwap;
        const int swapTimer = 250; //ms

        bool frameChanged;

		public Clock()
		{
			mySprite = ItemSpriteFactory.Instance.CreateClockSprite();
		}

        public void Update(GameTime gameTime)
        {
            timeTillSwap -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeTillSwap <= 0)
            {
                if (frameChanged)
                    mySprite = ItemSpriteFactory.Instance.CreateClockSprite();
                else
                    mySprite = ItemSpriteFactory.Instance.CreateClockSprite();
                timeTillSwap = swapTimer;
                frameChanged = !frameChanged;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            mySprite.Draw(spriteBatch, position, Color.White);
        }
    }
}