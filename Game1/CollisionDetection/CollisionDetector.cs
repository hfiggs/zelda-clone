using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Game1;
using Game1.Item;
using Game1.Environment;
using Game1.Enemy;
using Game1.Projectile;
using Game1.Player;

namespace Game1.CollisionDetection
{
    class CollisionDetector
    {
        private Room room;
        private LinkedList<Collision> collisionList;
        //private Object collider, collidee;

        public CollisionDetector(Room room)
        {
            this.room = room;
        }

        public LinkedList<Collision> GetCollisionList()
        {
            collisionList = new LinkedList<Collision>();
            LinkedList<IItem> ItemList = room.ItemList;
            LinkedList<IEnvironment> EnvironmentList = room.EnvironmentList;
            LinkedList<IEnemy> EnemyList = room.EnemyList;
            LinkedList<IProjectile> ProjectileList = room.ProjectileList;
            IPlayer player = room.Link;
            Rectangle playerRec = room.Link.GetPlayerHitbox();

            foreach (IEnvironment environment in EnvironmentList)
            { // player colliding with environment

                //some environment objects have multiple hitboxes
                foreach (Rectangle envRect in environment.GetHitboxes())
                {
                    //Environment collides with player
                    Rectangle intersectPlayer = Rectangle.Intersect(playerRec, envRect);
                    if (!intersectPlayer.IsEmpty)
                    {
                        char side = DetermineSide(playerRec, envRect, intersectPlayer);
                        collisionList.AddLast(new Collision(side, intersectPlayer, player, environment));
                    }

                    //--Waiting for Enemy to have a hitbox
                    //Environment collides with Enemy
                    /*foreach (IEnemy enemy in EnemyList)
                    {
                        foreach (Rectangle enemyRect in enemy.GetHitboxes())
                        {
                            Rectangle intersectEnemy = Rectangle.Intersect(enemyRect, envRect);
                            if (!intersectEnemy.IsEmpty)
                            {
                                char side = DetermineSide(enemyRect, envRect, intersectPlayer);
                                collisionList.Add(new Collision(side, intersectEnemy, enemy, environment));
                            }
                        }
                    }*/
                }
            }

            //Only player collides with item
            foreach (IItem item in ItemList)
            {
                Rectangle itemHitbox = item.GetHitbox();
                Rectangle intersection = Rectangle.Intersect(itemHitbox, playerRec);
                if(!intersection.IsEmpty)
                {
                    char side = DetermineSide(playerRec, itemHitbox, intersection);
                    collisionList.AddLast(new Collision(side, intersection, player, item));
                }
            }

            /*
            foreach (IEnemy enemy in EnemyList)
            { // enemy attacking player
                if (playerRec.intersectsWith())
                { // Need to determine how we will recieve an enemy rectangle
                    Rectangle intersectionRec = Rectangle.intersect(playerRec, ); // Need enemy rectangle
                    char side = DetermineSide( , playerRec, intersectionRec); // Need enemy rectangle
                    collisionList.Add(new Collision(side, intersectionRec, , playerRec)); // Need enemy rectangle
                }
            }

            // Need Player attacking enemy loop
            */
            foreach (IProjectile proj in ProjectileList)
            { // projectile hits player
                Rectangle projHitbox = proj.GetHitbox();
                Rectangle intersectPlayer = Rectangle.Intersect(projHitbox, playerRec);
                if (!intersectPlayer.IsEmpty)
                {
                    char side = DetermineSide(projHitbox , playerRec, intersectPlayer);
                    collisionList.AddLast(new Collision(side, intersectPlayer, proj, player));
                }
                
                //projectile hits enemy
                foreach(IEnemy enemy in EnemyList)
                {
                    //--waiting on enemy hitboxes
                }
            }

            return collisionList;
        }

        private char DetermineSide(Rectangle colider, Rectangle colidee, Rectangle intersectionRec)
        {
            int xOverlap = intersectionRec.Width;
            int yOverlap = intersectionRec.Height;
            char side;

            if (xOverlap > yOverlap)
            {
                if (colider.Y < colidee.Y)
                {
                    side = 'N';
                }
                else
                {
                    side = 'S';
                }
            }
            else
            {
                if (colider.X < colidee.X)
                {
                    side = 'W';
                }
                else
                {
                    side = 'E';
                }
            }
            return side;
        }
    }
}
