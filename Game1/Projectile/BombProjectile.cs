﻿using Game1.Audio;
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

        private float timer;
        private bool detonated, swallowed;
        private Vector2 position;

        private List<IParticle> particles;
        private bool particlesSpawned;

        private const int undetonatedHitboxXOffset = 13; // pixels
        private const int undetonatedHitboxYOffset = 17; // pixels
        private const int undetonatedHitboxDim = 16; // pixels

        private const int detonatedHitboxXOffset = 10; // pixels
        private const int detonatedHitboxYOffset = 12; // pixels
        private const int detonatedHitboxDim = 24; // pixels

        private const int cloudOffset = 15; // pixels
        private const int cloudHexOffset = 8; //pixels
        private const int spriteRadius = 12; // pixels

        private const float detonationTime = 70;
        private const float particleTime = 600f; // ms
        private float particleTimer;

        private const float bombPlaceDelay = 0.5f; //sec
        private const float bombExplodeDelay = 1.5f; //sec

        public BombProjectile(Vector2 position, IPlayer player)
        {
            this.position = position;
            detonated = false;
            swallowed = false;
            timer = 0;
            sprite = ProjectileSpriteFactory.Instance.CreateBombProjectileSprite();
            this.player = player;

            particles = new List<IParticle>();
            particlesSpawned = false;
            particleTimer = particleTime;

            const string bombPlacingAudio = "bombPlace", bombExplodingAudio = "bombExplode";
            AudioManager.PlayFireForget(bombPlacingAudio, bombPlaceDelay);
            AudioManager.PlayFireForget(bombExplodingAudio, bombExplodeDelay);
        }

        public void Update(GameTime gameTime)
        {
            timer += detonationTime * (float)gameTime.ElapsedGameTime.TotalSeconds;
            const int timeOfDetonation = 70;
            if (timer > timeOfDetonation) {
                detonated = true;
                if(!particlesSpawned)
                {
                    AddCloudParticles(particles);
                    particlesSpawned = true;
                }
            }

            if(detonated)
            {
                particleTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            particles.RemoveAll(p => (particlesSpawned && particleTimer <= 0));

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
            return new Vector2(position.X + spriteRadius, position.Y + spriteRadius);
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
            const float x = 0.75f, y = 0.535f, z = 0.535f, w = 0.4f;
            particles.Add(new BombOverlay(new Color(new Vector4(x, y, z, w))));
        }

        public Rectangle GetHitbox()
        {
            Rectangle hitbox;
            if(!detonated) {
                hitbox = new Rectangle((int)position.X + undetonatedHitboxXOffset, (int)position.Y + undetonatedHitboxYOffset, undetonatedHitboxDim, undetonatedHitboxDim);
            } else {
                hitbox = new Rectangle((int)position.X + detonatedHitboxXOffset, (int)position.Y + detonatedHitboxYOffset, detonatedHitboxDim, detonatedHitboxDim);
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

        public void EditPosition(Vector2 amount)
        {
            position = Vector2.Add(amount, position);
        }
    }
}
