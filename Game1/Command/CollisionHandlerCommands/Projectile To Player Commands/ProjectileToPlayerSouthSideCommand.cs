using Game1.Audio;
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
        private const char south = 'S';
        private const string shield = "shield";
        private readonly Vector2 southVector = new Vector2(0, -1);

        public ProjectileToPlayerSouthSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IProjectile proj = (IProjectile)collision.collider;
            IPlayer player = (IPlayer)collision.collidee;

            switch (proj)
            {
                case EnemyBoomerang _:

                    if (player.GetDirection() != south)
                    {
                        player.ReceiveDamage(boomerangDamage, southVector);
                    } else
                    {
                        AudioManager.PlayFireForget(shield);
                    }

                    proj.BeginDespawn();

                    break;

                case Fireballs _:

                    player.ReceiveDamage(fireballDamage, southVector);
                    proj.BeginDespawn();

                    break;
                case CandleFire _:
                    player.ReceiveDamage(fireballDamage, southVector);
                    break;
            }
        }
    }
}
