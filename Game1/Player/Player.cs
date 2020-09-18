//Authors: Jared Perkins, Hunter Figgs


using Microsoft.Xna.Framework;
using System;

namespace Game1.Player
{
    class Player : IPlayer
    {
        int health;
        PlayerStateFactory stateFactory;
        //enum currentItem;
        IPlayerState state;

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public void MoveLeft()
        {
            throw new NotImplementedException();
        }

        public void MoveRight()
        {
            throw new NotImplementedException();
        }

        public void MoveUp()
        {
            throw new NotImplementedException();
        }

        public void MoveDown()
        {
            throw new NotImplementedException();
        }

        public void UseItem(int item)
        {
            throw new NotImplementedException();
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void ReceiveDamage()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime time)
        {
            throw new NotImplementedException();
        }

        public Rectangle GetLocation()
        {
            throw new NotImplementedException();
        }
    }
}
