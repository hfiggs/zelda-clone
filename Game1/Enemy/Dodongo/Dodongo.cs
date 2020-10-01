using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Enemy
{
    class Dodongo : IEnemy
    {
        private EnemyStateMachine stateMachine;

        public Dodongo(Game1 game, SpriteBatch spriteBatch, Vector2 position)
        {
            stateMachine = new EnemyStateMachine(game, spriteBatch);

            switch ((new Random()).Next(4))
            {
                case 0:
                    stateMachine.SetState(new DodongoStateUp(stateMachine, position));
                    break;
                case 1:
                    stateMachine.SetState(new DodongoStateDown(stateMachine, position));
                    break;
                case 2:
                    stateMachine.SetState(new DodongoStateLeft(stateMachine, position));
                    break;
                case 3:
                    stateMachine.SetState(new DodongoStateRight(stateMachine, position));
                    break;
            }
        }

        public void Attack() {  /* Cannot attack */ }

        public void Draw()
        {
            stateMachine.Draw(Color.White);
        }

        public void ReceiveDamage() {  /* TODO: Receive damage */ }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            stateMachine.Update(gameTime, drawingLimits);
        }
    }
}
