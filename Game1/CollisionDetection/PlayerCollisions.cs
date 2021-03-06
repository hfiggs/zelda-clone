﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Game1.Item;
using Game1.Enemy;
using Game1.Player;
using Game1.Collision_Handling;
using Game1.RoomLoading;
using Game1.Environment;
using System;
using Game1.Util;
using Game1.CollisionDetection.CollisionDetectionUtil;

namespace Game1.CollisionDetection
{
    class PlayerCollisions
    {
        private readonly List<IPlayer> players;
        private Rectangle playerHitbox;
        private Rectangle swordHitbox;
        private readonly List<IItem> ItemList;
        private readonly List<IEnvironment> EnvironmentList;
        private readonly List<IEnemy> EnemyList;
        private readonly List<Collision> collisionList;

        private readonly List<Type> InvalidSwordPickups = new List<Type>() { typeof(Triforce), typeof(Key), typeof(Bow), };

        private bool[] foundLoadZoneCollision;
        private int innerLoadZoneLoopCounter, outerLoadZoneLoopCounter;

        //roombounds: 256, 216
        private readonly List<Rectangle> roomBounds = new List<Rectangle> { new Rectangle(-51, 0, 50, 216), new Rectangle(0, 216, 256, 50), new Rectangle(256, 0, 50, 216), new Rectangle(0, -51, 256, 50) };
        private readonly Rectangle roomInner = new Rectangle(13, 8, 227, 157);

        public PlayerCollisions(Screen screen)
        {
            collisionList = new List<Collision>();

            EnemyList = screen.CurrentRoom.EnemyList;

            ItemList = screen.CurrentRoom.ItemList;

            EnvironmentList = screen.CurrentRoom.InteractEnviornment;

            players = screen.Players;

            foundLoadZoneCollision = new bool[screen.Players.Count];
            outerLoadZoneLoopCounter = 0;
        }

        // Collision order: player to item, player to enemy
        public List<Collision> GetCollisionList()
        {
            foreach (IPlayer player in players)
            {
                playerHitbox = player.GetPlayerHitbox();
                swordHitbox = player.GetSwordHitbox();

                // Player/Sword collides with Item
                foreach (IItem item in ItemList)
                {
                    if (!DetectionUtil.AddCollision(playerHitbox, item.GetHitbox(), player, item, collisionList) && !InvalidSwordPickups.Contains(item.GetType()))
                        DetectionUtil.AddCollision(swordHitbox, item.GetHitbox(), player, item, collisionList);
                }

                // Sword collides with Enemy
                foreach (IEnemy enemy in EnemyList)
                {
                    foreach (Rectangle enemyHitbox in enemy.GetHitboxes())
                    {
                        DetectionUtil.AddCollision(swordHitbox, enemyHitbox, player, enemy, collisionList);
                    }
                }

                bool collision = false;
                foreach (IEnvironment environment in CollisionDetectionUtil.DetectionUtil.GetSingleCollisionObjects(EnvironmentList))
                {
                    foreach (Rectangle envHitbox in environment.GetHitboxes())
                    {
                        // Player collides with Environment
                        Rectangle intersectPlayer = Rectangle.Intersect(playerHitbox, envHitbox);
                        if (!intersectPlayer.IsEmpty)
                        {
                            var side = CollisionDetectionUtil.DetectionUtil.DetermineSide(playerHitbox, envHitbox, intersectPlayer);
                            collisionList.Add(new Collision(side, intersectPlayer, player, environment));
                            
                            if(environment is LoadZone)
                            {
                                foundLoadZoneCollision[outerLoadZoneLoopCounter] = true;
                            }

                            collision = true;
                            break;
                        }
                    }
                    if (collision)
                        break;
                }

                // Player collides with multi-collision environment
                foreach (IEnvironment environment in CollisionDetectionUtil.DetectionUtil.GetMultiCollisionObjects(EnvironmentList))
                {
                    foreach (Rectangle envHitbox in environment.GetHitboxes())
                    {
                        DetectionUtil.AddCollision(playerHitbox, envHitbox, player, environment, collisionList);
                    }
                }

                // Player collides with Player
                innerLoadZoneLoopCounter = 0;
                foreach (IPlayer player2 in players)
                {
                    Rectangle player2Hitbox = player2.GetPlayerHitbox();

                    Rectangle intersectPlayer = Rectangle.Intersect(playerHitbox, player2Hitbox);
                    if ((intersectPlayer.Width == playerHitbox.Width && intersectPlayer.Height == playerHitbox.Height) || player.requesting || player2.requesting) { 
                        // Do nothing if they are the same or if player is requesting
                    } else if (!intersectPlayer.IsEmpty)
                    {
                        var side = CollisionDetectionUtil.DetectionUtil.DetermineSide(playerHitbox, player2Hitbox, intersectPlayer);
                        collisionList.Add(new Collision(side, intersectPlayer, player, player2));
                        collision = true;
                        break;
                    }

                    innerLoadZoneLoopCounter++;

                    if (collision)
                        break;
                }

                //player intersecting with outside bounds
                foreach (Rectangle bound in roomBounds)
                {
                    Rectangle intersectPlayer = Rectangle.Intersect(playerHitbox, bound);
                    if(!intersectPlayer.IsEmpty)
                    {
                        var side = CollisionDetectionUtil.DetectionUtil.DetermineSide(playerHitbox, bound, intersectPlayer);
                        collisionList.Add(new Collision(side, intersectPlayer, player, EnvironmentList[0])); //environment object here is passed as a "dummy". There will always be at least 1 evnironment object
                        foundLoadZoneCollision[outerLoadZoneLoopCounter] = true;
                    }
                }
                player.requesting = foundLoadZoneCollision[outerLoadZoneLoopCounter];

                Rectangle innerRoomInt = Rectangle.Intersect(playerHitbox, roomInner);
                if(!innerRoomInt.IsEmpty)
                {
                    RoomUtil.ExitDoor(player.playerID);
                } else
                {
                    foundLoadZoneCollision[outerLoadZoneLoopCounter] = true;
                    player.requesting = true;
                }

                outerLoadZoneLoopCounter++;
            }
            return collisionList;
        }
    }
}
