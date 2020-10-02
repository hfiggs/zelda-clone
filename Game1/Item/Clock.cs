using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Item
{

	public class Clock : IItem
	{
        private ISprite sprite;

        private Vector2 position;


        int timeTillSwap;
        const int swapTimer = 250; //ms

        bool frameChanged;

		public Clock(Vector2 position)
		{
			sprite = ItemSpriteFactory.Instance.CreateClockSprite();

            this.position = position;
        }

        public void Update(GameTime gameTime)
        {
            timeTillSwap -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeTillSwap <= 0)
            {
                if (frameChanged)
                    sprite = ItemSpriteFactory.Instance.CreateClockSprite();
                else
                    sprite = ItemSpriteFactory.Instance.CreateClockSprite();
                timeTillSwap = swapTimer;
                frameChanged = !frameChanged;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }
    }
}