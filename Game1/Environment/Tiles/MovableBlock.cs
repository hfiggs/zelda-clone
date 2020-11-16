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
        //private char movementDirection;

        const int widthAndHeight = 16;
        private Rectangle hitbox1 = new Rectangle(0, 0, widthAndHeight, widthAndHeight);

        private List<Rectangle> hitboxes = new List<Rectangle>();
        public bool hasMoved;

        public MovableBlock(Vector2 position, char movementDirection)
        {
            sprite = EnvironmentSpriteFactory.instance.createBlock();
            this.position = position;
            hitbox1.Location += position.ToPoint();
            hitboxes.Add(hitbox1);
            //this.movementDirection = movementDirection;
            hasMoved = false;
        }

        public void Update(GameTime gameTime)
        {
            if(movementTime > 0)
            {
                position = position + movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                hitbox1.Location = position.ToPoint();
                hitboxes.Clear();
                hitboxes.Add(hitbox1);
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
            //calculated differently likely due to a float rounding/truncating error
            const float modifier1 = 15.5f, modifier2 = 16.0f;
            float moveX = movement.X * modifier1;
            float moveY = movement.Y * modifier1;
            if (movement.X > 0)
                moveX = movement.X * modifier2;
            if(movement.Y > 0)
                moveY = movement.Y * modifier2;
            movementSpeed = new Vector2(moveX, moveY) / seconds;
            hasMoved = true;
        }
    }
}
