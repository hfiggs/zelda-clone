
using Game1.Command.CollisionHandlerCommands;
using System;
using System.Collections.Generic;
using Game1.Player;
using Game1.Environment;
using Game1.Enemy;
using Game1.Projectile;
using Game1.Item;

namespace Game1.Collision_Handling
{
    static class CollisionHandler
    {
        private const char north = 'N', south = 'S', west = 'W', east = 'E';

        static Dictionary<Tuple<Type, Type, char>, ICollisionCommand> collisionDict = new Dictionary<Tuple<Type, Type, char>, ICollisionCommand>
        {
            //player runs into wall
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IEnvironment),east), new PlayerToBlockEastSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IEnvironment),north), new PlayerToBlockNorthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IEnvironment),south), new PlayerToBlockSouthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IEnvironment),west), new PlayerToBlockWestSideCommand() },
            //Player damages enemy
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IEnemy),east), new PlayerToEnemy() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IEnemy),north), new PlayerToEnemy() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IEnemy),south), new PlayerToEnemy() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IEnemy),west), new PlayerToEnemy() },
            //Enemy damages player
            {new Tuple<Type,Type,char>(typeof(IEnemy),typeof(IPlayer),east), new EnemyToPlayerEastSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IEnemy),typeof(IPlayer),north), new EnemyToPlayerNorthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IEnemy),typeof(IPlayer),south), new EnemyToPlayerSouthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IEnemy),typeof(IPlayer),west), new EnemyToPlayerWestSideCommand() },
            //Projectile hits player
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IPlayer),east), new ProjectileToPlayerEastSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IPlayer),north), new ProjectileToPlayerNorthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IPlayer),south), new ProjectileToPlayerSouthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IPlayer),west), new ProjectileToPlayerWestSideCommand() },
            //Projectile hits enemy
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IEnemy),east), new ProjectileToEnemyEastSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IEnemy),north), new ProjectileToEnemyNorthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IEnemy),south), new ProjectileToEnemySouthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IEnemy),west), new ProjectileToEnemyWestSideCommand() },
            //Player hits Item
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IItem),east), new LinkToItemCommand() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IItem),north), new LinkToItemCommand() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IItem),south), new LinkToItemCommand() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IItem),west), new LinkToItemCommand() },
            //Projectile hits Item
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IItem),east), new ProjectileToItemCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IItem),north), new ProjectileToItemCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IItem),south), new ProjectileToItemCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IItem),west), new ProjectileToItemCommand() },
            //Enemy runs into wall
            {new Tuple<Type,Type,char>(typeof(IEnemy),typeof(IEnvironment),east), new EnemyToBlockEastSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IEnemy),typeof(IEnvironment),north), new EnemyToBlockNorthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IEnemy),typeof(IEnvironment),south), new EnemyToBlockSouthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IEnemy),typeof(IEnvironment),west), new EnemyToBlockWestSideCommand() },
            //Projectile to environment
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IEnvironment),east), new ProjectileToEnvironmentEastSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IEnvironment),north), new ProjectileToEnvironmentNorthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IEnvironment),south), new ProjectileToEnvironmentSouthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IEnvironment),west), new ProjectileToEnvironmentWestSideCommand() }

        };
        static public void HandleCollisions(List<Collision> collisions)
        {
            foreach(Collision collision in collisions)
            {
                Tuple<Type, Type, char> key = new Tuple<Type, Type, char>(collision.collider.GetType().GetInterfaces()[0], collision.collidee.GetType().GetInterfaces()[0], collision.side);
                if(collisionDict.ContainsKey(key))
                {
                    collisionDict[key].Execute(collision);
                }
                else
                {
                    //TODO Remove before submission
                    throw (new NotSupportedException());
                }
            }

        }
    }
}
