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

        public PlayerInventory1()
        {
            HalfHeartCount = defaultHalfHearts;
            MaxHalfHearts = defaultHalfHearts;

            // everything else auto-initializes to 0 and false
        }

        public int HalfHeartCount { get; private set; }
        public int MaxHalfHearts { get; private set; }
        public int BombCount { get; private set; }
        public int RupeeCount { get; private set; }
        public int KeyCount { get; private set; }
        public int TriforceCount { get; private set; }
        public bool HasBoomerang { get; set; }
        public bool HasBow { get; set; }
        public bool HasArrow { get; set; }
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

        public void AddMaxHearts()
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

        public void AddTriforce()
        {
            if (TriforceCount < maxTriforceCount) TriforceCount++;
        }
    }
}
