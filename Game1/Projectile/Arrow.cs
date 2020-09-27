﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Projectile
{
    class Arrow : IProjectile
    {
        private int modifier;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private ProjectileSpriteSheet sprite;

        public Arrow(char direction)
        {
            this.direction = direction;
            sprite = ProjectileSpriteFactory.Instance.CreateArrowSprite();
            modifier = 0;
        }
        public void Update()
        {
            if (direction == 'N' || direction == 'W') {
                modifier -= 5;
            } else {
                modifier += 5;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle sourceRectangle;
            Rectangle destinationRectangle;
            int columnOfSprite = sprite.GetColumnOfSprite();

            switch (direction)
            {
                case 'N':
                    sourceRectangle = sprite.PickSprite(columnOfSprite, 0);
                    destinationRectangle = new Rectangle((int)position.X, (int)position.Y + modifier, sourceRectangle.Width, sourceRectangle.Height);
                    break;
                case 'S':
                    sourceRectangle = sprite.PickSprite(columnOfSprite, 1);
                    destinationRectangle = new Rectangle((int)position.X, (int)position.Y + modifier, sourceRectangle.Width, sourceRectangle.Height);
                    break;
                case 'W':
                    sourceRectangle = sprite.PickSprite(columnOfSprite, 2);
                    destinationRectangle = new Rectangle((int)position.X + modifier, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);
                    break;
                case 'E':
                    sourceRectangle = sprite.PickSprite(columnOfSprite, 3);
                    destinationRectangle = new Rectangle((int)position.X + modifier, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);
                    break;
                default: // Shouldn't occur
                    sourceRectangle = sprite.PickSprite(columnOfSprite, 3);
                    destinationRectangle = new Rectangle(0, 0, sourceRectangle.Width, sourceRectangle.Height);
                    break;
            }

            spriteBatch.Draw(sprite.GetTexture(), destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
