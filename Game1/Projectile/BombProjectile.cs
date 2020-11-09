using Game1.Particle;
using Game1.Player;
using Game1.Player.PlayerInventory;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Projectile
{
    class BombProjectile : IProjectile
    {
        private ISprite sprite;
        private IPlayer player;

        private float detonationTime, timer;
        private bool detonated, swallowed;
        private Vector2 position;

        private List<IParticle> particles;
        private bool particlesSpawned;

        private const int cloudOffset = 15; // pixels
        private const int cloudHexOffset = 8; //pixels
        private const int spriteDiameter = 40; // pixels

        private const float centralOffset = spriteDiameter / 3.0f;

        private int timeUntilNoExplosionHitbox;
        private const int explosionHitboxTime = 100; // ms

        private const int explosionDiameter = 24; // pixels

        private const float bombPlaceDelay = 0.5f; //sec
        private const float bombExplodeDelay = 1.5f; //sec

        public BombProjectile(Vector2 position, IPlayer player)
        {
            this.position = position;
            detonated = false;
            swallowed = false;
            detonationTime = 70;
            timer = 0;
            sprite = ProjectileSpriteFactory.Instance.CreateBombProjectileSprite();
            this.player = player;

            particles = new List<IParticle>();
            particlesSpawned = false;

            AudioManager.PlayFireForget("bombPlace", bombPlaceDelay);
            AudioManager.PlayFireForget("bombExplode", bombExplodeDelay);
        }

        public void Update(GameTime gameTime)
        {
            timer += detonationTime * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > 70) {
                detonated = true;
                if(!particlesSpawned)
                {
                    AddCloudParticles(particles);
                    particlesSpawned = true;
                }

                timeUntilNoExplosionHitbox = explosionHitboxTime;
            }

            if(detonated)
            {
                timeUntilNoExplosionHitbox -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            particles.RemoveAll(p => (detonated && particles.Count == 0));

            foreach (IParticle particle in particles)
            {
                particle.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (!detonated) {
                sprite.Draw(spriteBatch, position, color);
            }

            foreach (IParticle particle in particles)
            {
                particle.Draw(spriteBatch, color);
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

        private Vector2 GetCenteredPosition()
        {
            return new Vector2(position.X + centralOffset, position.Y + centralOffset);
        }

        private void AddCloudParticles(List<IParticle> particles)
        {
            player.PlayerInventory.SetItemInUse(ItemEnum.Bomb, false);
            particles.Add(new Cloud(GetCenteredPosition(), true));
            particles.Add(new Cloud(GetCenteredPosition(), false));
            particles.Add(new Cloud(new Vector2(GetCenteredPosition().X - cloudOffset, GetCenteredPosition().Y - cloudHexOffset)));
            particles.Add(new Cloud(new Vector2(GetCenteredPosition().X, GetCenteredPosition().Y - cloudOffset)));
            particles.Add(new Cloud(new Vector2(GetCenteredPosition().X + cloudOffset, GetCenteredPosition().Y - cloudHexOffset)));
            particles.Add(new Cloud(new Vector2(GetCenteredPosition().X + cloudOffset, GetCenteredPosition().Y + cloudOffset - cloudHexOffset + 1)));
            particles.Add(new Cloud(new Vector2(GetCenteredPosition().X, GetCenteredPosition().Y + cloudOffset)));
            particles.Add(new Cloud(new Vector2(GetCenteredPosition().X - cloudOffset, GetCenteredPosition().Y + cloudOffset - cloudHexOffset + 1)));
        }

        public Rectangle GetHitbox()
        {
            Rectangle hitbox;
            const int xAndYDiff = 14;
            const int width = 12;
            const int height = 16;
            if(!detonated) {
                hitbox = new Rectangle((int)position.X + xAndYDiff, (int)position.Y + xAndYDiff, width, height);
            } else {
                hitbox = new Rectangle((int)GetCenteredPosition().X - explosionDiameter/2, (int)GetCenteredPosition().Y - explosionDiameter / 2, explosionDiameter, explosionDiameter);
            }

            return hitbox;
        }

        public bool ShouldDelete()
        {
            return (detonated && particles.Count == 0) || swallowed;
        }

        public void BeginDespawn()
        {
            swallowed = true;
            player.PlayerInventory.SetItemInUse(ItemEnum.Bomb, false);
        }
    }
}
