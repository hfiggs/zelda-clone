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
    class EnemyDamageDecorator : IEnemy
    {
        IEnemy original;
        private float damagedTimer;
        Color[] flickers = { Color.LightBlue, Color.Orange, Color.Red };
        int currentFlicker = 0;
        EnemyStateMachine machine;

        Vector2 knockbackMagnitude = new Vector2(32, 32);
        public EnemyDamageDecorator( IEnemy Original, EnemyStateMachine stateMachine, Vector2 direction)
        {
            this.original = Original;
            machine = stateMachine;
            damagedTimer = 80f; //ms

            knockbackMagnitude = Vector2.Multiply(knockbackMagnitude, direction);
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
            Vector2 slideAmount = Vector2.Multiply(knockbackMagnitude, (float)gameTime.ElapsedGameTime.TotalMilliseconds);
            damagedTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if(damagedTimer > 60)
            {
                original.editPosition(slideAmount);
            }
            if(damagedTimer % 20 == 0)
            {
                currentFlicker++;
                if (currentFlicker > 2)
                    currentFlicker = 0;
            }

            if(damagedTimer <= 0)
            {
                machine.swapInList(this, original);
            }
            original.Update(gameTime, drawingLimits);
        }

        public void editPosition(Vector2 amount)
        {
            original.editPosition(amount);
        }

        public bool shouldRemove()
        {
            return original.shouldRemove();
        }
    }
}
