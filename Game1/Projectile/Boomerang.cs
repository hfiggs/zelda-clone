using Game1.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Projectile
{
    class Boomerang : IProjectile
    {
        private int rowModifier, counter;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private ProjectileSpriteSheet sprite;
        private bool returned;
        public IPlayer Player { get; private set; }
        private Vector2 position;
        private float moveSpeed, totalElapsedGameTime;

        public Boomerang(char direction, IPlayer player) {
            this.direction = direction;
            this.player = player;
            sprite = ProjectileSpriteFactory.Instance.CreateBoomerangSprite();
            //centers the boomerang at the center of the player's hitbox, -5 in the y direction
            position.X = player.GetPlayerHitbox().X + (player.GetPlayerHitbox().Width / 2) - (sprite.PickSprite(0, 0).Width / 2);
            position.Y = player.GetPlayerHitbox().Y + (player.GetPlayerHitbox().Height / 2) - (sprite.PickSprite(0, 0).Height / 2) - 5.0f;
            rowModifier = 0;
            moveSpeed = 200;
            totalElapsedGameTime = 0;
            counter = 0;
            returned = false;
        }
        public void Update(GameTime gameTime) {
            totalElapsedGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (totalElapsedGameTime < 1) {
                if (direction == 'N') {
                    position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else if (direction == 'S') {
                    position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else if (direction == 'W') {
                    position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else {
                    position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            } else if (!returned) {
                Rectangle currentLocation = sprite.PickSprite(0, 0);
                currentLocation.Location = new Point((int)position.X, (int)position.Y);
                Vector2 recievePoisition = new Vector2(player.GetPlayerHitbox().X + (player.GetPlayerHitbox().Width / 2) - (sprite.PickSprite(0, 0).Width / 2), player.GetPlayerHitbox().Y + (player.GetPlayerHitbox().Height / 2) - (sprite.PickSprite(0, 0).Height / 2) - 5.0f);

                Vector2 positionDiff = new Vector2(currentLocation.X, currentLocation.Y) - recievePoisition;
                //minimum range to "recieve" boomerang should be no less than 5 - see README
                returned =  positionDiff.Length() < 5.0f;
                positionDiff = Vector2.Normalize(positionDiff);
                position.X -= positionDiff.X * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                position.Y -= positionDiff.Y * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if(returned)
            {
                Player.setItemUsable(2);
            }

            // Used to change sprite sheet row to allow for flashing
            if (counter % 5 == 0) {
                if (rowModifier == 3) {
                    rowModifier = 0;
                } else {
                    rowModifier++;
                }
            }

            counter++;
        }
        public void Draw(SpriteBatch spriteBatch, Color color) {
            if (!returned) {
                int columnOfSprite = sprite.GetColumnOfSprite();
                Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite, rowModifier);
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
            return returned;
        }

        public void BeginDespawn()
        {
            totalElapsedGameTime = 2;
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)position.X + 16, (int)position.Y + 16, 8, 8);
        }
    }
}
