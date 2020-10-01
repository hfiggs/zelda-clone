﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Item
{
	public class Bomb : IItem
	{
        private ISprite sprite;

        private Vector2 position;

		public Bomb(Vector2 position)
		{
            sprite = ItemSpriteFactory.Instance.CreateBombSprite();

            this.position = position;
		}

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public void Update(GameTime gameTime)
        {
            //Do Nothing
        }
    }
}