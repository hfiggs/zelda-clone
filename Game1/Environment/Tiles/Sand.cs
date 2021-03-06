﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Environment
{
    class Sand : IEnvironment
    {
        private ISprite sprite;
        Vector2 position;

        private Rectangle hitbox1 = new Rectangle();
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public Sand(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.CreateSand();
            this.position = position;
            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
        }

public void Update(GameTime gameTime)
        {
            //throw new NotImplementedException("For later collision mechanics");
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public List<Rectangle> GetHitboxes()
        {
            return hitboxes;
        }
    }
}
