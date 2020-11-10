using Game1.Particle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Projectile
{
    class EnemyBoomerang : IProjectile
    {
        private int rowModifier, counter;
        private float moveSpeed, totalElapsedGameTime;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private ProjectileSpriteSheet sprite;
        private bool returned;
        private Vector2 position;
        private Vector2 originalPosition;

        private List<IParticle> particles = new List<IParticle>();
        private readonly Vector2 particleOffset = new Vector2(16.0f, 12.0f);

        public EnemyBoomerang(char direction, Vector2 position)
        {
            this.direction = direction;
            this.position = position;
            this.originalPosition = position;
            sprite = ProjectileSpriteFactory.Instance.CreateBoomerangSprite();
            moveSpeed = 100;
            rowModifier = 0;
            totalElapsedGameTime = 0;
            counter = 0;
            returned = false;

        }
        public void Update(GameTime gameTime)
        {

            Rectangle origin = new Rectangle((int)originalPosition.X, (int)originalPosition.Y, 16, 16);
            // Stop drawing and updating position of boomerang if it has returned to its owner
            totalElapsedGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (origin.Contains((int)position.X, (int)position.Y) && totalElapsedGameTime > 1) {
                returned = true;
            }


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
            } else if (totalElapsedGameTime < 2) {
                if (direction == 'N') {
                    position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else if (direction == 'S') {
                    position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else if (direction == 'W') {
                    position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else {
                    position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
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

            particles.RemoveAll(p => (p.ShouldDelete()));

            foreach (IParticle particle in particles)
            {
                particle.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (!returned) {
                int columnOfSprite = sprite.GetColumnOfSprite();
                Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite, rowModifier);
                Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);
                spriteBatch.Draw(sprite.GetTexture(), destinationRectangle, sourceRectangle, color);
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

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)position.X + 16, (int)position.Y + 16, 8, 8);
        }

        public bool ShouldDelete()
        {
            return returned;
        }

        public void BeginDespawn()
        {
            totalElapsedGameTime = 1;
            AddParticle(new ShieldDeflect(position + particleOffset));
        }

        public void AddParticle(IParticle particle)
        {
            particles.Add(particle);
        }
    }
}
