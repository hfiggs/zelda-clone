using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
        Vector2 knockbackMagnitude = new Vector2(1f, 1f);

        private const float deathSoundVol = 0.75f;
        public EnemyDamageDecorator( IEnemy Original, Vector2 direction, Game1 game)
        {
            this.original = Original;
            damagedTimer = 350f; //ms
            timeTillFlickerSwap = 50f;
            knockbackMagnitude = Vector2.Multiply(knockbackMagnitude, direction);
            this.game = game;
            stillSlide = true;

            SoundHandle(Original);
        }

        private void SoundHandle(IEnemy Original)
        {
            if (Original.GetType().Name.Equals("Aquamentus"))
            {
                AudioManager.PlayFireForget("aquamentusHurt");
            }
            else
            {
                if (Original.ShouldRemove())
                {
                    AudioManager.PlayFireForget("enemyDeath", 0.0f, deathSoundVol);
                }
                else
                {
                    AudioManager.PlayFireForget("enemyHurt");
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
            if (damagedTimer > 300 && stillSlide)
            {
                original.EditPosition(Vector2.Multiply(knockbackMagnitude, (float)gameTime.ElapsedGameTime.TotalMilliseconds));
            }
            else
                stillSlide = false;
            if(timeTillFlickerSwap <= 0)
            {
                currentFlicker++;
                if (currentFlicker > 2)
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

        public Rectangle GetHitbox()
        {
            return original.GetHitbox();
        }

        public void SetState(IEnemyState state)
        {
            original.SetState(state);
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
    }
}
