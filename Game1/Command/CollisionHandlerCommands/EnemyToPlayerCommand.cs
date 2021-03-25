using Game1.Collision_Handling;
using Game1.Enemy;
using Game1.GameState;
using Game1.Player;
using Game1.Util;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class EnemyToPlayerCommand : ICollisionCommand
    {
        private readonly Game1 game;

        public EnemyToPlayerCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(Collision collision)
        {
            IEnemy enemy = (IEnemy)collision.Collider;
            IPlayer player = (IPlayer)collision.Collidee;

            if (enemy is OldMan)
            {
                // do nothing
            }
            else if (enemy is Hand)
            {
                int damage = CollisionHandlerUtil.GetEnemyDamage(enemy.GetType());

                player.ReceiveDamage(damage, new Vector2(0, 0));

                if (player.PlayerInventory.HalfHeartCount != 0)
                {
                    game.SetState(new GameStateWallmaster(game, player));
                }
            }
            else
            {
                int damage = CollisionHandlerUtil.GetEnemyDamage(enemy.GetType());

                player.ReceiveDamage(damage, CompassDirectionUtil.GetOppositeDirectionVector(collision.Side));
            }
        }
    }
}
