﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Enemy
{
    class Dodongo : IEnemy
    {
        private EnemyStateMachine stateMachine;
        private float health;
        private IEnemyState state;

        public Dodongo(Game1 game, Vector2 position)
        {
            switch ((new Random()).Next(4))
            {
                case 0:
                    state = new DodongoStateUp(this, position);
                    break;
                case 1:
                    state = new DodongoStateDown(this, position);
                    break;
                case 2:
                    state = new DodongoStateLeft(this, position);
                    break;
                case 3:
                    state = new DodongoStateRight(this, position);
                    break;
            }
            health = 8f;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Draw(spriteBatch, color);
        }

        public void editPosition(Vector2 amount)
        {
            stateMachine.editPosition(amount);
        }

        public void ReceiveDamage(float amount, Vector2 direction) 
        {

            health -= amount;
            EnemyDamageDecorator decorator = new EnemyDamageDecorator(this, stateMachine, direction);
            stateMachine.swapInList(this, decorator);

        }

        public bool shouldRemove()
        {
            return health <= 0;
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
