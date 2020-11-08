﻿using Game1.Collision_Handling;
using Game1.Enemy;
using Game1.Player;
using Game1.Projectile;
using Microsoft.Xna.Framework;
using System.CodeDom;
using System.ComponentModel;
using System.Reflection;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToEnemyWestSideCommand : ICollisionCommand
    {
        private const int boomerangStunTime = 10000; // ms
        private const int bombStunTime = 5000; // ms

        public ProjectileToEnemyWestSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IProjectile proj = (IProjectile)collision.collider;
            IEnemy enemy = (IEnemy)collision.collidee;
            Vector2 knockbackDirect = new Vector2(0,0);
            if (enemy.GetType() != typeof(Aquamentus) || proj.GetType() == typeof(Boomerang))
                knockbackDirect = new Vector2(1,0);

            if(proj.GetType() == typeof(Boomerang))
            {
                switch(enemy)
                {
                    case Jelly _:
                    case Bat _:
                        enemy.ReceiveDamage(.5f, knockbackDirect);
                        proj.BeginDespawn();
                        break;
                    case Aquamentus _:
                        break;
                    case Dodongo _:
                        enemy.ReceiveDamage(.5f, knockbackDirect);
                        proj.BeginDespawn();
                        break;
                    default:
                        enemy.StunnedTimer = boomerangStunTime;
                        proj.BeginDespawn();
                        break;
                }
            }
            else if(proj.GetType() == typeof(Arrow))
            {
                enemy.ReceiveDamage(2f, knockbackDirect);
                proj.BeginDespawn();
            }
            else if(proj.GetType() == typeof(SwordBeam))
            {
                enemy.ReceiveDamage(1f, knockbackDirect);
                proj.BeginDespawn();
            }
            else if(proj.GetType() == typeof(BombProjectile))
            {
                const int bombWidth = 12; // bomb's width before explosion
                const int bombHeight = 16; // bomb's height before explosion
                if (enemy.GetType() == typeof(Dodongo) && proj.GetHitbox().Width == bombWidth && proj.GetHitbox().Height == bombHeight)
                {
                    const int dodongoHeadWidth = 4;
                    if (collision.intersectionRec.Width < dodongoHeadWidth) {
                        enemy.ReceiveDamage(0f, knockbackDirect);
                        proj.BeginDespawn();
                    } else {
                        enemy.StunnedTimer = bombStunTime;
                        proj.BeginDespawn();
                    }
                } else if (proj.GetHitbox().Width != bombWidth && proj.GetHitbox().Height != bombHeight) {
                    enemy.ReceiveDamage(4f, knockbackDirect);
                }
            }
        }
    }
}
