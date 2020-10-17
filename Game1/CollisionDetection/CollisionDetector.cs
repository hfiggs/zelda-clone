using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Game1.CollisionDetection
{
    class CollisionDetector
    {
        private IRoom room;
        private List<Collision> collisionList;
        private Object collider, collidee;

        public CollisionDetector(IRoom room) {
            this.room = room;
        }

        public List<Collision> GetCollisionList() {
            LinkedList<IItem> ItemList = room.ItemList;
            LinkedList<IEnvironment> EnvironmentList = room.EnvironmentList;
            LinkedList<IEnemy> EnemyList = room.EnemyList;
            LinkedList<IProjectile> ProjectileList = room.ProjectileList;
            IPlayer Link = null; //TODO: make this a reference to link when room is created
            Rectangle playerRec = room.GetPlayerRectangle();

            foreach (IEnvironment environment in EnvironmentList) { // player colliding with environment

                //some environment objects have multiple hitboxes
                foreach(Rectangle envRect in environment.GetHitboxes())
                {
                    Rectangle intersection = Intersect(playerRec, envRect);
                    if(!intersection.Equals(null))
                    {
                        char side = DetermineSide(playerRec, envRect, intersection);
                        collisionList.Add(new Collision(side, intersectionRec, Link, environment));
                    }
                }
            }

            // Need enemy colliding with environment loop

            foreach (IItem item in ItemList) { // player colliding with item
                if (playerRec.intersectsWith()) { // Need to determine how we will recieve an item rectangle
                    Rectangle intersectionRec = Rectangle.intersect(playerRec, ); // Need item rectangle
                    char side = DetermineSide(playerRec, , intersectionRec); // Need item rectangle
                    collisionList.Add(new Collision(side, intersectionRec, playerRec, )); // Need item rectangle
                }
            }

            foreach (IEnemy enemy in EnemyList) { // enemy attacking player
                if (playerRec.intersectsWith()) { // Need to determine how we will recieve an enemy rectangle
                    Rectangle intersectionRec = Rectangle.intersect(playerRec, ); // Need enemy rectangle
                    char side = DetermineSide( , playerRec, intersectionRec); // Need enemy rectangle
                    collisionList.Add(new Collision(side, intersectionRec, , playerRec)); // Need enemy rectangle
                }
            }

            // Need Player attacking enemy loop

            foreach (IEnemy enemy in EnemyList) { // projectile attacking player
                if (playerRec.intersectsWith()) { // Need to determine how we will recieve a projectile rectangle
                    Rectangle intersectionRec = Rectangle.intersect(playerRec, ); // Need projectile rectangle
                    char side = DetermineSide( , playerRec, intersectionRec); // Need projectile rectangle
                    collisionList.Add(new Collision(side, intersectionRec, , playerRec)); // Need projectile rectangle
                }
            }

            // Need projectile attacking enemy loop

            return collisionList;
        }

        private char DetermineSide(Rectangle colider, Rectangle colidee, Rectangle intersectionRec) {
            int xOverlap = intersectionRec.Width;
            int yOverlap = intersectionRec.Height;
            char side;

            if (xOverlap > yOverlap) {
                if (colider.Y < colidee.Y) {
                    side = 'N';
                } else {
                    side = 'S';
                }
            } else {
                if (colider.X < colidee.X) {
                    side = 'W';
                } else {
                    side = 'E';
                }
            }
            return side;
        }
    }
}
