using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Environment
{
    class MovableBlock : IEnvironment
    {
        private ISprite sprite;
        private Vector2 position;
        private float movementTime;
        private Vector2 movementSpeed;

        private Rectangle hitbox1 = new Rectangle(0, 0, 32, 32);
        private List<Rectangle> hitboxes = new List<Rectangle>();

        public MovableBlock(Vector2 position)
        {
            sprite = EnvironmentSpriteFactory.instance.createBlock();
            this.position = position;
            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
        }

        public void BehaviorUpdate(GameTime gameTime)
        {
            if(movementTime > 0)
            {
                position = position + movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                movementTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public List<Rectangle> GetHitboxes()
        {
            return hitboxes;
        }

        public void Move(Vector2 movement, float seconds)
        {
            //movement is in units of tiles (16 pixels)
            movementTime = seconds;
            movementSpeed = (movement * 16.0f) / seconds;
        }
    }
}
