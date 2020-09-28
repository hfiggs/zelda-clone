﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Projectile
{
    class enemyBoomerang : IProjectile
    {
        private int xModifier, yModifier, counter, rowModifier;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private ProjectileSpriteSheet sprite;
        private bool returned;
        private Point position;

        public enemyBoomerang(char direction, Point position)
        {
            this.direction = direction;
            this.position = position;
            sprite = ProjectileSpriteFactory.Instance.CreateBoomerangSprite();
            xModifier = 0;
            yModifier = 0;
            rowModifier = 0;
            counter = 0;
            returned = false;

        }
        public void Update()
        {
            if (counter < 100) {
                if (direction == 'N') {
                    yModifier -= 2;
                } else if (direction == 'S') {
                    yModifier += 2;
                } else if (direction == 'W') {
                    xModifier -= 2;
                } else {
                    xModifier += 2;
                }
            } else if (counter < 200) {
                if (direction == 'N') {
                    yModifier += 2;
                } else if (direction == 'S') {
                    yModifier -= 2;
                } else if (direction == 'W') {
                    xModifier += 2;
                } else {
                    xModifier -= 2;
                }
            }

            counter++;

            // Stop drawing and updating position of boomerang if it has returned to its owner
            if (xModifier == 0 && yModifier == 0) {
                counter++;
                returned = true;
            }

            // Used to change sprite sheet row to allow for flashing
            if (counter % 5 == 0) { 
                if (rowModifier == 3) {
                    rowModifier = 0;
                } else {
                    rowModifier++;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!returned) {
                int columnOfSprite = sprite.GetColumnOfSprite();
                Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite, rowModifier);
                Rectangle destinationRectangle = new Rectangle(position.X + xModifier, position.Y + yModifier, sourceRectangle.Width, sourceRectangle.Height);
                spriteBatch.Draw(sprite.GetTexture(), destinationRectangle, sourceRectangle, Color.White);
            }
        }
    }
}
