using Game1.Player;
using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Projectile
{
    class Boomerang : IProjectile
    {
        private int rowModifier, counter;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private const char north = 'N', south = 'S', west = 'W', east = 'E';
        private ProjectileSpriteSheet sprite;
        private bool returned;
        public IPlayer Player { get; private set; }
        private Vector2 position;
        private const float moveSpeed = 100, playerYAdjust = 5f;
        private float totalElapsedGameTime;
        private const int two = 2;

        public Boomerang(char direction, IPlayer player) {
            this.direction = direction;
            this.Player = player;
            sprite = ProjectileSpriteFactory.Instance.CreateBoomerangSprite();
            //centers the boomerang at the center of the player's hitbox, -5 (playerYAdjust) in the y direction
            position.X = player.GetPlayerHitbox().X + (player.GetPlayerHitbox().Width / two) - (sprite.PickSprite(0, 0).Width / two);
            position.Y = player.GetPlayerHitbox().Y + (player.GetPlayerHitbox().Height / two) - (sprite.PickSprite(0, 0).Height / two) - playerYAdjust;
            rowModifier = 0;
            totalElapsedGameTime = 0;
            counter = 0;
            returned = false;
        }
        public void Update(GameTime gameTime) {
            totalElapsedGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (totalElapsedGameTime < 1) {
                if (direction == north) {
                    position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else if (direction == south) {
                    position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else if (direction == west) {
                    position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else if (direction == east) {
                    position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            } else if (!returned) {
                Rectangle currentLocation = sprite.PickSprite(0, 0);
                currentLocation.Location = new Point((int)position.X, (int)position.Y);
                Vector2 recievePoisition = new Vector2(Player.GetPlayerHitbox().X + (Player.GetPlayerHitbox().Width / two) - (sprite.PickSprite(0, 0).Width / two), Player.GetPlayerHitbox().Y + (Player.GetPlayerHitbox().Height / two) - (sprite.PickSprite(0, 0).Height / two) - playerYAdjust);

                Vector2 positionDiff = new Vector2(currentLocation.X, currentLocation.Y) - recievePoisition;
                //minimum range to "recieve" boomerang should be no less than 5 - see README
                returned =  positionDiff.Length() < playerYAdjust;
                positionDiff = Vector2.Normalize(positionDiff);
                position.X -= positionDiff.X * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                position.Y -= positionDiff.Y * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if(returned)
            {
                Player.PlayerInventory.SetItemInUse(ItemEnum.Boomerang, false);
            }

            // Used to change sprite sheet row to allow for flashing
            const int spriteChangeInterval = 5, rowMax = 3;

            if (counter % spriteChangeInterval == 0) {
                if (rowModifier == rowMax) {
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
            const int twoSeconds = 2;
            totalElapsedGameTime = twoSeconds;
        }

        public Rectangle GetHitbox()
        {
            const int xAndYDiff = 16, widthAndHeight = 8;
            return new Rectangle((int)position.X + xAndYDiff, (int)position.Y + xAndYDiff, widthAndHeight, widthAndHeight);
        }
    }
}
