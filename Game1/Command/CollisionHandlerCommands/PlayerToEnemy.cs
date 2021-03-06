﻿using Game1.Collision_Handling;
using Game1.Enemy;
using Game1.Player;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class PlayerToEnemy : ICollisionCommand
    {
        const char north = 'N', south = 'S', west = 'W', east = 'E';

        public PlayerToEnemy()
        {

        }

        public void Execute(Collision collision)
        {
            IPlayer player = (IPlayer)collision.Collider;
            IEnemy enemy = (IEnemy)collision.Collidee;
            if (collision.IntersectionRec.Width != 0 || collision.IntersectionRec.Height != 0)
            {
                if (player.GetDirection() == east)
                    enemy.ReceiveDamage(1f, new Vector2(1, 0));
                else if (player.GetDirection() == west)
                    enemy.ReceiveDamage(1f, new Vector2(-1, 0));
                else if (player.GetDirection() == south)
                    enemy.ReceiveDamage(1f, new Vector2(0, 1));
                else if (player.GetDirection() == north)
                    enemy.ReceiveDamage(1f, new Vector2(0, -1));
            }
        }
    }
}
