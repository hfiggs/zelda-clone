using Game1.Enemy;
using Game1.Item.ItemUtil;
using Game1.RoomLoading;
using System;
using System.Collections.Generic;

namespace Game1.Item.ItemDropper
{
    class ItemDropper
    {
        private readonly Screen screen;
        private readonly Random random;

        private const int randomMax = 4;
        private const int dropInt = 0;

        public ItemDropper(Screen screen)
        {
            this.screen = screen;
            random = new Random(Guid.NewGuid().GetHashCode());
        }

        public void SpawnDrops(List<IEnemy> enemies)
        {
            foreach (IEnemy enemy in enemies)
            {
                var randomDrop = random.Next(randomMax);

                if (randomDrop == dropInt)
                    ItemDropperUtil.DropItem(screen, enemy);
            }
        }
    }
}
