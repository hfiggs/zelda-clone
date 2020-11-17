/* Author: Hunter Figgs.3 */

using Game1.Sprite;
using Game1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Environment
{
    class OverworldTreeTR : IEnvironment
    {
        private readonly ISprite sprite;
        private readonly Vector2 position;

        private const int width = 8;
        private const int height = 16;
        private readonly Rectangle hitbox1 = new Rectangle(0, 0, width, height);
        private readonly List<Rectangle> hitboxes = new List<Rectangle>();

        public OverworldTreeTR(Vector2 position)
        {
            sprite = OverworldEnvironmentSpriteFactory.instance.CreateOverworldTreeTR();
            this.position = position;
            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
        }

        public void Update(GameTime gameTime)
        {
            // Do nothing
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color, SpriteLayerUtil.envBelowPlayerLayer2);
        }

        public List<Rectangle> GetHitboxes()
        {
            return hitboxes;
        }
    }
}
