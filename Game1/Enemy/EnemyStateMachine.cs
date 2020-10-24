using Game1.Projectile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics.Eventing.Reader;

namespace Game1.Enemy
{
    class EnemyStateMachine
    {
        public Game1 game;

        private IEnemyState state;

        public EnemyStateMachine(Game1 game)
        {
            this.game = game;
        }

        public void SetState(IEnemyState state)
        {
            this.state = state;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Sprite.Draw(spriteBatch, state.GetPosition(), color);
        }

        public void Attack()
        {
            state.Attack();
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            state.Update(gametime, drawingLimits);
        }

        public void swapInList(IEnemy enemy, IEnemy decorator)
        {
            game.EnemyList.Remove(enemy);
            game.EnemyList.AddFirst(decorator);
        }

        public void editPosition(Vector2 amount)
        {
            state.editPosition(amount);
        }

        public Vector2 GetPosition()
        {
            return state.GetPosition();
        }

        public Vector2 GetDirection()
        {
            return state.GetDirection();
        }

        public Rectangle GetPlayerRectangle()
        {
            return game.GetPlayerRectangle();
        }

        public Rectangle GetHitbox()
        {
            return state.GetHitbox();
        }

        public Vector2 GetWindowDimensions()
        {
            return game.GetWindowDimensions();
        }

        public void spawnProjectile(IProjectile projectile)
        {
            game.SpawnProjectile(projectile);
        }
    }
}
