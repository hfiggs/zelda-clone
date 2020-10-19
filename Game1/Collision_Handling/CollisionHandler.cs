﻿
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
        static Dictionary<Tuple<Type, Type, char>, ICollisionCommand> collisionDict = new Dictionary<Tuple<Type, Type, char>, ICollisionCommand>
        {
            //player runs into wall (Jared) TODO
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IEnvironment),'E'), new LinkToBlockEastSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IEnvironment),'N'), new LinkToBlockNorthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IEnvironment),'S'), new LinkToBlockSouthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IEnvironment),'W'), new LinkToBlockWestSideCommand() },
            //Player damages enemy (Jared) TODO
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IEnemy),'E'), new LinkToEnemyEastSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IEnemy),'N'), new LinkToEnemyNorthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IEnemy),'S'), new LinkToEnemySouthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IEnemy),'W'), new LinkToEnemyWestSideCommand() },
            //Enemy damages player (Hunter) TODO
            {new Tuple<Type,Type,char>(typeof(IEnemy),typeof(IPlayer),'E'), new EnemyToLinkEastSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IEnemy),typeof(IPlayer),'N'), new EnemyToLinkNorthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IEnemy),typeof(IPlayer),'S'), new EnemyToLinkSouthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IEnemy),typeof(IPlayer),'W'), new EnemyToLinkWestSideCommand() },
            //Projectile hits player (Hunter) TODO
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IPlayer),'E'), new ProjectileToPlayerEastSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IPlayer),'N'), new ProjectileToPlayerNorthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IPlayer),'S'), new ProjectileToPlayerSouthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IPlayer),'W'), new ProjectileToPlayerWestSideCommand() },
            //Projectile hits enemy (Jared) TODO
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IEnemy),'E'), new ProjectileToEnemyEastSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IEnemy),'N'), new ProjectileToEnemyNorthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IEnemy),'S'), new ProjectileToEnemySouthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IEnemy),'W'), new ProjectileToEnemyWestSideCommand() },
            //Player hits Item (Hunter) TODO
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IItem),'E'), new LinkToItemCommand() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IItem),'N'), new LinkToItemCommand() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IItem),'S'), new LinkToItemCommand() },
            {new Tuple<Type,Type,char>(typeof(IPlayer),typeof(IItem),'W'), new LinkToItemCommand() },
            //Projectile hits Item (Hunter) TODO
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IItem),'E'), new ProjectileToItemCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IItem),'N'), new ProjectileToItemCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IItem),'S'), new ProjectileToItemCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IItem),'W'), new ProjectileToItemCommand() },
            //Enemy runs into wall (Jared) TODO
            {new Tuple<Type,Type,char>(typeof(IEnemy),typeof(IEnvironment),'E'), new EnemyToBlockEastSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IEnemy),typeof(IEnvironment),'N'), new EnemyToBlockNorthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IEnemy),typeof(IEnvironment),'S'), new EnemyToBlockSouthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IEnemy),typeof(IEnvironment),'W'), new EnemyToBlockWestSideCommand() },
            //Projectile to environment (Jared) TODO
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IEnvironment),'E'), new ProjectileToEnvironmentEastSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IEnvironment),'N'), new ProjectileToEnvironmentNorthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IEnvironment),'S'), new ProjectileToEnvironmentSouthSideCommand() },
            {new Tuple<Type,Type,char>(typeof(IProjectile),typeof(IEnvironment),'W'), new ProjectileToEnvironmentWestSideCommand() }

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