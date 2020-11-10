using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Game1.Environment
{
    class EnvironmentListFactory
    {

        public static LinkedList<IEnvironment> GetEnvironmentList()
        {
            const int xAndY = 50;
            Vector2 position = new Vector2(xAndY, xAndY);

            LinkedList<IEnvironment> list = new LinkedList<IEnvironment>();

            list.AddLast(new DoorEBombable(position));

            return list;
        }
    }
}
