using Game1.Particle;
using Game1.Player;
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
        private bool detonated;
        private Vector2 position;

        private List<IParticle> particles;
        private bool particlesSpawned;

        private const int cloudOffset = 15; // pixels
        private const int spriteDiameter = 40; // pixels

        public BombProjectile(Vector2 position, IPlayer player)
        {
            this.position = position;
            detonated = false;
            detonationTime = 70;
            timer = 0;
            sprite = ProjectileSpriteFactory.Instance.CreateBombProjectileSprite();
            this.player = player;

            particles = new List<IParticle>();
            particlesSpawned = false;
        }

        public bool Update(GameTime gameTime)
        {
            timer += detonationTime * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > 70) {
                detonated = true;
                if(!particlesSpawned)
                {
                    AddCloudParticles(particles);
                    particlesSpawned = true;
                }
            }

            particles.RemoveAll(p => p.ShouldDelete());

            foreach (IParticle particle in particles)
            {
                particle.Update(gameTime);
            }

            return detonated && particles.Count == 0;
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
            return new Vector2(position.X + (spriteDiameter / 2f), position.Y + (spriteDiameter / 2f));
        }

        private void AddCloudParticles(List<IParticle> particles)
        {
            player.setItemUsable(3);
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
    }
}
