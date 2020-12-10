using Game1.Collision_Handling;
using Game1.Environment;
using Game1.Util;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game1.CollisionDetection.CollisionDetectionUtil
{
    public static class DetectionUtil
    {
        public static List<IEnvironment> GetSingleCollisionObjects(List<IEnvironment> environmentList)
        {
            return environmentList.FindAll(e => !(e is PortalBlock portal && (portal.State == PortalBlockState.Blue || portal.State == PortalBlockState.Orange)) && !(e is LoadZone) && !(e is EnterBasementLoadZone) && !(e is ExitBasementLoadZone) && !(e is EnterDungeonLoadZone) && !(e is ExitDungeonLoadZone));
        }

        public static List<IEnvironment> GetMultiCollisionObjects(List<IEnvironment> environmentList)
        {
            return environmentList.FindAll(e => (e is PortalBlock portal && (portal.State == PortalBlockState.Blue || portal.State == PortalBlockState.Orange)) || (e is LoadZone) || (e is EnterBasementLoadZone) || (e is ExitBasementLoadZone) || (e is EnterDungeonLoadZone) || (e is ExitDungeonLoadZone));
        }
        
        // If collision found between hitboxes, adds to list and returns true, otherwise returns false
        public static bool AddCollision(Rectangle colliderHitbox, Rectangle collideeHitbox, object collider, object collidee, List<Collision> collisionList)
        {
            Rectangle intersection = Rectangle.Intersect(colliderHitbox, collideeHitbox);
            if (!intersection.IsEmpty)
            {
                var side = DetermineSide(colliderHitbox, collideeHitbox, intersection);
                collisionList.Add(new Collision(side, intersection, collider, collidee));

                return true;
            }
            else
                return false;
        }

        public static CompassDirection DetermineSide(Rectangle colider, Rectangle colidee, Rectangle intersectionRec)
        {
            if (intersectionRec.Width > intersectionRec.Height)
            {
                return colider.Y < colidee.Y ? CompassDirection.North : CompassDirection.South;
            }
            else
            {
                return colider.X < colidee.X ? CompassDirection.West : CompassDirection.East;
            }
        }
    }
}
