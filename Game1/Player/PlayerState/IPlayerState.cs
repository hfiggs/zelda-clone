/* Author:
 * Jared Perkins
 * Hunter Figgs */

using Microsoft.Xna.Framework;

using Game1.Sprite;

namespace Game1.Player
{
    public interface IPlayerState
    {
        void MoveLeft();

        void MoveRight();

        void MoveUp();

        void MoveDown();

        void UseItem();

        void Attack();

        void Update(GameTime time);

        Vector2 position { get; set; }

        char GetDirection();

        ISprite Sprite { get; }
    }
}
