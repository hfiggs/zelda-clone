using Game1.Collision_Handling;
using Game1.Player;
using Game1.Projectile;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToPlayerNorthSideCommand : ICollisionCommand
    {
        private const int boomerangDamage = 2; // 1 full heart
        private const int fireballDamage = 1; // 1 half heart
        private const char north = 'N';
        private readonly Vector2 northVector = new Vector2(0, 1);

        public ProjectileToPlayerNorthSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IProjectile proj = (IProjectile)collision.collider;
            IPlayer player = (IPlayer)collision.collidee;

            switch (proj)
            {
                case EnemyBoomerang _:

                    if (player.GetDirection() != north)
                    {
                        player.ReceiveDamage(boomerangDamage, northVector);
                    } else
                    {
                        AudioManager.PlayFireForget("shield");
                    }

                    proj.BeginDespawn();

                    break;

                case Fireballs _:

                    player.ReceiveDamage(fireballDamage, northVector);
                    proj.BeginDespawn();

                    break;
            }
        }
    }
}
