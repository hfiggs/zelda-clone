﻿/* Author: Hunter Figgs.3 */

using Game1.Player;
using Game1.Sprite;

namespace Game1.GameState.GameStateUtil
{
    public enum PickupItem
    {
        None = 0,
        Arrow = 1,
        Bow = 2,
        Boomerang = 3,
        Triforce = 4
    }

    public static class PickupUtil
    {
        public static ISprite GetPlayerPickupSprite(PickupItem pickupItem, IPlayer player)
        {
            ISprite sprite;

            if (player.GetType() == typeof(Player1))
            {
                switch (pickupItem)
                {
                    case PickupItem.Triforce:
                        sprite = PlayerSpriteFactory.Instance.CreateTwoHandItemSprite();
                        break;
                    case PickupItem.None:
                    case PickupItem.Arrow:
                    case PickupItem.Bow:
                    case PickupItem.Boomerang:
                    default:
                        sprite = PlayerSpriteFactory.Instance.CreateOneHandItemSprite();
                        break;
                }
            } else {
                switch (pickupItem)
                {
                    case PickupItem.Triforce:
                        sprite = PlayerSpriteFactory.Instance.CreateZeldaTwoHandItemSprite();
                        break;
                    case PickupItem.None:
                    case PickupItem.Arrow:
                    case PickupItem.Bow:
                    case PickupItem.Boomerang:
                    default:
                        sprite = PlayerSpriteFactory.Instance.CreateZeldaOneHandItemSprite();
                        break;
                }
            }

            return sprite;
        }

        public static ISprite GetPickupItemSprite(PickupItem pickupItem)
        {
            ISprite sprite;

            switch (pickupItem)
            {
                case PickupItem.Arrow:
                    sprite = ItemSpriteFactory.Instance.CreateArrowItemSprite();
                    break;
                case PickupItem.Bow:
                    sprite = ItemSpriteFactory.Instance.CreateBowSprite();
                    break;
                case PickupItem.Boomerang:
                    sprite = ItemSpriteFactory.Instance.CreateBoomerangSprite();
                    break;
                case PickupItem.Triforce:
                    sprite = ItemSpriteFactory.Instance.CreateTriforceSprite();
                    break;
                case PickupItem.None:
                default:
                    sprite = ItemSpriteFactory.Instance.CreateNothingItemSprite();
                    break;
            }

            return sprite;
        }
    }
}
