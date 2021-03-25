using Game1.Audio;
using Game1.Collision_Handling;
using Game1.Player;
using Game1.Projectile;
using Game1.Util;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToPlayerCommand : ICollisionCommand
    {
        private const int boomerangDamage = 2; // 1 full heart
        private const int fireballDamage = 1; // 1 half heart
        private const string shield = "shield";

        public ProjectileToPlayerCommand() {}

        public void Execute(Collision collision)
        {
            IProjectile proj = (IProjectile)collision.Collider;
            IPlayer player = (IPlayer)collision.Collidee;

            switch (proj)
            {
                case EnemyBoomerang _:

                    if (player.GetDirection() != CompassDirectionUtil.GetDirectionCharCaps(collision.Side))
                    {
                        player.ReceiveDamage(boomerangDamage, CompassDirectionUtil.GetOppositeDirectionVector(collision.Side));
                    } 
                    else
                    {
                        AudioManager.PlayFireForget(shield);
                    }

                    proj.BeginDespawn();

                    break;

                case Fireballs _:

                    player.ReceiveDamage(fireballDamage, CompassDirectionUtil.GetOppositeDirectionVector(collision.Side));
                    proj.BeginDespawn();

                    break;
                case CandleFire _:
                    player.ReceiveDamage(fireballDamage, CompassDirectionUtil.GetOppositeDirectionVector(collision.Side));
                    break;
            }
        }
    }
}
