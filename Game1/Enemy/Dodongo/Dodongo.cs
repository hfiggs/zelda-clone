using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class Dodongo : IEnemy
    {
        public int StunnedTimer { get; set; } = 0;

        private int deathTimer = 0;
        private float health;
        private IEnemyState state;
        private IEnemyState nextState;
        private Vector2 position;
        private Game1 game;

        public Dodongo(Game1 game, Vector2 position)
        {
            this.game = game;
            this.position = position;

            switch (new Random().Next(4))
            {
                case 0:
                    nextState = new DodongoStateUp(this, position);
                    break;
                case 1:
                    nextState = new DodongoStateDown(this, position);
                    break;
                case 2:
                    nextState = new DodongoStateLeft(this, position);
                    break;
                case 3:
                default:
                    nextState = new DodongoStateRight(this, position);
                    break;
            }

            state = new EnemyStateSpawning(this.position, this, nextState);

            health = 8f;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (!this.ShouldRemove())
                state.Draw(spriteBatch, color);
        }

        public void EditPosition(Vector2 amount)
        {
            state.editPosition(amount);
        }

        public void ReceiveDamage(float amount, Vector2 direction) 
        {
            const float bombDamage = 4f;
            if (StunnedTimer > 0) {
                health -= amount;
                
            } else if (amount == 0) {
                health -= bombDamage;
                Vector2 currentPosition = state.GetPosition();
                switch (state) {
                case DodongoStateUp _:
                    nextState = new DodongoStateUpSwallow(this, currentPosition);
                    break;
                case DodongoStateDown _:
                    nextState = new DodongoStateDownSwallow(this, currentPosition);
                    break;
                case DodongoStateLeft _:
                    nextState = new DodongoStateLeftSwallow(this, currentPosition);
                    break;
                case DodongoStateRight _:
                default:
                    nextState = new DodongoStateRightSwallow(this, currentPosition);
                    break;
                }
                state = nextState;
                const int timeTillDeath = 1000; // milliseconds
                deathTimer = timeTillDeath;
            }
        }

        public bool ShouldRemove()
        {
            return health <= 0 && deathTimer <= 0;
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            deathTimer -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (StunnedTimer == 0)
                state.Update(gameTime, drawingLimits);

            StunnedTimer -= (StunnedTimer == int.MaxValue) ? 0 : (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            StunnedTimer = Math.Max(0, StunnedTimer);
        }

        public Vector2 GetPosition()
        {
            return state.GetPosition();
        }
        public void SetState(IEnemyState state)
        {
            this.state = state;
        }

        public List<Rectangle> GetHitboxes()
        {
            return state.GetHitboxes();
        }
    }
}
