using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Game1.Environment
{
    class EnvironmentListTopFactory
    {

        public static LinkedList<IEnvironment> GetEnvironmentList()
        {

            Vector2 position = new Vector2(64.0f, 64.0f);

            LinkedList<IEnvironment> list = new LinkedList<IEnvironment>();


            return list;
        }
    }
}
