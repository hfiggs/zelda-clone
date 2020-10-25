using Game1.Collision_Handling;
using Game1.Player;
using Game1.Projectile;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToPlayerSouthSideCommand : ICollisionCommand
    {
        private const int boomerangDamage = 2; // 1 full heart
        private const int fireballDamage = 1; // 1 half heart

        private readonly Vector2 southVector = new Vector2(0, -1);

        public ProjectileToPlayerSouthSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IProjectile proj = ((IProjectile)collision.collider);

            switch (proj)
            {
                case EnemyBoomerang _:
                    ((IPlayer)collision.collidee).ReceiveDamage(boomerangDamage, southVector);
                    proj.BeginDespawn();
                    break;
                case Fireballs _:
                    ((IPlayer)collision.collidee).ReceiveDamage(fireballDamage, southVector);
                    proj.BeginDespawn();
                    break;
            }
        }
    }
}
