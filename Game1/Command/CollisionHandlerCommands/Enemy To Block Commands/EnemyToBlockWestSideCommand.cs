

using Game1.Collision_Handling;
using Game1.Enemy;
using Game1.Environment;
using Game1.Player;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Command.CollisionHandlerCommands
{
    class EnemyToBlockWestSideCommand : ICollisionCommand
    {
        public EnemyToBlockWestSideCommand()
        {
        }

        public void Execute(Collision collision)
        {
            IEnemy enemy = (IEnemy)collision.collider;
            Vector2 movementAmount = new Vector2(-collision.intersectionRec.Width, 0);
            if(enemy.GetType() != typeof(Bat) && enemy.GetType() != typeof(Hand))
            {
                enemy.EditPosition(movementAmount);
            }
            else
            {
                if (collision.intersectionRec.X >= 210)
                    enemy.EditPosition(new Vector2(-collision.intersectionRec.Width, 0));
                else if (collision.intersectionRec.Y <= 32)
                    enemy.EditPosition(new Vector2(0, collision.intersectionRec.Height));
                else if (collision.intersectionRec.Y >= 135)
                    enemy.EditPosition(new Vector2(0, -collision.intersectionRec.Height));
                else if (collision.intersectionRec.X <= 32)
                    enemy.EditPosition(new Vector2(collision.intersectionRec.Width, 0));
            }
        }
    }
}
