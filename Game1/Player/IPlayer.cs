/* Authors:
 * Jared Perkins
 * Hunter Figgs */

using Game1.Player.PlayerInventory;
using Game1.Projectile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Player
{
    public interface IPlayer
    {
        IPlayerInventory PlayerInventory { get; }

        void MoveLeft();

        void MoveRight();

        void MoveUp();

        void MoveDown();

        void UseItem();

        void Attack();

        void ReceiveDamage(int halfHearts, Vector2 direction);

        void Update(GameTime time);

        void Draw(SpriteBatch spriteBatch, Color color);

        Rectangle GetLocation();

        char GetDirection();

        void SetState(IPlayerState state);

        void SpawnProjectile(IProjectile projectile);

        void EditPosition(Vector2 amount);

        Rectangle GetPlayerHitbox();

        Rectangle GetSwordHitbox();

        void SetSwordHitbox(Rectangle newHitbox);
    }
}
