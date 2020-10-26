﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Projectile
{
    class SwordBeam : IProjectile
    {
        private int columnModifier, counter, rowModifier;
        private float totalTime;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private const float moveSpeed = 250;
        private const float delay = 200; // miliseconds
        private ProjectileSpriteSheet sprite;
        private Vector2 position;
        private bool removeMe = false;

        public SwordBeam(char direction, Vector2 position)
        {
            this.direction = direction;
            this.position = position + new Vector2(-2, 3);
            sprite = ProjectileSpriteFactory.Instance.CreateSwordBeamSprite();
            columnModifier = 0;
            rowModifier = 0;
            counter = 0;
            totalTime = 0;
        }
        public void Update(GameTime gameTime)
        {
            totalTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (totalTime >= delay) {
                if (direction == 'N') {
                    position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    rowModifier = 0;
                } else if (direction == 'S') {
                    position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    rowModifier = 1;
                } else if (direction == 'W') {
                    position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    rowModifier = 2;
                } else if (direction == 'E') {
                    position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    rowModifier = 3;
                }

                // Used to change sprite sheet column and allow for flashing
                if (counter == 5) {
                    if (columnModifier == 0) {
                        columnModifier = 1;
                    } else {
                        columnModifier = 0;
                    }
                    counter = 0;
                } else {
                    counter++;
                }
            }

            if(position.Y < 10 || position.Y > 130 || position.X < 10 || position.X > 206)
            {
                BeginDespawn();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (totalTime >= delay) {
                int columnOfSprite = sprite.GetColumnOfSprite();
                Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite + columnModifier, rowModifier); ;
                Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);

                spriteBatch.Draw(sprite.GetTexture(), destinationRectangle, sourceRectangle, color);
            }
        }

        public override bool Equals(object obj)
        {
            return this.GetType().Equals(obj.GetType());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool ShouldDelete()
        {
            return removeMe;
        }

        public void BeginDespawn()
        {
           removeMe = true;
        }

        public Rectangle GetHitbox()
        {
            Rectangle hitbox;

            if (direction == 'N' || direction == 'S')
            {
                hitbox = new Rectangle((int)position.X + 17, (int)position.Y + 12, 7, 16);
            } else
            {
                hitbox = new Rectangle((int)position.X + 11, (int)position.Y + 16, 18, 7);
            }

            return hitbox;
        }
    }
}
