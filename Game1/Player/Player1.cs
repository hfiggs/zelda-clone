/* Authors:
 * Jared Perkins
 * Hunter Figgs */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Player
{
    class Player1 : IPlayer
    {
       //private Vector2 position;
        //enum currentItem;

        public Player1(Vector2 position, SpriteBatch spriteBatch)
        {
            //this.position = position;
            PlayerStateFactory.Instance.Initialize(spriteBatch, position);
        }

        public void Draw()
        {
            PlayerStateFactory.Instance.Draw();
        }

        public void MoveLeft()
        {
            PlayerStateFactory.Instance.MoveLeft();
        }

        public void MoveRight()
        {
            PlayerStateFactory.Instance.MoveRight();
        }

        public void MoveUp()
        {
            PlayerStateFactory.Instance.MoveUp();
        }

        public void MoveDown()
        {
            PlayerStateFactory.Instance.MoveDown();
        }

        public void UseItem(int item)
        {
            PlayerStateFactory.Instance.UseItem();
        }

        public void Attack()
        {
            PlayerStateFactory.Instance.Attack();
        }

        public void ReceiveDamage()
        {
            PlayerStateFactory.Instance.ReceiveDamage();
        }

        public void Update(GameTime time)
        {
            PlayerStateFactory.Instance.Update(time);
        }

        public Rectangle GetLocation()
        {
            throw new NotImplementedException();
        }
    }
}
