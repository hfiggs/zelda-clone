using Game1.Environment;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game1.CollisionDetection.CollisionDetectionUtil
{
    public static class CollisonDetectionUtil
    {
        private static readonly char north = 'N', south = 'S', west = 'W', east = 'E';

        public static List<IEnvironment> GetSingleCollisionObjects(List<IEnvironment> environmentList)
        {
            return environmentList.FindAll(e => !(e is PortalBlock portal && (portal.State == PortalBlockState.Blue || portal.State == PortalBlockState.Orange)) && !(e is LoadZone) && !(e is EnterBasementLoadZone) && !(e is ExitBasementLoadZone) && !(e is EnterDungeonLoadZone) && !(e is ExitDungeonLoadZone));
        }

        public static List<IEnvironment> GetMultiCollisionObjects(List<IEnvironment> environmentList)
        {
            return environmentList.FindAll(e => (e is PortalBlock portal && (portal.State == PortalBlockState.Blue || portal.State == PortalBlockState.Orange)) || (e is LoadZone) || (e is EnterBasementLoadZone) || (e is ExitBasementLoadZone) || (e is EnterDungeonLoadZone) || (e is ExitDungeonLoadZone));
        }

        public static char DetermineSide(Rectangle colider, Rectangle colidee, Rectangle intersectionRec)
        {
            if (intersectionRec.Width > intersectionRec.Height)
            {
                return colider.Y < colidee.Y ? north : south;
            }
            else
            {
                return colider.X < colidee.X ? west : east;
            }
        }
    }
}
