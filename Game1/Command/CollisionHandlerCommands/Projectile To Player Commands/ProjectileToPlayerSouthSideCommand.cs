﻿

using Game1.Collision_Handling;
using Game1.Player;
using Game1.Projectile;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToPlayerSouthSideCommand : ICollisionCommand
    {
        public ProjectileToPlayerSouthSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            ((IProjectile)collision.collider).BeginDespawn();
            ((IPlayer)collision.collidee).ReceiveDamage(new Vector2(0, -1));
        }
    }
}
