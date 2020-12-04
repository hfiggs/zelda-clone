namespace Game1.Player.PlayerInventory
{
    public enum ItemEnum
    {
        None = 0,
        Bow = 1,
        Arrow = 2,
        Boomerang = 3,
        Bomb = 4,
        BluePotion = 5,
        BlueCandle = 6,
        PortalGun = 7
    }

    public interface IPlayerInventory
    {
        int HalfHeartCount { get; }

        void AddHealth(int halfHeartsToAdd);

        void SubHealth(int halfHeartsToSub);

        int MaxHalfHearts { get; }

        void AddMaxHeart();

        int BombCount { get; }

        void AddBomb();

        bool SubBomb();

        int RupeeCount { get; }

        void AddRupees(int rupeesToAdd);

        bool SubRupees(int rupeesToSub);

        int KeyCount { get; }

        void AddKey();

        bool SubKey();

        int BluePotionCount { get; }

        void AddBluePotion();

        bool SubBluePotion();

        int TriforceCount { get; }

        void AddTriforce();

        bool HasItem(ItemEnum item);

        void AddItem(ItemEnum item);

        ItemEnum EquippedItem { get; set; }

        bool IsItemInUse(ItemEnum item);

        void SetItemInUse(ItemEnum item, bool isInUse);

        void RefreshCandle();

        bool HasCompass { get; set; }

        bool HasMap { get; set; }
    }
}
