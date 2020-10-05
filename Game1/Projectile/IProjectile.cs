﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Projectile
{
    public interface IProjectile
    {
        bool Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, Color color);
        bool Equals(Object obj);
    }
}
