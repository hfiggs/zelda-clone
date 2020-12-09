/* Author: Hunter Figgs.3 */

using Game1.Enemy;
using Game1.RoomLoading;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
namespace Game1.Item.ItemUtil
{
    public static class ItemDropperUtil
    {
        private static readonly Random random = new Random(Guid.NewGuid().GetHashCode());

        private static readonly Dictionary<Type, Action<Screen, Vector2>> enemyMethodDict = new Dictionary<Type, Action<Screen, Vector2>>()
        {
            { typeof(Goriya), DropItems1 },
            { typeof(HardGoriya), DropItems1 },
            { typeof(Skeleton), DropItems2 },
            { typeof(Hand), DropItems2 },
            { typeof(Snake), DropItems1 },
            { typeof(ShootingSkeleton), DropItems2 },
            { typeof(HardSkeleton), DropItems2 },
            { typeof(Aquamentus), DropItems3 },
            { typeof(Dodongo), DropItems3 }
        };

        public static void DropItem(Screen screen, IEnemy enemy)
        {
            if (enemyMethodDict.ContainsKey(enemy.GetType()))
            {
                enemyMethodDict[enemy.GetType()].Invoke(screen, enemy.GetPosition());
            }
        }

        private static void DropItems1(Screen screen, Vector2 position)
        {
            const int randomMax = 10, bombCase1 = 0, bombCase2 = 1, bombCase3 = 3, heartSpawnCase1 = 4, heartSpawnCase2 = 5, heartSpawnCase3 = 6, clockCase1 = 7;
            int randomDrop = random.Next(randomMax);
            switch (randomDrop)
            {
                case bombCase1:
                case bombCase2:
                case bombCase3:
                    screen.CurrentRoom.SpawnItem(new Bomb(position));
                    break;
                case heartSpawnCase1:
                case heartSpawnCase2:
                case heartSpawnCase3:
                    screen.CurrentRoom.SpawnItem(new Heart(position));
                    break;
                case clockCase1:
                    screen.CurrentRoom.SpawnItem(new Clock(position));
                    break;
                default:
                    screen.CurrentRoom.SpawnItem(new RupeeYellow(position));
                    break;
            }
        }

        private static void DropItems2(Screen screen, Vector2 position)
        {
            const int randomMax = 10, clockSpawnCase = 0, heartSpawnCase1 = 1, heartSpawnCase2 = 2, blueRupeeSpawnCase1 = 3, blueRupeeSpawnCase2 = 4;
            int randomDrop = random.Next(randomMax);
            switch (randomDrop)
            {
                case clockSpawnCase:
                    screen.CurrentRoom.SpawnItem(new Clock(position));
                    break;
                case heartSpawnCase1:
                case heartSpawnCase2:
                    screen.CurrentRoom.SpawnItem(new Heart(position));
                    break;
                case blueRupeeSpawnCase1:
                case blueRupeeSpawnCase2:
                    screen.CurrentRoom.SpawnItem(new RupeeBlue(position));
                    break;
                default:
                    screen.CurrentRoom.SpawnItem(new RupeeYellow(position));
                    break;
            }
        }

        private static void DropItems3(Screen screen, Vector2 position)
        {
            const int randomMax = 5, yellowRupeeSpawnCase = 0, fairySpawnCase = 1;
            int randomDrop = random.Next(randomMax);
            switch (randomDrop)
            {
                case yellowRupeeSpawnCase:
                    screen.CurrentRoom.SpawnItem(new RupeeYellow(position));
                    break;
                case fairySpawnCase:
                    screen.CurrentRoom.SpawnItem(new Fairy(position));
                    break;
                default:
                    screen.CurrentRoom.SpawnItem(new Heart(position));
                    break;
            }
        }
    }
}
