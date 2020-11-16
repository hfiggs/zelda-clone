using Game1.Enemy;
using Game1.RoomLoading;
using System;
using System.Collections.Generic;

namespace Game1.Item.ItemDropper
{
    class ItemDropper
    {
        private readonly Screen screen;
        private readonly List<Type> hasDrops;

        public ItemDropper(Screen screen)
        {
            hasDrops = new List<Type>()
            {
                typeof(Aquamentus),
                typeof(Dodongo),
                typeof(Goriya),
                typeof(Skeleton),
                typeof(Hand),
                typeof(Snake)
            };

            this.screen = screen;
        }

        public void SpawnDrops(List<IEnemy> enemies)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            const int randomMax = 4;
            int randomDrop = random.Next(randomMax);
            
            foreach (IEnemy e in enemies)
            {
                Type type;
                if (e.GetType() == typeof(EnemyDamageDecorator))
                {
                    EnemyDamageDecorator damaged = (EnemyDamageDecorator)e;
                    type = damaged.GetType();
                }
                else
                {
                    type = e.GetType();
                }
                if (hasDrops.Contains(type))
                {
                    switch (randomDrop)
                    {
                        case 0:
                            GetDrops(e);
                            break;
                        default:
                            break;
                    }
                }  
            }
        }

        private void GetDrops(IEnemy enemy)
        { 
            Random random = new Random(Guid.NewGuid().GetHashCode());
            var type = enemy.GetType();

            if (type == typeof(Goriya))
            {
                const int randomMax = 10, fairySpawnCase = 0, heartSpawnCase1 = 1, heartSpawnCase2 = 2, heartSpawnCase3 = 3, heartSpawnCase4 = 4;
                int randomDrop = random.Next(randomMax);
                switch (randomDrop)
                {
                    case fairySpawnCase:
                        screen.CurrentRoom.SpawnItem(new Fairy(enemy.GetPosition()));
                        break;
                    case heartSpawnCase1: case heartSpawnCase2: case heartSpawnCase3: case heartSpawnCase4:
                        screen.CurrentRoom.SpawnItem(new Heart(enemy.GetPosition()));
                        break;
                    default:
                        screen.CurrentRoom.SpawnItem(new RupeeYellow(enemy.GetPosition()));
                        break;
                }
            }
            else if (type == typeof(Skeleton) || type == typeof(Hand) || type == typeof(Snake))
            {
                const int randomMax = 10, clockSpawnCase = 0, heartSpawnCase1 = 1, heartSpawnCase2 = 2, blueRupeeSpawnCase1 = 3, blueRupeeSpawnCase2 = 4;
                int randomDrop = random.Next(randomMax);
                switch (randomDrop)
                {
                    case clockSpawnCase:
                        screen.CurrentRoom.SpawnItem(new Clock(enemy.GetPosition()));
                        break;
                    case heartSpawnCase1: case heartSpawnCase2:
                        screen.CurrentRoom.SpawnItem(new Heart(enemy.GetPosition()));
                        break;
                    case blueRupeeSpawnCase1: case blueRupeeSpawnCase2:
                        screen.CurrentRoom.SpawnItem(new RupeeBlue(enemy.GetPosition()));
                        break;
                    default:
                        screen.CurrentRoom.SpawnItem(new RupeeYellow(enemy.GetPosition()));
                        break;
                }
            }
            else if (type == typeof(Aquamentus) || type == typeof(Dodongo))
            {
                const int randomMax = 5, yellowRupeeSpawnCase = 0, fairySpawnCase = 1;
                int randomDrop = random.Next(randomMax);
                switch (randomDrop)
                {
                    case yellowRupeeSpawnCase:
                        screen.CurrentRoom.SpawnItem(new RupeeYellow(enemy.GetPosition()));
                        break;
                    case fairySpawnCase:
                        screen.CurrentRoom.SpawnItem(new Fairy(enemy.GetPosition()));
                        break;
                    default:
                        screen.CurrentRoom.SpawnItem(new Heart(enemy.GetPosition()));
                        break;
                }
            }
        }
    }
}
