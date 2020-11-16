using Game1.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class EnemyDamageDecorator : IEnemy
    {
        public int StunnedTimer { get; set; } = 0;

        public IEnemy original;
        private float damagedTimer;
        private Color[] flickers = { Color.LightBlue, Color.Orange, Color.Red };
        private int currentFlicker = 0;
        private float timeTillFlickerSwap;
        private Game1 game;
        public bool stillSlide;
        private const float deathSoundVol = 0.75f;

        private const float xAndYKnockbackMagnitude = 0.33f;
        Vector2 knockbackMagnitude = new Vector2(xAndYKnockbackMagnitude, xAndYKnockbackMagnitude);

        public EnemyDamageDecorator(IEnemy Original, Vector2 direction, Game1 game)
        {
            this.original = Original;

            const float damagedTimerMax = 425f;
            damagedTimer = damagedTimerMax; //ms

            const float timeTillFlickerSwapMax = 50f;
            timeTillFlickerSwap = timeTillFlickerSwapMax;

            knockbackMagnitude = Vector2.Multiply(knockbackMagnitude, direction);
            this.game = game;
            stillSlide = true;

            SoundHandle(Original);
        }

        private void SoundHandle(IEnemy Original)
        {
            const string aquamentusAudio = "Aquamentus", aquamentusHurtAudio = "aquamentusHurt", enemyDeathAudio = "enemyDeath", enemyHurtAudio = "enemyHurt";

            if (Original.GetType().Name.Equals(aquamentusAudio))
            {
                AudioManager.PlayFireForget(aquamentusHurtAudio);
            }
            else
            {
                if (Original.ShouldRemove())
                {
                    AudioManager.PlayFireForget(enemyDeathAudio, 0.0f, deathSoundVol);
                }
                else
                {
                    AudioManager.PlayFireForget(enemyHurtAudio);
                }
            }
        }

        public void ReceiveDamage(float amount, Vector2 direction)
        {
            //For now enemies will be unable to be damaged after being struck for the short period of time that this is wrapping the enemy.
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            original.Draw(spriteBatch, flickers[currentFlicker]);
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            damagedTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            timeTillFlickerSwap -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            const int minDamageTimer = 275;

            if (damagedTimer > minDamageTimer && stillSlide)
            {
                original.EditPosition(Vector2.Multiply(knockbackMagnitude, (float)gameTime.ElapsedGameTime.TotalMilliseconds));
            }
            else
                stillSlide = false;
            if(timeTillFlickerSwap <= 0)
            {
                currentFlicker++;

                const int currentFlickerMax = 2;

                if (currentFlicker > currentFlickerMax)
                    currentFlicker = 0;
            }

            if(damagedTimer <= 0)
            {
                game.Screen.CurrentRoom.UnDecoratedEnemyList.Add(this);
            }
            original.Update(gameTime, drawingLimits);
        }

        public void EditPosition(Vector2 amount)
        {
                original.EditPosition(amount);
        }

        public bool ShouldRemove()
        {
            return original.ShouldRemove();
        }

        public List<Rectangle> GetHitboxes()
        {
            return original.GetHitboxes();
        }

        public void SetState(IEnemyState state)
        {
            original.SetState(state);
        }

        public Vector2 GetPosition()
        {
            return original.GetPosition();
        }

        public void stopKnockback(Vector2 possibleCorrections)
        {
            stillSlide = false;
            Vector2 correction = possibleCorrections;

            if (knockbackMagnitude.X > 0)
                correction = Vector2.Multiply(correction, new Vector2(-1, 0));
            else if (knockbackMagnitude.X < 0)
                correction = Vector2.Multiply(correction, new Vector2(1, 0));
            else if (knockbackMagnitude.Y > 0)
                correction = Vector2.Multiply(correction, new Vector2(0, -1));
            else if (knockbackMagnitude.Y < 0)
                correction = Vector2.Multiply(correction, new Vector2(0, 1));

            original.EditPosition(correction);
        }

        public new Type GetType()
        {
            return original.GetType();
        }
    }
}
