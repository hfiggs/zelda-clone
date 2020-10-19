﻿/* Authors:
 * Jared Perkins
 * Hunter Figgs */

using Game1.Projectile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        void Draw(SpriteBatch spriteBatch, Color color);

        Rectangle GetLocation();

        char GetDirection();

        void SetState(IPlayerState state);

        int GetItem();

        void spawnProjectile(IProjectile projectile);

        void setItemUsable(int item);

        void setItemNotUsable();

        Rectangle GetPlayerHitbox();

        Rectangle GetSwordHitbox();

        void SetSwordHitbox(Rectangle newHitbox);
    }
}
