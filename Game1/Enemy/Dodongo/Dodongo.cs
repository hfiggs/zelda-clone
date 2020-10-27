using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Enemy
{
    class Dodongo : IEnemy
    {
        public int StunnedTimer { get; set; } = 0;

        private float health;
        private IEnemyState state;
        private Vector2 position;
        private Game1 game;

        public Dodongo(Game1 game, Vector2 position)
        {
            this.game = game;
            this.position = position;

            IEnemyState nextState;
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
            state.Draw(spriteBatch, color);
        }

        public void EditPosition(Vector2 amount)
        {
            state.editPosition(amount);
        }

        public void ReceiveDamage(float amount, Vector2 direction) 
        {

            health -= amount;
            EnemyDamageDecorator decorator = new EnemyDamageDecorator(this, direction, game);
            game.Screen.CurrentRoom.EnemyList.Add(decorator);
            game.Screen.CurrentRoom.EnemyList.Remove(this);

        }

        public bool ShouldRemove()
        {
            return health <= 0;
        }

        public void ReceiveDamage() {  /* TODO: Receive damage */ }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            if (StunnedTimer == 0)
            {
                state.Update(gameTime, drawingLimits);
            }

            StunnedTimer -= (StunnedTimer == int.MaxValue) ? 0 : (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            StunnedTimer = Math.Max(0, StunnedTimer);
        }

        public void SetState(IEnemyState state)
        {
            this.state = state;
        }

        public Rectangle GetHitbox()
        {
            return state.GetHitbox();
        }
    }
}
