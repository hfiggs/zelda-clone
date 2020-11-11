/* Author: Hunter Figgs.3 */

using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Environment
{

    class OverworldFloorTile : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;

        private Rectangle hitbox1 = new Rectangle();
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public OverworldFloorTile(Vector2 position)
        {
            sprite = OverworldEnvironmentSpriteFactory.instance.CreateOverworldFloorTile();
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
