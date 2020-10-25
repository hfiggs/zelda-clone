

using Game1.Collision_Handling;
using Game1.Player;
using Game1.Projectile;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToPlayerEastSideCommand : ICollisionCommand
    {
        private const int boomerangDamage = 2; // 1 full heart
        private const int fireballDamage = 1; // 1 half heart

        private readonly Vector2 eastVector = new Vector2(-1, 0);

        public ProjectileToPlayerEastSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IProjectile proj = ((IProjectile)collision.collider);

            switch (proj)
            {
                case EnemyBoomerang _:
                    ((IPlayer)collision.collidee).ReceiveDamage(boomerangDamage, eastVector);
                    proj.BeginDespawn();
                    break;
                case Fireballs _:
                    ((IPlayer)collision.collidee).ReceiveDamage(fireballDamage, eastVector);
                    proj.BeginDespawn();
                    break;
            }
        }
    }
}
