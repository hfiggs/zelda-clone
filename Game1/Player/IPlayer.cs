/* Authors:
 * Jared Perkins
 * Hunter Figgs */

using Microsoft.Xna.Framework;

namespace Game1.Player
{
    public interface IPlayer
    {
        void MoveLeft();

        void MoveRight();

        void MoveUp();

        void MoveDown();

        void UseItem(int item);

        void Attack();

        void ReceiveDamage();

        void Update(GameTime time);

        void Draw();

        Rectangle GetLocation();

    }
}
