using Game1.Enemy;
using Game1.Item;
using Game1.Player;
using System;
using System.Collections.Generic;

namespace Game1.Collision_Handling
{

    static class CollisionHandlerUtil
    {
        #region PLAYER_ITEM_PICKUP

        private const int fairyHalfHearts = 6;
        private const int heartHalfHearts = 2;

        private const int yellowRupee = 1;
        private const int blueRupee = 5;

        public static void HandlePlayerPickupItem(IPlayer player, IItem item)
        {
            switch (item)
            {
                case Bomb _:
                    player.PlayerInventory.AddBomb();
                    break;
                case Bow _:
                    player.PlayerInventory.HasBow = true;
                    break;
                case Clock _:
                    throw new NotImplementedException();
                case Compass _:
                    player.PlayerInventory.HasCompass = true;
                    break;
                case Fairy _:
                    player.PlayerInventory.AddHealth(fairyHalfHearts);
                    break;
                case Heart _:
                    player.PlayerInventory.AddHealth(heartHalfHearts);
                    break;
                case HeartContainer _:
                    player.PlayerInventory.AddMaxHeart();
                    break;
                case ItemBoomerang _:
                    player.PlayerInventory.HasBoomerang = true;
                    break;
                case Key _:
                    player.PlayerInventory.AddKey();
                    break;
                case Map _:
                    player.PlayerInventory.HasMap = true;
                    break;
                case RupeeYellow _:
                    player.PlayerInventory.AddRupees(yellowRupee);
                    break;
                case RupeeBlue _:
                    player.PlayerInventory.AddRupees(blueRupee);
                    break;
                case Triforce _:
                    player.PlayerInventory.AddTriforce();
                    break;
            }

            item.ShouldDelete = true;
        }

        #endregion

        #region ENEMY_DAMAGE

        private const int noDamage = 0;
        private const int halfHeart = 1;
        private const int fullHeart = 2;

        static private Dictionary<Type, int> enemyDamageDictionary = new Dictionary<Type, int>()
        {
            { typeof(Aquamentus), halfHeart },
            { typeof(Bat), halfHeart },
            { typeof(Dodongo), fullHeart },
            { typeof(Goriya), fullHeart },
            { typeof(Hand), noDamage },
            { typeof(Jelly), halfHeart },
            { typeof(Merchant), noDamage },
            { typeof(OldMan), noDamage },
            { typeof(Skeleton), halfHeart },
            { typeof(Snake), halfHeart },
            { typeof(SpikeTrap), halfHeart }
        };

        // returns enemy damage in half hearts
        public static int GetEnemyDamage(Type enemyType)
        {
            return enemyDamageDictionary.ContainsKey(enemyType) ? enemyDamageDictionary[enemyType] : 0;
        }

        #endregion
    }
}
