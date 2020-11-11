using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1.Environment
{
    class InvisibleWall : IEnvironment
    {
        private ISprite sprite;
        Vector2 position;

        private List<Rectangle> hitboxes = new List<Rectangle>();

        public InvisibleWall(Vector2 position, Vector2 hitboxSize)
        {
            sprite = EnvironmentSpriteFactory.instance.createBlack();
            this.position = position;
            hitboxes.Add(new Rectangle(position.ToPoint(), hitboxSize.ToPoint()));
        }

        public void Update(GameTime gameTime)
        {
            //throw new NotImplementedException("For later collision mechanics");
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            //Nothing to draw. This is invisible
        }

        public List<Rectangle> GetHitboxes()
        {
            return hitboxes;
        }
    }
}
