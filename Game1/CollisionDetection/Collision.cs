using System;
using Microsoft.Xna.Framework;

namespace Game1.CollisionDetection
{
    class Collision
    {
        public char side { get; private set; } // Side consits of {N, S, E, W} (stands for North, South, East, West)
        public Rectangle intersectionRec { get; private set; }
        public Object collider { get; private set; }
        public Object collidee { get; private set; }

        public Collision(char side, Rectangle intersectionRec, Object collider, Object collidee)
        {
            this.side = side;
            this.intersectionRec = intersectionRec;
            this.collider = collider;
            this.collidee = collidee;
        }

        //strictly for debugging
        public override string ToString()
        {
            return "[" + collider.GetType().ToString() + " -> " + collidee.GetType().ToString() + ", " + intersectionRec.ToString() + ", " + side + "]";
        }
    }
}