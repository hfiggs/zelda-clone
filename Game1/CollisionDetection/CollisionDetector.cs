using System;
using System.Collections.Generic;

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
            Rectangle playerRec = room.GetPlayerRectangle();

            foreach (IEnvironment environment in EnvironmentList) { // player colliding with environment
                if (playerRec.intersectsWith()) { // Need to determine how we will recieve an environment rectangle
                    Rectangle intersectionRec = Rectangle.intersect(playerRec, ); // Need environment rectangle
                    char side = DetermineSide(playerRec, , intersectionRec); // Need environment rectangle
                    collisionList.Add(new Collision(side, intersectionRec, playerRec, ));
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
