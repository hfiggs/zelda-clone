﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Game1.Item
{
    class RupeeYellow : IItem
    {
        private ISprite sprite;
        public Vector2 Position { get; set; }

        int timeTillSwap;
        bool frameChanged = true;
        const int timer = 250; //ms

        public RupeeYellow(Vector2 Position)
        {
            sprite = ItemSpriteFactory.Instance.CreateBlueRupeeSprite();

            this.Position = Position;

            timeTillSwap = timer;
        }
        public void Update(GameTime gameTime)
        {
            timeTillSwap -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeTillSwap <= 0)
            {
                if (frameChanged)
                    sprite = ItemSpriteFactory.Instance.CreateYellowRupeeSprite();
                else
                    sprite = ItemSpriteFactory.Instance.CreateBlueRupeeSprite();
                timeTillSwap = timer;
                frameChanged = !frameChanged;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, Position, color);
        }

        public Rectangle GetHitbox()
        {
            const int xOffset = 16, yOffset = 11, width = 10, height = 18;
            return new Rectangle((int)Position.X + xOffset, (int)Position.Y + yOffset, width, height);
        }

        public bool ShouldDelete { get; set; } = false;
    }
}