using Game1.Util;
using Microsoft.Xna.Framework;

namespace Game1.Collision_Handling
{
    public class Collision
    {
        public CompassDirection Side { get; private set; }
        public Rectangle IntersectionRec { get; private set; }
        public object Collider { get; private set; }
        public object Collidee { get; private set; }

        public Collision(CompassDirection side, Rectangle intersectionRec, object collider, object collidee)
        {
            Side = side;
            IntersectionRec = intersectionRec;
            Collider = collider;
            Collidee = collidee;
        }

        // Strictly for debugging
        public override string ToString()
        {
            return "[" + Collider.GetType().ToString() + " -> " + Collidee.GetType().ToString() + ", " + IntersectionRec.ToString() + ", " + Side + "]";
        }
    }
}