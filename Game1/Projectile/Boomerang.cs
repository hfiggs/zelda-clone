using Game1.Audio;
using Game1.Environment;
using Game1.Particle;
using Game1.Player;
using Game1.Player.PlayerInventory;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1.Projectile
{
    class Boomerang : IProjectile
    {
        private int rowModifier = 0;
        private int counter = 0;
        private char direction; // 'N' = North, 'S' = South, 'W' = West, 'E' = East
        private const char north = 'N', south = 'S', west = 'W', east = 'E';
        private ProjectileSpriteSheet sprite;
        private bool returned = false;
        public IPlayer Player { get; private set; }
        private Vector2 position;

        private const float initialVelocity = 300.0f;
        private const float returnVelocity = 150.0f;
        private const float accelleration = 34700.0f;
        private float currentVelocity = initialVelocity;
        private const float maxDist = 80; //in px
        private float distTravelled = 0;
        private bool collided = false;

        //minimum range to "recieve" boomerang should be no less than 5 - see README
        private const float minimumRecieveDist = 5.0f;
        private const float minimumCatchDist = 30.0f;
        private const float recieveYOffest = -2.0f;

        private readonly Rectangle hitboxOrig = new Rectangle(16, 16, 8, 8);
        private Rectangle hitbox = new Rectangle(16, 16, 8, 8);
        private bool rayTraced = false;
        private const int rayLength = 5;

        private List<IParticle> particles = new List<IParticle>();
        private readonly Vector2 particleOffset = new Vector2(16.0f, 12.0f);
        
        SoundEffectInstance sound;
        private float soundVol = 0.5f;
        //delay should be no more than 0.5f (creates sound bugs)
        private float soundDelay = 0.25f;
        private bool despawning = false;
        const string boomerangAudio = "boomerang";

        public Boomerang(char direction, IPlayer player) {
            this.direction = direction;
            this.Player = player;
            sprite = ProjectileSpriteFactory.Instance.CreateBoomerangSprite();
            //centers the boomerang at the center of the player's hitbox, -5 in the y direction
            position.X = player.GetPlayerHitbox().X + (player.GetPlayerHitbox().Width / 2) - (sprite.PickSprite(0, 0).Width / 2);
            position.Y = player.GetPlayerHitbox().Y + (player.GetPlayerHitbox().Height / 2) - (sprite.PickSprite(0, 0).Height / 2) + recieveYOffest;

            sound = AudioManager.PlayLooped(boomerangAudio, soundDelay, soundVol);
            player.setBoomerangOut(true);

            InitRayTraceHitbox();
        }
        public void Update(GameTime gameTime) {
            if (distTravelled < maxDist && !collided) {
                BoomerangOut(gameTime);
            } else if (!returned) {
                BoomerangIn(gameTime);
            }

            if(rayTraced)
                hitbox = hitboxOrig;

            if(returned)
            {
                Player.PlayerInventory.SetItemInUse(ItemEnum.Boomerang, false);
                AudioManager.StopSound(sound);
            }

            // Used to change sprite sheet row to allow for flashing
            const int counterInterval = 5, rowMax = 3;
            if (counter % counterInterval == 0) {
                if (rowModifier == rowMax) {
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

        private void BoomerangOut(GameTime gameTime)
        {
            if (direction == north)
            {
                position.Y -= currentVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (direction == south)
            {
                position.Y += currentVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (direction == west)
            {
                position.X -= currentVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (direction == east)
            {
                position.X += currentVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            distTravelled += currentVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //decellerating with clamp
            currentVelocity -= (float)(accelleration * (gameTime.ElapsedGameTime.TotalSeconds * gameTime.ElapsedGameTime.TotalSeconds));
            if (currentVelocity > initialVelocity)
            {
                currentVelocity = initialVelocity;
            }
        }

        private void BoomerangIn(GameTime gameTime)
        {
            const int dividebyTwo = 2;

            //return velocity clamp - strict ordering with below accelleration
            if (currentVelocity > returnVelocity)
            {
                currentVelocity = returnVelocity;
            }

            Rectangle currentLocation = sprite.PickSprite(0, 0);
            currentLocation.Location = new Point((int)position.X, (int)position.Y);
            Vector2 recievePoisition = new Vector2(Player.GetPlayerHitbox().X + (Player.GetPlayerHitbox().Width / dividebyTwo) - (sprite.PickSprite(0, 0).Width / dividebyTwo), Player.GetPlayerHitbox().Y + (Player.GetPlayerHitbox().Height / dividebyTwo) - (sprite.PickSprite(0, 0).Height / dividebyTwo) + recieveYOffest);

            Vector2 positionDiff = new Vector2(currentLocation.X, currentLocation.Y) - recievePoisition;
            if(positionDiff.Length() < minimumCatchDist)
            {
                CatchBoomerang();
            }
            returned = positionDiff.Length() < minimumRecieveDist;
            positionDiff = Vector2.Normalize(positionDiff);
            position.X -= positionDiff.X * currentVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Y -= positionDiff.Y * currentVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //accellerating - strict ordering with above clamp
            currentVelocity += (float)(accelleration * (gameTime.ElapsedGameTime.TotalSeconds * gameTime.ElapsedGameTime.TotalSeconds)) / (float)dividebyTwo;
        }

        private void InitRayTraceHitbox()
        {
            switch(direction)
            {
                case north:
                    hitbox.Y -= rayLength;
                    hitbox.Height += rayLength;
                    rayTraced = true;
                    break;
                case south:
                    hitbox.Height += rayLength;
                    rayTraced = true;
                    break;
                case east:
                    hitbox.Width += rayLength;
                    rayTraced = true;
                    break;
                case west:
                    hitbox.X -= rayLength;
                    hitbox.Width += rayLength;
                    rayTraced = true;
                    break;
                default:
                    const string errorMessage = "Direction not initialized";
                    Console.WriteLine(errorMessage);
                    break;
            }
            rayTraced = true;
        }

        private void CatchBoomerang()
        {
            Player.setBoomerangOut(false);
            Player.UseItem();
        }
        public void Draw(SpriteBatch spriteBatch, Color color) {
            if (!returned) {
                int columnOfSprite = sprite.GetColumnOfSprite();
                Rectangle sourceRectangle = sprite.PickSprite(columnOfSprite, rowModifier);
                Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);
                spriteBatch.Draw(sprite.GetTexture(), destinationRectangle, sourceRectangle, color, 0f, new Vector2(0f, 0f), SpriteEffects.None, SpriteLayerUtil.projectileLayer);
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
            if (!despawning)
            {
                AddParticle(new ShieldDeflect(position + particleOffset));
                AudioManager.StopSound(sound);
                sound = AudioManager.PlayLooped(boomerangAudio, 0.0f, soundVol);
                despawning = true;
                collided = true;
            }
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)position.X + hitbox.X, (int)position.Y + hitbox.Y, hitbox.Width, hitbox.Height);
        }

        public void AddParticle(IParticle particle)
        {
            particles.Add(particle);
        }
    }
}
