using Game1.Collision_Handling;
using Game1.Enemy;
using Game1.GameState;
using Game1.Player;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class EnemyToPlayerEastSideCommand : ICollisionCommand
    {
        private readonly Vector2 eastVector = new Vector2(-1, 0);

        private readonly Game1 game;

        public  EnemyToPlayerEastSideCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute(Collision collision)
        {
            IEnemy enemy = (IEnemy)collision.collider;
            IPlayer player = (IPlayer)collision.collidee;

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

                player.ReceiveDamage(damage, eastVector);
            }
        }
    }
}
