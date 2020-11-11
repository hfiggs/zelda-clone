/* Author: Hunter Figgs.3 */

using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Environment
{
    class OverworldFloor : IEnvironment
    {
        private readonly ISprite sprite;
        private readonly Vector2 position;

        private readonly Rectangle hitbox1 = new Rectangle();
        private readonly List<Rectangle> hitboxes = new List<Rectangle>();

        public OverworldFloor(Vector2 position)
        {
            sprite = OverworldEnvironmentSpriteFactory.instance.CreateOverworldFloor();
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
            sprite.Draw(spriteBatch, position, color);
        }

        public List<Rectangle> GetHitboxes()
        {
            return hitboxes;
        }
    }
}
