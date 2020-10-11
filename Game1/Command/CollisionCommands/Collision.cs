using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Command.CollisionCommands
{
    class Collision
    {
        public char side { get; private set; }

        private Rectangle CollisionRect;

        public Object Collider1 { get; private set; }
        public Object Collider2 { get; private set; }

        public Collision(Object Collider1, Object Collider2)
        {
            this.Collider1 = Collider1;
            this.Collider2 = Collider2;

            //calculate collision rect, side, etc. - this is being completed in the collision branch
        }

    }
}
