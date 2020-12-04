﻿using Game1.Audio;
using Game1.Collision_Handling;
using Game1.Player;
using Game1.Projectile;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToPlayerWestSideCommand : ICollisionCommand
    {
        private const int boomerangDamage = 2; // 1 full heart
        private const int fireballDamage = 1; // 1 half heart
        private const char west = 'W';
        private const string shield = "shield";
        private readonly Vector2 westVector = new Vector2(1, 0);

        public ProjectileToPlayerWestSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IProjectile proj = (IProjectile)collision.collider;
            IPlayer player = (IPlayer)collision.collidee;

            switch (proj)
            {
                case EnemyBoomerang _:

                    if (player.GetDirection() != west)
                    {
                        player.ReceiveDamage(boomerangDamage, westVector);
                    } else
                    {
                        AudioManager.PlayFireForget(shield);
                    }

                    proj.BeginDespawn();

                    break;

                case Fireballs _:

                    player.ReceiveDamage(fireballDamage, westVector);
                    proj.BeginDespawn();

                    break;
                case CandleFire _:
                    player.ReceiveDamage(fireballDamage, westVector);
                    break;
            }
        }
    }
}
