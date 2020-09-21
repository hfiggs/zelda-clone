/* Author:
 * Jared Perkins
 * Hunter Figgs */

using Microsoft.Xna.Framework;

using Game1.Sprite;
using Microsoft.Xna.Framework.Graphics;

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

        void Draw(SpriteBatch spriteBatch);

        ISprite Sprite { get; }

    }
}
