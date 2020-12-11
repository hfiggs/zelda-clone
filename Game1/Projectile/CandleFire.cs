using System;
using Game1.Player;
using Game1.Player.PlayerInventory;
using Game1.Environment;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Audio;
using Game1.Util;

namespace Game1.Projectile
{
    class CandleFire : IProjectile
    {
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private const char north = 'N', south = 'S', west = 'W', east = 'E';
        private ISprite sprite;
        private Vector2 position;
        private const float maxMoveSpeed = 80;
        private float moveSpeed;
        private const float despawnTimerMax = 1;
        private float despawnTimer;
        private bool removeMe = false;
        private IPlayer player;

        public CandleFire(char direction, Vector2 position, IPlayer player)
        {
            this.direction = direction;
            this.position = position;
            this.player = player;

            moveSpeed = maxMoveSpeed;
            despawnTimer = despawnTimerMax;

            sprite = EnvironmentSpriteFactory.instance.CreateFire();

            AudioManager.PlayFireForget("flames");
        }

        public void Update(GameTime gameTime)
        {
            const int timeModifier = 100; // To put Seconds on the same scale as moveSpeed
            if (moveSpeed > 0)
                moveSpeed = Math.Max((moveSpeed - timeModifier * (float)gameTime.ElapsedGameTime.TotalSeconds), 0);

            if (direction == north)
            {
                position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (direction == south)
            {
                position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (direction == west)
            {
                position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (direction == east)
            {
                position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            // Despawn counter
            if (moveSpeed == 0 && despawnTimer > 0)
                despawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (moveSpeed == 0 && despawnTimer <= 0)
                this.BeginDespawn();

            sprite.Update();
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color, SpriteLayerUtil.topLayer);
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
            const int widthAndHeight = 16;
            Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, widthAndHeight, widthAndHeight);

            return hitbox;
        }

        public bool ShouldDelete()
        {
            return removeMe;
        }

        public void BeginDespawn()
        {
            removeMe = true;
        }

        public void EditPosition(Vector2 amount)
        {
            position = Vector2.Add(amount, position);
        }
    }
}
