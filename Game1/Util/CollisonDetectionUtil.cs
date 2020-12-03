using Game1.Environment;
using System.Collections.Generic;

namespace Game1.Util
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
    }
}
