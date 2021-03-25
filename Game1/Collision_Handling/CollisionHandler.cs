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

namespace Game1.Collision_Handling
{
    public class CollisionHandler
    {
        private readonly Dictionary<Tuple<Type, Type>, ICollisionCommand> collisionDict;
        
        public void HandleCollisions(List<Collision> collisions)
        {
            foreach(Collision collision in collisions)
            {
                Tuple<Type, Type> key = new Tuple<Type, Type>(collision.Collider.GetType().GetInterfaces()[0], collision.Collidee.GetType().GetInterfaces()[0]);

                try
                {
                    collisionDict[key].Execute(collision);
                }
                catch (Exception)
                {
                    Console.Error.WriteLine("Unhandled collision type: {0} -> {1}", key.Item1.Name, key.Item2.Name);
                }
            }
        }

        public CollisionHandler(Game1 game)
        {
            collisionDict = new Dictionary<Tuple<Type, Type>, ICollisionCommand>
            {
                // Player runs into wall
                {new Tuple<Type,Type>(typeof(IPlayer),typeof(IEnvironment)), new PlayerToBlockCommand(game) },

                // Player damages enemy
                {new Tuple<Type,Type>(typeof(IPlayer),typeof(IEnemy)), new PlayerToEnemy() },

                // Enemy damages player
                {new Tuple<Type,Type>(typeof(IEnemy),typeof(IPlayer)), new EnemyToPlayerCommand(game) },

                // Projectile hits player
                {new Tuple<Type,Type>(typeof(IProjectile),typeof(IPlayer)), new ProjectileToPlayerCommand() },

                // Projectile hits enemy
                {new Tuple<Type,Type>(typeof(IProjectile),typeof(IEnemy)), new ProjectileToEnemyCommand() },

                // Player hits Item
                {new Tuple<Type,Type>(typeof(IPlayer),typeof(IItem)), new LinkToItemCommand(game) },

                // Projectile hits Item
                {new Tuple<Type,Type>(typeof(IProjectile),typeof(IItem)), new ProjectileToItemCommand(game) },

                // Enemy runs into wall
                {new Tuple<Type,Type>(typeof(IEnemy),typeof(IEnvironment)), new EnemyToBlockCommand() },

                // Projectile to environment
                {new Tuple<Type,Type>(typeof(IProjectile),typeof(IEnvironment)), new ProjectileToEnvironmentCommand(game) },

                // Player collides with Player
                {new Tuple<Type,Type>(typeof(IPlayer),typeof(IPlayer)), new PlayerToPlayerCommand(game) },
            };
        }
    }
}
