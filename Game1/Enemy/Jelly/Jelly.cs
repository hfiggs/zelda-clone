using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class Jelly : IEnemy
    {
        private IEnemyState state;
        private Vector2 position;
        private Game1 game;
        public Jelly(Game1 game, Vector2 spawnPosition)
        {
            this.game = game;
            this.position = spawnPosition;
            state = new JellyStateMoving(spawnPosition);
        }

        public void ReceiveDamage()
        {
            state.ReceiveDamage();
        }
        public void SpawnAnimation()
        {
            SpawnDecorator decorator = new SpawnDecorator(this, position, game);
            game.Screen.CurrentRoom.EnemyList.Add(decorator);
            game.Screen.CurrentRoom.EnemyList.Remove(this);
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Draw(spriteBatch,color);
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            state.Update(gameTime, drawingLimits);
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
