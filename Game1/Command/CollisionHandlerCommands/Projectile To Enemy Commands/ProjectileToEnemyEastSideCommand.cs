using Game1.Collision_Handling;
using Game1.Enemy;
using Game1.Player;
using Game1.Projectile;
using Microsoft.Xna.Framework;

namespace Game1.Command.CollisionHandlerCommands
{
    class ProjectileToEnemyEastSideCommand : ICollisionCommand
    {
        private const int boomerangStunTime = 10000; // ms
        private const int bombStunTime = 5000; // ms
        private const float halfHeart = 0.5f, oneHeart = 1.0f, twoHearts = 2.0f, fourHearts = 4.0f;

        public ProjectileToEnemyEastSideCommand()
        {

        }

        public void Execute(Collision collision)
        {
            IProjectile proj = (IProjectile)collision.collider;
            IEnemy enemy = (IEnemy)collision.collidee;
            Vector2 knockbackDirect = new Vector2(0, 0);
            if (enemy.GetType() != typeof(Aquamentus) || proj.GetType() == typeof(Boomerang))
                knockbackDirect = new Vector2(-1, 0);

            if (collision.collider.GetType() == typeof(Boomerang))
            {
                switch (enemy)
                {
                    case Jelly _:
                    case Bat _:
                        enemy.ReceiveDamage(halfHeart, knockbackDirect);
                        proj.BeginDespawn();
                        break;
                    case Aquamentus _:
                        break;
                    case Dodongo _:
                        enemy.ReceiveDamage(halfHeart, knockbackDirect);
                        proj.BeginDespawn();
                        break;
                    default:
                        if(enemy.StunnedTimer != int.MaxValue)
                        {
                            enemy.StunnedTimer = boomerangStunTime;
                        }
                        proj.BeginDespawn();
                        break;
                }
            }
            else if (proj.GetType() == typeof(Arrow))
            {
                enemy.ReceiveDamage(twoHearts, knockbackDirect);
                proj.BeginDespawn();
            }
            else if (proj.GetType() == typeof(SwordBeam))
            {
                enemy.ReceiveDamage(oneHeart, knockbackDirect);
                proj.BeginDespawn();
            }
            else if (proj.GetType() == typeof(BombProjectile))
            {
                const int bombWidth = 16; // bomb's width before explosion
                const int bombHeight = 16; // bomb's height before explosion
                if (enemy.GetType() == typeof(Dodongo) && proj.GetHitbox().Width == bombWidth && proj.GetHitbox().Height == bombHeight)
                {
                    const int dodongoHeadWidth = 4;
                    if (collision.intersectionRec.Width < dodongoHeadWidth)
                    {
                        enemy.ReceiveDamage(0f, knockbackDirect);
                        proj.BeginDespawn();
                    } else {
                        enemy.StunnedTimer = bombStunTime;
                        proj.BeginDespawn();
                    }
                } else if (proj.GetHitbox().Width != bombWidth && proj.GetHitbox().Height != bombHeight) {
                    enemy.ReceiveDamage(fourHearts, knockbackDirect);
                }
            } else if (proj.GetType() == typeof(CandleFire)) {
                enemy.ReceiveDamage(halfHeart, knockbackDirect);
            }
        }
    }
}
