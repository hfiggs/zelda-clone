using Game1.Collision_Handling;
using Game1.Enemy;
using Game1.Player;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class EnemyToPlayerWestSideCommand : ICollisionCommand
    {
        private readonly Vector2 westVector = new Vector2(1, 0);

        public EnemyToPlayerWestSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IEnemy enemy = (IEnemy)collision.collider;
            IPlayer player = (IPlayer)collision.collidee;
            if (enemy.GetType() != typeof(OldMan))
            {
                int damage = CollisionHandlerUtil.GetEnemyDamage(enemy.GetType());

                player.ReceiveDamage(damage, westVector);
            }
        }
    }
}
