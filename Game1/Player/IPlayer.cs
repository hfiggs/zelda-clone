//Authors: Jared Perkins, Hunter Figgs

using Microsoft.Xna.Framework;

namespace Game1.Player
{
    interface IPlayer
    {
        void MoveLeft();

        void MoveRight();

        void MoveUp();

        void MoveDown();

        void UseItem(int item);

        void Attack();

        void ReceiveDamage();

        void Draw();

    }
}
