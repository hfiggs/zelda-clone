﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1.Environment
{
    class Black : IEnvironment
    {
        private ISprite sprite;
        Vector2 position;

        const int widthAndHeight = 16;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public Black(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.CreateBlack();
            this.position = position;
            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
        }

public void Update(GameTime gameTime)
        {
            //throw new NotImplementedException("For later collision mechanics");
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public List<Rectangle> GetHitboxes()
        {
            return hitboxes;
        }
    }
}
