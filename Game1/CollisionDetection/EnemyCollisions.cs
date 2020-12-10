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
        
        private readonly List<IEnemy> enemyList;
        private readonly List<IPlayer> players;
        private readonly List<IEnvironment> environmentList;
        private readonly List<Collision> collisionList;

        public EnemyCollisions(Screen screen)
        {
            collisionList = new List<Collision>();

            enemyList = screen.CurrentRoom.EnemyList;

            environmentList = screen.CurrentRoom.InteractEnviornment;

            players = screen.Players;
        }

        public List<Collision> GetCollisionList()
        {
            // Enemy collides with Player
            foreach (IEnemy enemy in enemyList)
            {
                foreach (Rectangle enemyHitbox in enemy.GetHitboxes())
                {
                    foreach (IPlayer player in players)
                    {
                        DetectionUtil.AddCollision(enemyHitbox, player.GetPlayerHitbox(), enemy, player, collisionList);
                    }
                }
            }

            // Enemy collides with Environment
            foreach (IEnemy enemy in enemyList)
            {
                foreach (Rectangle enemyHitbox in enemy.GetHitboxes())
                {
                    foreach (IEnvironment environment in environmentList)
                    {
                        foreach (Rectangle envHitbox in environment.GetHitboxes())
                        {
                            if (DetectionUtil.AddCollision(enemyHitbox, envHitbox, enemy, environment, collisionList))
                                goto NextEnemy;
                        }
                    }
                }
            NextEnemy:
                continue;
            }

            return collisionList;
        }
    }
}
