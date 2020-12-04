using Game1.Player;
using Game1.Player.PlayerInventory;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Projectile
{
    class Arrow : IProjectile
    {
        private int rowModifier;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private const char north = 'N', south = 'S', west = 'W', east = 'E';
        private ProjectileSpriteSheet sprite;
        private Vector2 position;
        private const float moveSpeed = 400;
        private bool removeMe = false;

        public IPlayer Player { get; private set; }

        public Arrow(char direction, Vector2 position, IPlayer player)
        {
            this.direction = direction;
            this.position = position;
            sprite = ProjectileSpriteFactory.Instance.CreateArrowSprite();

            Player = player;
        }
        public void Update(GameTime gameTime)
        {
            const int northSprite = 0, southSprite = 1, westSprite = 2, eastSprite = 3;

            if (direction == north) {
                position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                rowModifier = northSprite;
            } else if (direction == south) {
                position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                rowModifier = southSprite;
            } else if (direction == west) {
                position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                rowModifier = westSprite;
            } else if (direction == east) {
                position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                rowModifier = eastSprite;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            int columnOfSprite = sprite.GetColumnOfSprite();
            Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite, rowModifier);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);

            spriteBatch.Draw(sprite.GetTexture(), destinationRectangle, sourceRectangle, color, 0f, new Vector2(0f, 0f), SpriteEffects.None, SpriteLayerUtil.projectileLayer);
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

            if (direction == north || direction == south)
            {
                const int xdiff = 18, ydiff = 12, width = 5, height = 16;
                hitbox = new Rectangle((int)position.X + xdiff, (int)position.Y + ydiff, width, height);
            }
            else
            {
                const int xdiff = 11, ydiff = 17, width = 19, height = 5;
                hitbox = new Rectangle((int)position.X + xdiff, (int)position.Y + ydiff, width, height);
            }

            return hitbox;
        }

        public bool ShouldDelete()
        {
            return removeMe;
        }

        public void BeginDespawn()
        {
            removeMe = true;
            Player.PlayerInventory.SetItemInUse(ItemEnum.Bow, false);
        }

        public void EditPosition(Vector2 amount)
        {
            position = Vector2.Add(amount, position);
        }
    }
}
