

using Game1.Collision_Handling;
using Game1.Enemy;
using Game1.Environment;
using Game1.Player;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Command.CollisionHandlerCommands
{
    class EnemyToBlockNorthSideCommand : ICollisionCommand
    {
        private const int rightXBorder = 210, leftXBorder = 32, topYBorder = 32, botYBorder = 135;

        public EnemyToBlockNorthSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IEnemy enemy = (IEnemy)collision.Collider;

            Vector2 movementAmount = new Vector2(0, -collision.IntersectionRec.Height);
            if (enemy.GetType() != typeof(Bat) && enemy.GetType() != typeof(Hand))
            {

                if (enemy.GetType() == typeof(EnemyDamageDecorator) && ((EnemyDamageDecorator)enemy).stillSlide)
                {
                    ((EnemyDamageDecorator)enemy).stopKnockback(new Vector2(collision.IntersectionRec.Width, collision.IntersectionRec.Height));
                }
                else
                {
                    enemy.EditPosition(movementAmount);
                }
            }
            else
            {
                if (collision.IntersectionRec.X >= rightXBorder)
                    enemy.EditPosition(new Vector2(-collision.IntersectionRec.Width, 0));
                else if (collision.IntersectionRec.Y <= topYBorder)
                    enemy.EditPosition(new Vector2(0, collision.IntersectionRec.Height));
                else if (collision.IntersectionRec.Y >= botYBorder)
                    enemy.EditPosition(new Vector2(0, -collision.IntersectionRec.Height));
                else if (collision.IntersectionRec.X <= leftXBorder)
                    enemy.EditPosition(new Vector2(collision.IntersectionRec.Width, 0));
            }
        }
    }
}
