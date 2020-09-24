/* Author:
 * Jared Perkins
 * Hunter Figgs */

using Microsoft.Xna.Framework;

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

        void Update(GameTime time);

        Vector2 GetPosition();

        char GetDirection();

        ISprite Sprite { get; }
    }
}
