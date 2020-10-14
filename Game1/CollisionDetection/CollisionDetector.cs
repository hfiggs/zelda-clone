using System;
using System.Collections.Generic;

namespace Game1.CollisionDetection
{
    class CollisionDetector
    {
        private IRoom room;
        private List<Collision> collisionList;
        private Object collider, collidee;

        public CollisionDetector(IRoom room) 
        {
            this.room = room;
        }

        public List<Collision> GetCollisionList()
        {
            /* Cycle through room lists
             * if collision is true
             *      find intersection rec
             *      determine side
             *      create and add collision to list
             */ 

            return collisionList;
        }
    }
}
