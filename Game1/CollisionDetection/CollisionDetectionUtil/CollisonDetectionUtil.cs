using Game1.Environment;
using Game1.Util;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game1.CollisionDetection.CollisionDetectionUtil
{
    public static class CollisonDetectionUtil
    {
        public static List<IEnvironment> GetSingleCollisionObjects(List<IEnvironment> environmentList)
        {
            return environmentList.FindAll(e => !(e is PortalBlock portal && (portal.State == PortalBlockState.Blue || portal.State == PortalBlockState.Orange)) && !(e is LoadZone) && !(e is EnterBasementLoadZone) && !(e is ExitBasementLoadZone) && !(e is EnterDungeonLoadZone) && !(e is ExitDungeonLoadZone));
        }

        public static List<IEnvironment> GetMultiCollisionObjects(List<IEnvironment> environmentList)
        {
            return environmentList.FindAll(e => (e is PortalBlock portal && (portal.State == PortalBlockState.Blue || portal.State == PortalBlockState.Orange)) || (e is LoadZone) || (e is EnterBasementLoadZone) || (e is ExitBasementLoadZone) || (e is EnterDungeonLoadZone) || (e is ExitDungeonLoadZone));
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
