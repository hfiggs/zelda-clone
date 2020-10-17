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
        private List<Collision> collisionList;
        //private Object collider, collidee;

        public CollisionDetector(Room room)
        {
            this.room = room;
        }

        public List<Collision> GetCollisionList()
        {
            collisionList = new List<Collision>();
            List<IItem> ItemList = room.ItemList;
            List<IEnvironment> EnvironmentList = room.EnvironmentList;
            List<IEnemy> EnemyList = room.EnemyList;
            List<IProjectile> ProjectileList = room.ProjectileList;
            IPlayer Link = room.Link;
            Rectangle playerRec = new Rectangle(0, 0, 64, 64); //dummy variable, will be somthing like this later: room.Link.GetPlayerRectangle();

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
                        collisionList.Add(new Collision(side, intersectPlayer, Link, environment));
                    }

                    //Environment collides with Enemy
                    foreach (IEnemy enemy in EnemyList)
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
                    }
                }
            }
            /*
            foreach (IItem item in ItemList)
            { // player colliding with item
                if (playerRec.intersectsWith())
                { // Need to determine how we will recieve an item rectangle
                    Rectangle intersectionRec = Rectangle.intersect(playerRec, ); // Need item rectangle
                    char side = DetermineSide(playerRec, , intersectionRec); // Need item rectangle
                    collisionList.Add(new Collision(side, intersectionRec, playerRec, )); // Need item rectangle
                }
            }

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

            foreach (IEnemy enemy in EnemyList)
            { // projectile attacking player
                if (playerRec.intersectsWith())
                { // Need to determine how we will recieve a projectile rectangle
                    Rectangle intersectionRec = Rectangle.intersect(playerRec, ); // Need projectile rectangle
                    char side = DetermineSide( , playerRec, intersectionRec); // Need projectile rectangle
                    collisionList.Add(new Collision(side, intersectionRec, , playerRec)); // Need projectile rectangle
                }
            }*/

            // Need projectile attacking enemy loop

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
