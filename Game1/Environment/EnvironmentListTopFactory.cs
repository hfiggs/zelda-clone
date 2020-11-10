using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Game1.Environment
{
    class EnvironmentListTopFactory
    {

        public static LinkedList<IEnvironment> GetEnvironmentList()
        {
            const float xAndY = 64f;
            Vector2 position = new Vector2(xAndY, xAndY);

            LinkedList<IEnvironment> list = new LinkedList<IEnvironment>();


            return list;
        }
    }
}
