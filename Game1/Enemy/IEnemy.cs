﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    public interface IEnemy
    {
        void ReceiveDamage();

        void Update(GameTime gameTime, Rectangle drawingLimits5);

        void Draw(SpriteBatch spriteBatch, Color color);

        void SetState(IEnemyState state);

        Rectangle GetHitbox();
    }
}
