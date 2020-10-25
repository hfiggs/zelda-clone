using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Enemy
{
    class SpawnDecorator : IEnemy
    {
        IEnemy original;
        IEnemy spawnParticles;
        private float spawnTimer;
        Game1 game;

        public SpawnDecorator(IEnemy original, Vector2 position, Game1 game)
        {
            this.game = game;
            this.original = original;
            this.spawnTimer = 450f;
            this.spawnParticles = new SpawnParticles(position);
        }

        public void ReceiveDamage(float amount, Vector2 direction)
        {
            //For now enemies will be unable to be damaged after spawning
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            this.spawnParticles.Draw(spriteBatch, color);
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            spawnTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (spawnTimer <= 0)
            {
                game.Screen.CurrentRoom.EnemyList.Add(original);
                game.Screen.CurrentRoom.EnemyList.Remove(this);
            }
            spawnParticles.Update(gameTime, drawingLimits);
        }
        public void SpawnAnimation()
        {

        }
        public Rectangle GetHitbox()
        {
            return original.GetHitbox();
        }

        public void SetState(IEnemyState state)
        {
            original.SetState(state);
        }

        public void ReceiveDamage()
        {
            //throw new NotImplementedException();
        }
    }
}
