using Game1.Particle;
using Game1.Player;
using Game1.Player.PlayerInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1.Projectile
{
    class Boomerang : IProjectile
    {
        private int rowModifier, counter;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private ProjectileSpriteSheet sprite;
        private bool returned;
        public IPlayer Player { get; private set; }
        private Vector2 position;
        private float moveSpeed, totalElapsedGameTime;

        //minimum range to "recieve" boomerang should be no less than 5 - see README
        private const float minimumRecieveDist = 5.0f;

        private List<IParticle> particles = new List<IParticle>();
        private readonly Vector2 particleOffset = new Vector2(16.0f, 12.0f);
        SoundEffectInstance sound;
        private float soundVol = 0.5f;

        public Boomerang(char direction, IPlayer player) {
            this.direction = direction;
            this.Player = player;
            sprite = ProjectileSpriteFactory.Instance.CreateBoomerangSprite();
            //centers the boomerang at the center of the player's hitbox, -5 in the y direction
            position.X = player.GetPlayerHitbox().X + (player.GetPlayerHitbox().Width / 2) - (sprite.PickSprite(0, 0).Width / 2);
            position.Y = player.GetPlayerHitbox().Y + (player.GetPlayerHitbox().Height / 2) - (sprite.PickSprite(0, 0).Height / 2) - 5.0f;
            rowModifier = 0;
            moveSpeed = 100;
            totalElapsedGameTime = 0;
            counter = 0;
            returned = false;

            sound = AudioManager.PlayLooped("boomerang", 0.0f, soundVol);
        }
        public void Update(GameTime gameTime) {
            totalElapsedGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (totalElapsedGameTime < 1) {
                if (direction == 'N') {
                    position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Console.WriteLine("N");
                } else if (direction == 'S') {
                    position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Console.WriteLine("S");
                } else if (direction == 'W') {
                    position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Console.WriteLine("W");
                } else {
                    position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Console.WriteLine("E");
                }
            } else if (!returned) {
                Rectangle currentLocation = sprite.PickSprite(0, 0);
                currentLocation.Location = new Point((int)position.X, (int)position.Y);
                Vector2 recievePoisition = new Vector2(Player.GetPlayerHitbox().X + (Player.GetPlayerHitbox().Width / 2) - (sprite.PickSprite(0, 0).Width / 2), Player.GetPlayerHitbox().Y + (Player.GetPlayerHitbox().Height / 2) - (sprite.PickSprite(0, 0).Height / 2) - 5.0f);

                Vector2 positionDiff = new Vector2(currentLocation.X, currentLocation.Y) - recievePoisition;
                returned =  positionDiff.Length() < minimumRecieveDist;
                positionDiff = Vector2.Normalize(positionDiff);
                position.X -= positionDiff.X * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                position.Y -= positionDiff.Y * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if(returned)
            {
                Player.PlayerInventory.SetItemInUse(ItemEnum.Boomerang, false);
                AudioManager.StopSound(sound);
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
        public void Draw(SpriteBatch spriteBatch, Color color) {
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

        public bool ShouldDelete()
        {
            return returned;
        }

        public void BeginDespawn()
        {
            totalElapsedGameTime = 2;
            AddParticle(new ShieldDeflect(position + particleOffset));
            AudioManager.StopSound(sound);
            sound = AudioManager.PlayLooped("boomerang", 0.0f, soundVol);
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)position.X + 16, (int)position.Y + 16, 8, 8);
        }

        public void AddParticle(IParticle particle)
        {
            particles.Add(particle);
        }
    }
}
