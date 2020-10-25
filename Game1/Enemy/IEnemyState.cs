﻿using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    public interface IEnemyState
    {
        void Attack();

        void Update(GameTime gametime, Rectangle drawingLimits);

        void editPosition(Vector2 amount);

        void Draw(SpriteBatch spriteBatch, Color color);

        Vector2 GetPosition();

        Vector2 GetDirection();

        Rectangle GetHitbox();

        ISprite Sprite { get; }
    }
}
