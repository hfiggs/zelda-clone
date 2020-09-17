//Authors: Jared Perkins, Hunter Figgs


using Game1.Sprite;

namespace Game1.Player
{
    interface IPlayerState
    {
        void MoveLeft();

        void MoveRight();

        void MoveUp();

        void MoveDown();

        void UseItem();

        void Attack();

        void ReceiveDamage();

        ISprite getSprite();

    }
}
