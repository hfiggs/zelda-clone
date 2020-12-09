using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Game1.Enemy;
using Game1.Player;
using Game1.Collision_Handling;
using Game1.RoomLoading;
using Game1.Environment;
using Game1.CollisionDetection.CollisionDetectionUtil;

namespace Game1.CollisionDetection
{
    class EnemyCollisions
    {
        
        private readonly List<IEnemy> EnemyList;
        private List<IPlayer> players;
        private Rectangle playerHitbox;
        private readonly List<IEnvironment> EnvironmentList;
        private readonly List<Collision> collisionList;

        public EnemyCollisions(Screen screen)
        {
            collisionList = new List<Collision>();

            EnemyList = screen.CurrentRoom.EnemyList;

            EnvironmentList = screen.CurrentRoom.InteractEnviornment;

            players = screen.Players;

        }

        public List<Collision> GetCollisionList()
        {
            // Enemy collides with Player
            foreach (IEnemy enemy in EnemyList)
            {
                foreach (Rectangle enemyHitbox in enemy.GetHitboxes())
                {
                    foreach (IPlayer player in players)
                    {
                        playerHitbox = player.GetPlayerHitbox();
                        Rectangle intersectPlayer = Rectangle.Intersect(enemyHitbox, playerHitbox);
                        if (!intersectPlayer.IsEmpty)
                        {
                            char side = CollisonDetectionUtil.DetermineSide(enemyHitbox, playerHitbox, intersectPlayer);
                            collisionList.Add(new Collision(side, intersectPlayer, enemy, player));
                        }
                    }
                }
            }

            foreach (IEnemy enemy in EnemyList)
            {
                bool collision = false;
                foreach (Rectangle enemyHitbox in enemy.GetHitboxes())
                {
                    foreach (IEnvironment environment in EnvironmentList)
                    {
                        foreach (Rectangle envHitbox in environment.GetHitboxes())
                        {
                            Rectangle intersectEnemy = Rectangle.Intersect(enemyHitbox, envHitbox);
                            if (!intersectEnemy.IsEmpty)
                            {
                                char side = CollisonDetectionUtil.DetermineSide(enemyHitbox, envHitbox, intersectEnemy);
                                collisionList.Add(new Collision(side, intersectEnemy, enemy, environment));
                                collision = true;
                                break;
                            }
                        }
                        if (collision)
                            break;
                    }
                    if (collision)
                        break;
                }
            }

            return collisionList;
        }
    }
}
