

using Game1.Collision_Handling;
using Game1.Enemy;
using Game1.Player;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class PlayerToEnemySouthSideCommand : ICollisionCommand
    {
        public PlayerToEnemySouthSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            if (collision.intersectionRec.Width != 0 || collision.intersectionRec.Height != 0)
            {
                IEnemy enemy = (IEnemy)collision.collidee;

                enemy.ReceiveDamage(1f, new Vector2(-1, 0));
            }
        }
    }
}
