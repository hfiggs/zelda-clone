using System;

namespace Game1.Player.PlayerInventory
{
    class PlayerInventory1 : IPlayerInventory
    {
        private const int defaultHalfHearts = 6; // 3 full hearts
        private const int maxMaxHalfHearts = 26; // 16 full hearts (max from all heart pieces)
        private const int fullHeart = 2; // 2 half hearts = 1 full heart

        private const int maxBombCount = 8;
        private const int maxRupeeCount = 255;
        private const int maxKeyCount = 255;
        private const int maxTriforceCount = 8;
        private const int maxBluePotionCount = 1;

        private const int numItems = 8;
        private bool[] hasItem; // based on ItemEnum (None, Bow, Arrow, Boomerang, Bomb, BluePotion, BlueCandle, PortalGun)
        private bool[] isItemInUse;

        public PlayerInventory1()
        {
            HalfHeartCount = defaultHalfHearts;
            MaxHalfHearts = defaultHalfHearts;

            //hasItem = new bool[numItems] { true, false, false, false, true, false, false , true };

            hasItem = new bool[numItems] { true, true, true, true, true, false, false , true };
            RupeeCount = 16;

            isItemInUse = new bool[numItems] { false, false, false, false, false, false, false , false };

            BombCount = maxBombCount;

            // everything else auto-initializes to 0 or false
        }

        public int HalfHeartCount { get; private set; }
        public int MaxHalfHearts { get; private set; }
        public int BombCount { get; private set; }
        public int RupeeCount { get; private set; }
        public int KeyCount { get; private set; }
        public int TriforceCount { get; private set; }
        public int BluePotionCount { get; private set; }
        public ItemEnum EquippedItem { get; set; }
        public bool ItemInUse { get; set; }
        public bool HasCompass { get; set; }
        public bool HasMap { get; set; }

        public void AddHealth(int halfHeartsToAdd)
        {
            HalfHeartCount = Math.Min((HalfHeartCount + halfHeartsToAdd), MaxHalfHearts);
        }

        public void SubHealth(int halfHeartsToSub)
        {
            HalfHeartCount = Math.Max((HalfHeartCount - halfHeartsToSub), 0);
        }

        public void AddMaxHeart()
        {
            if (MaxHalfHearts < maxMaxHalfHearts) MaxHalfHearts += fullHeart;
        }

        public void AddBomb()
        {
            if (BombCount < maxBombCount) BombCount++;
        }

        public bool SubBomb()
        {
            if (BombCount == 0)
            {
                return false;
            }
            else
            {
                BombCount--;
                return true;
            }
        }

        public void AddRupees(int rupeesToAdd)
        {
            RupeeCount = Math.Min((RupeeCount + rupeesToAdd), maxRupeeCount);
        }

        public bool SubRupees(int rupeesToSub)
        {
            if (RupeeCount - rupeesToSub < 0)
            {
                return false;
            }
            else
            {
                RupeeCount -= rupeesToSub;
                return true;
            }
        }

        public void AddKey()
        {
            if (KeyCount < maxKeyCount) KeyCount++;
        }

        public bool SubKey()
        {
            if (KeyCount == 0)
            {
                return false;
            }
            else
            {
                KeyCount--;
                return true;
            }
        }

        public void AddBluePotion()
        {
            if (BluePotionCount < maxBluePotionCount) BluePotionCount++;
            hasItem[(int)ItemEnum.BluePotion] = true;
        }

        public bool SubBluePotion()
        {
            if (BluePotionCount == 0)
            {
                return false;
            }
            else
            {
                BluePotionCount--;
                hasItem[(int)ItemEnum.BluePotion] = false;
                return true;
            }
        }

        public void AddTriforce()
        {
            if (TriforceCount < maxTriforceCount) TriforceCount++;
        }

        public bool HasItem(ItemEnum item)
        {
            return hasItem[(int)item];
        }

        public void AddItem(ItemEnum item)
        {
            hasItem[(int)item] = true;
        }

        public bool IsItemInUse(ItemEnum item)
        {
            return isItemInUse[(int)item];
        }

        public void SetItemInUse(ItemEnum item, bool isInUse)
        {
            isItemInUse[(int)item] = isInUse;
        }

        public void RefreshCandle()
        {
            this.SetItemInUse(ItemEnum.BlueCandle, false);
        }
    }
}
