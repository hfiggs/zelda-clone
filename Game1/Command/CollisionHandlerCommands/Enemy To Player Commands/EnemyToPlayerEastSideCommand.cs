using Game1.Collision_Handling;
using Game1.Enemy;
using Game1.Player;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class EnemyToPlayerEastSideCommand : ICollisionCommand
    {
        private readonly Vector2 eastVector = new Vector2(-1, 0);

        public  EnemyToPlayerEastSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IEnemy enemy = (IEnemy)collision.collider;
            IPlayer player = (IPlayer)collision.collidee;

            int damage = CollisionHandlerUtil.GetEnemyDamage(enemy.GetType());

            player.ReceiveDamage(damage, eastVector);
        }
    }
}
