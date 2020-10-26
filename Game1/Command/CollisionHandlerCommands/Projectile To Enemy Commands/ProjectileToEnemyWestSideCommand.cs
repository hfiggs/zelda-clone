

using Game1.Collision_Handling;
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
                if (enemy.GetType() == typeof(Jelly))
                {
                    enemy.ReceiveDamage(.5f, knockbackDirect);
                    proj.BeginDespawn();
                }
                else if (enemy.GetType() == typeof(Bat))
                {
                    enemy.ReceiveDamage(.5f, knockbackDirect);
                    proj.BeginDespawn();
                }
                else
                {
                    throw new System.NotImplementedException();
                    proj.BeginDespawn();
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
                enemy.ReceiveDamage(4f, knockbackDirect);
                proj.BeginDespawn();
            }
        }
    }
}
