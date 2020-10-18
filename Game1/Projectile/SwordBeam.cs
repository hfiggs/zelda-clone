﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Projectile
{
    class SwordBeam : IProjectile
    {
        private int columnModifier, counter, rowModifier;
        private float moveSpeed;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private ProjectileSpriteSheet sprite;
        private Vector2 position;

        public SwordBeam(char direction, Vector2 position)
        {
            this.direction = direction;
            this.position = position;
            sprite = ProjectileSpriteFactory.Instance.CreateSwordBeamSprite();
            moveSpeed = 500;
            columnModifier = 0;
            rowModifier = 0;
            counter = 0;
        }
        public bool Update(GameTime gameTime)
        {
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

            //for now just return false, able to return true in the future when this needs to be removed from the projectiles list in game.
            return false;
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            int columnOfSprite = sprite.GetColumnOfSprite();
            Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite + columnModifier, rowModifier); ;
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);

            spriteBatch.Draw(sprite.GetTexture(), destinationRectangle, sourceRectangle, color);
        }

        public override bool Equals(object obj)
        {
            return this.GetType().Equals(obj.GetType());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
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
