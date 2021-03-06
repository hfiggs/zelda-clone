﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Item
{

	public class Clock : IItem
	{
        private ISprite sprite;

        public Vector2 Position { get; set; }

        public Clock(Vector2 Position)
		{
			sprite = ItemSpriteFactory.Instance.CreateClockSprite();
            this.Position = Position;
        }

        public void Update(GameTime gameTime)
        {
            //do nothing
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, Position, color);
        }

        public Rectangle GetHitbox()
        {
            const int xOffset = 14, yOffset = 11, width = 13, height = 18;
            return new Rectangle((int)Position.X + xOffset, (int)Position.Y + yOffset, width, height);
        }

        public bool ShouldDelete { get; set; } = false;
    }
}