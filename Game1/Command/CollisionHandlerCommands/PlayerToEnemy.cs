using Game1.Collision_Handling;
using Game1.Enemy;
using Game1.Player;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class PlayerToEnemy : ICollisionCommand
    {
        public PlayerToEnemy()
        {

        }

        public void Execute(Collision collision)
        {
            IPlayer player = (IPlayer)collision.collider;
            IEnemy enemy = (IEnemy)collision.collidee;
            if (collision.intersectionRec.Width != 0 || collision.intersectionRec.Height != 0)
            {
                if (player.GetDirection() == 'E')
                    enemy.ReceiveDamage(1f, new Vector2(1, 0));
                else if (player.GetDirection() == 'W')
                    enemy.ReceiveDamage(1f, new Vector2(-1, 0));
                else if (player.GetDirection() == 'S')
                    enemy.ReceiveDamage(1f, new Vector2(0, 1));
                else if (player.GetDirection() == 'N')
                    enemy.ReceiveDamage(1f, new Vector2(0, -1));
            }
        }
    }
}
