﻿using Game1.Enemy;
using Game1.GameState;
using Game1.Item;
using Game1.Player;
using Game1.Player.PlayerInventory;
using Game1.GameState.GameStateUtil;
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

        public static void HandlePlayerPickupItem(Game1 game, IPlayer player, IItem item)
        {
            switch (item)
            {
                case ArrowItem _:
                    foreach (IPlayer Player in game.Screen.Players)
                    {
                        Player.PlayerInventory.AddItem(ItemEnum.Arrow);
                    }
                    game.SetState(new GameStatePickupItem(game, PickupItem.Arrow, item, player));
                    break;
                case Bomb _:
                    player.PlayerInventory.AddBomb();
                    break;
                case Bow _:
                    foreach (IPlayer Player in game.Screen.Players)
                    {
                        Player.PlayerInventory.AddItem(ItemEnum.Bow);
                    }
                    game.SetState(new GameStatePickupItem(game, PickupItem.Bow, item, player));
                    break;
                case Clock _:
                    // ShouldDelete gets set to true
                    break;
                case Compass _:
                    foreach (IPlayer Player in game.Screen.Players) {
                        Player.PlayerInventory.HasCompass = true;
                    }
                    break;
                case Fairy _:
                    player.PlayerInventory.AddHealth(fairyHalfHearts);
                    break;
                case Heart _:
                    player.PlayerInventory.AddHealth(heartHalfHearts);
                    break;
                case HeartContainer _:
                    foreach (IPlayer Player in game.Screen.Players)
                    {
                        Player.PlayerInventory.AddMaxHeart();
                        Player.PlayerInventory.AddHealth(heartHalfHearts);
                    }
                    break;
                case ItemBoomerang _:
                    foreach (IPlayer Player in game.Screen.Players)
                    {
                        Player.PlayerInventory.AddItem(ItemEnum.Boomerang);
                    }
                    game.SetState(new GameStatePickupItem(game, PickupItem.Boomerang, item, player));
                    break;
                case Key _:
                    foreach (IPlayer Player in game.Screen.Players)
                    {
                        Player.PlayerInventory.AddKey();
                    }
                    break;
                case Map _:
                    foreach (IPlayer Player in game.Screen.Players)
                    {
                        Player.PlayerInventory.HasMap = true;
                    }
                    break;
                case RupeeYellow _:
                    player.PlayerInventory.AddRupees(yellowRupee);
                    break;
                case RupeeBlue _:
                    player.PlayerInventory.AddRupees(blueRupee);
                    break;
                case Triforce _:
                    player.PlayerInventory.AddTriforce();
                    player.PlayerInventory.AddHealth(fairyHalfHearts);
                    game.SetState(new GameStateWin(game, PickupItem.Triforce, item, player));
                    break;
                case BluePotion _:
                    player.PlayerInventory.AddBluePotion();
                    break;
                case BlueCandle _:
                    foreach (IPlayer Player in game.Screen.Players)
                    {
                        Player.PlayerInventory.AddItem(ItemEnum.BlueCandle);
                    }
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
            { typeof(HardGoriya), fullHeart },
            { typeof(Hand), noDamage },
            { typeof(Jelly), halfHeart },
            { typeof(Merchant), noDamage },
            { typeof(OldMan), noDamage },
            { typeof(Skeleton), halfHeart },
            { typeof(ShootingSkeleton), halfHeart },
            { typeof(HardSkeleton), fullHeart },
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
