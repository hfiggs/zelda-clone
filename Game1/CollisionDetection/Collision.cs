using System;
using Microsoft.Xna.Framework;

namespace Game1.CollisionDetection
{
    class Collision
    {
        private char side;
        private Rectangle intersectionRec;
        private Object collider;
        private Object collidee;

        public Collision(char side, Rectangle intersectionRec, Object collider, Object collidee)
        {
            this.side = side;
            this.intersectionRec = intersectionRec;
            this.collider = collider;
            this.collidee = collidee;
        }

        public char GetSide()
        {
            return side;
        }

        public Rectangle GetRectangle()
        {
            return intersectionRec;
        }

        public Object GetCollider()
        {
            return collider;
        }

        public Object GetCollidee()
        {
            return collidee;
        }
    }
}
