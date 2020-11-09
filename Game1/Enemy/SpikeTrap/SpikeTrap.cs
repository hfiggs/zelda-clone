/* Author: Hunter Figgs */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class SpikeTrap : IEnemy
    {
        public int StunnedTimer { get; set; } = 0;

        private IEnemyState state;

        public SpikeTrap(Game1 game, Vector2 homePosition, int verticalRange, int horizontalRange)
        {
            state = new SpikeTrapStateHome(game, this, homePosition, verticalRange, horizontalRange);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Draw(spriteBatch,color);
        }

        public void ReceiveDamage(float amount, Vector2 direction)
        {
            // Cannot receive damage
        }

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

        public void EditPosition(Vector2 amount)
        {

        }

        public bool ShouldRemove()
        {
            return false;
        }

        public Vector2 GetPosition()
        {
            return state.GetPosition();
        }
        public List<Rectangle> GetHitboxes()
        {
            return state.GetHitboxes();
        }
    }
}
