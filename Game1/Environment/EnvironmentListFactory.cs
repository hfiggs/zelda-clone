using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Game1.Environment
{
    class EnvironmentListFactory
    {

        public static LinkedList<IEnvironment> GetEnvironmentList()
        {

            Vector2 position = new Vector2(0, 0);

            LinkedList<IEnvironment> list = new LinkedList<IEnvironment>();

            list.AddLast(new Block(position));

            return list;
        }
    }
}
