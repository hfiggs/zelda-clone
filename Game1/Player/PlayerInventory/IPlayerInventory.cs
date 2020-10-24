namespace Game1.Player.PlayerInventory
{
    public interface IPlayerInventory
    {
        int HalfHeartCount { get; }

        void AddHealth(int halfHeartsToAdd);

        void SubHealth(int halfHeartsToSub);

        int MaxHalfHearts { get; }

        void AddMaxHearts();

        int BombCount { get; }

        void AddBomb();

        bool SubBomb();

        int RupeeCount { get; }

        void AddRupees(int rupeesToAdd);

        bool SubRupees(int rupeesToSub);

        int KeyCount { get; }

        void AddKey();

        bool SubKey();

        int TriforceCount { get; }

        void AddTriforce();

        bool HasBoomerang { get; set; }

        bool HasBow { get; set; }

        bool HasArrow { get; set; }

        bool HasCompass { get; set; }

        bool HasMap { get; set; }
    }
}
