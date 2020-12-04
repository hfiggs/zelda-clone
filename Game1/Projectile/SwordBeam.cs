using Game1.Audio;
using Game1.Particle;
using Game1.Sprite;
using Game1.Util;
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
        private const char north = 'N', south = 'S', west = 'W', east = 'E';
        private const float moveSpeed = 200;
        private const float delay = 200; // miliseconds
        private ProjectileSpriteSheet sprite;
        private Vector2 position;
        private bool removeMe = false;
        private readonly Vector2 positionOffset = new Vector2(-2.0f, 3.0f);

        private const float soundDelay = 0.2f;

        private IParticle particles;
        private bool particlesSpawned;
        private readonly Vector2 particleOffset = new Vector2(14.0f, 14.0f);

        public SwordBeam(char direction, Vector2 position)
        {
            this.direction = direction;
            this.position = position + positionOffset;
            sprite = ProjectileSpriteFactory.Instance.CreateSwordBeamSprite();
            columnModifier = 0;
            rowModifier = 0;
            counter = 0;
            totalTime = 0;

            const string swordBeamAudio = "swordBeam";
            AudioManager.PlayFireForget(swordBeamAudio, soundDelay);
        }
        public void Update(GameTime gameTime)
        {
            const int northSprite = 0, southSprite = 1, westSprite = 2, eastSprite = 3;

            totalTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (totalTime >= delay && !removeMe) {
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

                // Used to change sprite sheet column and allow for flashing
                const int counterMax = 5;
                if (counter == counterMax) {
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

            const int topLimit = 10, bottomLimit = 130, leftLimit = 10, rightLimit = 206;
            if(position.Y < topLimit || position.Y > bottomLimit || position.X < leftLimit || position.X > rightLimit)
            {
                BeginDespawn();
                /*if(!particlesSpawned)
                    SpawnParticles();*/
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (totalTime >= delay && !removeMe) {
                int columnOfSprite = sprite.GetColumnOfSprite();
                Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite + columnModifier, rowModifier); ;
                Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);

                spriteBatch.Draw(sprite.GetTexture(), destinationRectangle, sourceRectangle, color, 0f, new Vector2(0f, 0f), SpriteEffects.None, SpriteLayerUtil.projectileLayer);
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
            if (particlesSpawned)
            {
                return removeMe && particles.ShouldDelete();
            }
            return removeMe;
        }

        public void BeginDespawn()
        {
            removeMe = true;
            if(!particlesSpawned)
                SpawnParticles();
        }

        public Rectangle GetHitbox()
        {
            Rectangle hitbox = new Rectangle(0, 0, 0, 0);

            if (!removeMe)
            {
                if (direction == north || direction == south)
                {
                    const int xDiff = 17, yDiff = 12, width = 7, height = 16;
                    hitbox = new Rectangle((int)position.X + xDiff, (int)position.Y + yDiff, width, height);
                }
                else
                {
                    const int xDiff = 11, yDiff = 16, width = 18, height = 7;
                    hitbox = new Rectangle((int)position.X + xDiff, (int)position.Y + yDiff, width, height);
                }
            }

            return hitbox;
        }

        private void SpawnParticles()
        {
            particles = new BeamExplosion(position + particleOffset);
            particlesSpawned = true;
        }

        public void EditPosition(Vector2 amount)
        {
            position = Vector2.Add(amount, position);
        }
    }
}
