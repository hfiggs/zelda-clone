using Game1.Enemy;
using Game1.RoomLoading;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Item.ItemDropper
{
    class ItemDropper
    {
        private readonly Game1 game;
        private List<Type> hasDrops;

        public ItemDropper(Game1 game)
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
            this.game = game;

        }

        public void SpawnDrops(List<IEnemy> enemies)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int randomDrop = random.Next(4);
            
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
                int randomDrop = random.Next(10);
                switch (randomDrop)
                {
                    case 0:
                        game.Screen.SpawnItem(new Fairy(enemy.GetPosition()));
                        break;
                    case 1: case 2: case 3: case 4:
                        game.Screen.SpawnItem(new Heart(enemy.GetPosition()));
                        break;
                    default:
                        game.Screen.SpawnItem(new RupeeYellow(enemy.GetPosition()));
                        break;
                }
            }
            else if (type == typeof(Skeleton) || type == typeof(Hand) || type == typeof(Snake))
            {
                int randomDrop = random.Next(10);
                switch (randomDrop)
                {
                    case 0:
                        game.Screen.SpawnItem(new Clock(enemy.GetPosition()));
                        break;
                    case 1: case 2:
                        game.Screen.SpawnItem(new Heart(enemy.GetPosition()));
                        break;
                    case 3: case 4:
                        game.Screen.SpawnItem(new RupeeBlue(enemy.GetPosition()));
                        break;
                    default:
                        game.Screen.SpawnItem(new RupeeYellow(enemy.GetPosition()));
                        break;
                }
            }
            else if (type == typeof(Aquamentus) || type == typeof(Dodongo))
            {
                int randomDrop = random.Next(5);
                switch (randomDrop)
                {
                    case 0:
                        game.Screen.SpawnItem(new RupeeYellow(enemy.GetPosition()));
                        break;
                    case 1:
                        game.Screen.SpawnItem(new Fairy(enemy.GetPosition()));
                        break;
                    default:
                        game.Screen.SpawnItem(new Heart(enemy.GetPosition()));
                        break;
                }
            }
        }
    }
}
