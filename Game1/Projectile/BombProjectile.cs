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
        private const float detonationTotalTime = 70;
        private bool detonated, swallowed;
        private Vector2 position;

        private List<IParticle> particles;
        private bool particlesSpawned;

        private const int cloudOffset = 15; // pixels
        private const int spriteDiameter = 40; // pixels

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
            detonationTime = detonationTotalTime;
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
            if (timer > detonationTotalTime) {
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
            const float radius = spriteDiameter / 2f;
            return new Vector2(position.X + radius, position.Y + radius);
        }

        private void AddCloudParticles(List<IParticle> particles)
        {
            player.PlayerInventory.SetItemInUse(ItemEnum.Bomb, false);
            particles.Add(new Cloud(GetCenteredPosition()));
            particles.Add(new Cloud(new Vector2(GetCenteredPosition().X - cloudOffset, GetCenteredPosition().Y)));
            particles.Add(new Cloud(new Vector2(GetCenteredPosition().X - cloudOffset, GetCenteredPosition().Y - cloudOffset)));
            particles.Add(new Cloud(new Vector2(GetCenteredPosition().X, GetCenteredPosition().Y - cloudOffset)));
            particles.Add(new Cloud(new Vector2(GetCenteredPosition().X + cloudOffset, GetCenteredPosition().Y - cloudOffset)));
            particles.Add(new Cloud(new Vector2(GetCenteredPosition().X + cloudOffset, GetCenteredPosition().Y)));
            particles.Add(new Cloud(new Vector2(GetCenteredPosition().X + cloudOffset, GetCenteredPosition().Y + cloudOffset)));
            particles.Add(new Cloud(new Vector2(GetCenteredPosition().X, GetCenteredPosition().Y + cloudOffset)));
            particles.Add(new Cloud(new Vector2(GetCenteredPosition().X - cloudOffset, GetCenteredPosition().Y + cloudOffset)));
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
                const int explosionRadius = explosionDiameter / 2;
                hitbox = new Rectangle((int)GetCenteredPosition().X - explosionRadius, (int)GetCenteredPosition().Y - explosionRadius, explosionDiameter, explosionDiameter);
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
