using Game1.Particle;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Projectile
{
    class SwordBeam : IProjectile
    {
        private int columnModifier, counter, rowModifier;
        private float totalTime;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private const float moveSpeed = 200;
        private const float delay = 200; // miliseconds
        private ProjectileSpriteSheet sprite;
        private Vector2 position;
        private bool removeMe = false;
        private readonly Vector2 positionOffset = new Vector2(-2.0f, 3.0f);

        private const float soundDelay = 0.2f;

        private IParticle particles;
        private bool particlesSpawned;
        private Vector2 particleOffset = new Vector2(14.0f, 14.0f);

        public SwordBeam(char direction, Vector2 position)
        {
            this.direction = direction;
            this.position = position + positionOffset;
            sprite = ProjectileSpriteFactory.Instance.CreateSwordBeamSprite();
            columnModifier = 0;
            rowModifier = 0;
            counter = 0;
            totalTime = 0;

            AudioManager.PlayFireForget("swordBeam", soundDelay);
        }
        public void Update(GameTime gameTime)
        {
            totalTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (totalTime >= delay && !removeMe) {
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

            if(particlesSpawned)
            {
                particles.Update(gameTime);
            }

            if(position.Y < 10 || position.Y > 130 || position.X < 10 || position.X > 206)
            {
                BeginDespawn();
                if(!particlesSpawned)
                    SpawnParticles();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (totalTime >= delay && !removeMe) {
                int columnOfSprite = sprite.GetColumnOfSprite();
                Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite + columnModifier, rowModifier); ;
                Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);

                spriteBatch.Draw(sprite.GetTexture(), destinationRectangle, sourceRectangle, color);
            }

            if (particlesSpawned)
            {
                particles.Draw(spriteBatch, color);
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
            return removeMe && particles.ShouldDelete();
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

        public void SpawnParticles()
        {
            particles = new BeamExplosion(position + particleOffset);
            particlesSpawned = true;
        }
    }
}
