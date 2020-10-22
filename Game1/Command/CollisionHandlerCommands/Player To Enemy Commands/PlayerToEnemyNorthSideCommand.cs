﻿

using Game1.Collision_Handling;
using Game1.Enemy;
using Game1.Player;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class PlayerToEnemyNorthSideCommand : ICollisionCommand
    {
        public PlayerToEnemyNorthSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IEnemy enemy = (IEnemy)collision.collidee;

            enemy.ReceiveDamage(1f, new Vector2(1, 0));
        }
    }
}