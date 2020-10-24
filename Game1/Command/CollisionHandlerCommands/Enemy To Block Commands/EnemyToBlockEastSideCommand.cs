

using Game1.Collision_Handling;
using Game1.Enemy;
using Game1.Player;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class EnemyToBlockEastSideCommand : ICollisionCommand
    {
        public EnemyToBlockEastSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IEnemy enemy = (IEnemy)collision.collider;
            Vector2 movementAmount = new Vector2(collision.intersectionRec.Width, 0);
            if (enemy.GetType() != typeof(Bat))
            {
                enemy.editPosition(movementAmount);
            }
        }
    }
}
