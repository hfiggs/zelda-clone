using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Game1.Item;
using Game1.Enemy;
using Game1.Player;
using Game1.Collision_Handling;
using Game1.RoomLoading;
using Game1.Environment;
using System;
using Game1.Util;

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

        private bool foundLoadZoneCollision = false;

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

        }

        // Collision order: player to item, player to enemy
        public List<Collision> GetCollisionList()
        {
            foreach (IPlayer player in players)
            {
                playerHitbox = player.GetPlayerHitbox();
                swordHitbox = player.GetSwordHitbox();
                // Player collides with Item
                foreach (IItem item in ItemList)
                {
                    Rectangle itemHitbox = item.GetHitbox();
                    Rectangle intersection = Rectangle.Intersect(itemHitbox, playerHitbox);
                    if (!intersection.IsEmpty)
                    {
                        char side = DetermineSide(playerHitbox, itemHitbox, intersection);
                        collisionList.Add(new Collision(side, intersection, player, item));
                    }
                    else
                    {
                        intersection = Rectangle.Intersect(itemHitbox, swordHitbox);
                        if (!intersection.IsEmpty && !InvalidSwordPickups.Contains(item.GetType()))
                        {
                            char side = DetermineSide(swordHitbox, itemHitbox, intersection);
                            collisionList.Add(new Collision(side, intersection, player, item));
                        }
                    }
                }

                // Player collides with Enemy
                foreach (IEnemy enemy in EnemyList)
                {
                    foreach (Rectangle enemyHitbox in enemy.GetHitboxes())
                    {
                        Rectangle intersectSword = Rectangle.Intersect(swordHitbox, enemyHitbox);
                        if (!intersectSword.IsEmpty)
                        {
                            char side = player.GetDirection();
                            collisionList.Add(new Collision(side, intersectSword, player, enemy));
                        }
                    }
                }

                bool collision = false;
                foreach (IEnvironment environment in EnvironmentList)
                {
                    foreach (Rectangle envHitbox in environment.GetHitboxes())
                    {
                        // Player collides with Environment
                        Rectangle intersectPlayer = Rectangle.Intersect(playerHitbox, envHitbox);
                        if (!intersectPlayer.IsEmpty)
                        {
                            char side = DetermineSide(playerHitbox, envHitbox, intersectPlayer);
                            collisionList.Add(new Collision(side, intersectPlayer, player, environment));
                            collision = true;
                            if(environment is LoadZone)
                            {
                                foundLoadZoneCollision = true;
                            }
                            break;
                        }
                    }
                    if (collision)
                        break;
                }

                //player intersecting with outside bounds
                foreach(Rectangle bound in roomBounds)
                {
                    Rectangle intersectPlayer = Rectangle.Intersect(playerHitbox, bound);
                    if(!intersectPlayer.IsEmpty)
                    {
                        char side = DetermineSide(playerHitbox, bound, intersectPlayer);
                        collisionList.Add(new Collision(side, intersectPlayer, player, EnvironmentList[0])); //environment object here is passed as a "dummy". There will always be at least 1 evnironment object
                        foundLoadZoneCollision = true;
                    }
                }
                player.requesting = foundLoadZoneCollision;

                Rectangle innerRoomInt = Rectangle.Intersect(playerHitbox, roomInner);
                if(!innerRoomInt.IsEmpty)
                {
                    RoomUtil.ExitDoor(player.playerID);
                }
                foundLoadZoneCollision = false;
            }
            return collisionList;
        }

        private char DetermineSide(Rectangle colider, Rectangle colidee, Rectangle intersectionRec)
        {
            const char north = 'N', south = 'S', west = 'W', east = 'E';
            int xOverlap = intersectionRec.Width;
            int yOverlap = intersectionRec.Height;
            char side;

            if (xOverlap > yOverlap)
            {
                if (colider.Y < colidee.Y)
                {
                    side = north;
                }
                else
                {
                    side = south;
                }
            }
            else
            {
                if (colider.X < colidee.X)
                {
                    side = west;
                }
                else
                {
                    side = east;
                }
            }
            return side;
        }
    }
}
