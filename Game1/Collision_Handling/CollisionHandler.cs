/* Authors: 
 * 
 * Hunter Figgs 
 * Jared Perkins
 */

using Game1.Command.CollisionHandlerCommands;
using System;
using System.Collections.Generic;
using Game1.Player;
using Game1.Environment;
using Game1.Enemy;
using Game1.Projectile;
using Game1.Item;
using Game1.Util;

namespace Game1.Collision_Handling
{
    public class CollisionHandler
    {
        private const CompassDirection north = CompassDirection.North, south = CompassDirection.South, west = CompassDirection.West, east = CompassDirection.East;

        private readonly Dictionary<Tuple<Type, Type, CompassDirection>, ICollisionCommand> collisionDict;
        
        public void HandleCollisions(List<Collision> collisions)
        {
            foreach(Collision collision in collisions)
            {
                Tuple<Type, Type, CompassDirection> key = new Tuple<Type, Type, CompassDirection>(collision.Collider.GetType().GetInterfaces()[0], collision.Collidee.GetType().GetInterfaces()[0], collision.Side);
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

        public CollisionHandler(Game1 game)
        {
            collisionDict = new Dictionary<Tuple<Type, Type, CompassDirection>, ICollisionCommand>
            {
                //player runs into wall
                {new Tuple<Type,Type,CompassDirection>(typeof(IPlayer),typeof(IEnvironment),east), new PlayerToBlockCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IPlayer),typeof(IEnvironment),north), new PlayerToBlockCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IPlayer),typeof(IEnvironment),south), new PlayerToBlockCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IPlayer),typeof(IEnvironment),west), new PlayerToBlockCommand(game) },
                //Player damages enemy
                {new Tuple<Type,Type,CompassDirection>(typeof(IPlayer),typeof(IEnemy),east), new PlayerToEnemy() },
                {new Tuple<Type,Type,CompassDirection>(typeof(IPlayer),typeof(IEnemy),north), new PlayerToEnemy() },
                {new Tuple<Type,Type,CompassDirection>(typeof(IPlayer),typeof(IEnemy),south), new PlayerToEnemy() },
                {new Tuple<Type,Type,CompassDirection>(typeof(IPlayer),typeof(IEnemy),west), new PlayerToEnemy() },
                //Enemy damages player
                {new Tuple<Type,Type,CompassDirection>(typeof(IEnemy),typeof(IPlayer),east), new EnemyToPlayerEastSideCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IEnemy),typeof(IPlayer),north), new EnemyToPlayerNorthSideCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IEnemy),typeof(IPlayer),south), new EnemyToPlayerSouthSideCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IEnemy),typeof(IPlayer),west), new EnemyToPlayerWestSideCommand(game) },
                //Projectile hits player
                {new Tuple<Type,Type,CompassDirection>(typeof(IProjectile),typeof(IPlayer),east), new ProjectileToPlayerEastSideCommand() },
                {new Tuple<Type,Type,CompassDirection>(typeof(IProjectile),typeof(IPlayer),north), new ProjectileToPlayerNorthSideCommand() },
                {new Tuple<Type,Type,CompassDirection>(typeof(IProjectile),typeof(IPlayer),south), new ProjectileToPlayerSouthSideCommand() },
                {new Tuple<Type,Type,CompassDirection>(typeof(IProjectile),typeof(IPlayer),west), new ProjectileToPlayerWestSideCommand() },
                //Projectile hits enemy
                {new Tuple<Type,Type,CompassDirection>(typeof(IProjectile),typeof(IEnemy),east), new ProjectileToEnemyEastSideCommand() },
                {new Tuple<Type,Type,CompassDirection>(typeof(IProjectile),typeof(IEnemy),north), new ProjectileToEnemyNorthSideCommand() },
                {new Tuple<Type,Type,CompassDirection>(typeof(IProjectile),typeof(IEnemy),south), new ProjectileToEnemySouthSideCommand() },
                {new Tuple<Type,Type,CompassDirection>(typeof(IProjectile),typeof(IEnemy),west), new ProjectileToEnemyWestSideCommand() },
                //Player hits Item
                {new Tuple<Type,Type,CompassDirection>(typeof(IPlayer),typeof(IItem),east), new LinkToItemCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IPlayer),typeof(IItem),north), new LinkToItemCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IPlayer),typeof(IItem),south), new LinkToItemCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IPlayer),typeof(IItem),west), new LinkToItemCommand(game) },
                //Projectile hits Item
                {new Tuple<Type,Type,CompassDirection>(typeof(IProjectile),typeof(IItem),east), new ProjectileToItemCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IProjectile),typeof(IItem),north), new ProjectileToItemCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IProjectile),typeof(IItem),south), new ProjectileToItemCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IProjectile),typeof(IItem),west), new ProjectileToItemCommand(game) },
                //Enemy runs into wall
                {new Tuple<Type,Type,CompassDirection>(typeof(IEnemy),typeof(IEnvironment),east), new EnemyToBlockEastSideCommand() },
                {new Tuple<Type,Type,CompassDirection>(typeof(IEnemy),typeof(IEnvironment),north), new EnemyToBlockNorthSideCommand() },
                {new Tuple<Type,Type,CompassDirection>(typeof(IEnemy),typeof(IEnvironment),south), new EnemyToBlockSouthSideCommand() },
                {new Tuple<Type,Type,CompassDirection>(typeof(IEnemy),typeof(IEnvironment),west), new EnemyToBlockWestSideCommand() },
                //Projectile to environment
                {new Tuple<Type,Type,CompassDirection>(typeof(IProjectile),typeof(IEnvironment),east), new ProjectileToEnvironmentCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IProjectile),typeof(IEnvironment),north), new ProjectileToEnvironmentCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IProjectile),typeof(IEnvironment),south), new ProjectileToEnvironmentCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IProjectile),typeof(IEnvironment),west), new ProjectileToEnvironmentCommand(game) },
                //Player collides with Player
                {new Tuple<Type,Type,CompassDirection>(typeof(IPlayer),typeof(IPlayer),east), new PlayerToPlayerCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IPlayer),typeof(IPlayer),north), new PlayerToPlayerCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IPlayer),typeof(IPlayer),south), new PlayerToPlayerCommand(game) },
                {new Tuple<Type,Type,CompassDirection>(typeof(IPlayer),typeof(IPlayer),west), new PlayerToPlayerCommand(game) }

            };
        }
    }
}
