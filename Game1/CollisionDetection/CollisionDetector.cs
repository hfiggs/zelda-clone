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
            Rectangle playerHitbox = room.Link.GetPlayerHitbox();
            Rectangle swordHitbox = room.Link.GetSwordHitbox();

            foreach (IEnvironment environment in EnvironmentList)
            { // player colliding with environment

                //some environment objects have multiple hitboxes
                foreach (Rectangle envHitbox in environment.GetHitboxes())
                {
                    //Environment collides with player
                    Rectangle intersectPlayer = Rectangle.Intersect(playerHitbox, envHitbox);
                    if (!intersectPlayer.IsEmpty)
                    {
                        char side = DetermineSide(playerHitbox, envHitbox, intersectPlayer);
                        collisionList.AddLast(new Collision(side, intersectPlayer, player, environment));
                    }

                    //Environment collides with Enemy
                    foreach (IEnemy enemy in EnemyList)
                    {
                        Rectangle enemyHitbox = enemy.GetHitbox();
                        Rectangle intersectEnemy = Rectangle.Intersect(enemyHitbox, envHitbox);
                        if (!intersectEnemy.IsEmpty)
                        {
                            char side = DetermineSide(enemyHitbox, envHitbox, intersectPlayer);
                            collisionList.AddLast(new Collision(side, intersectEnemy, enemy, environment));
                        }
                    }

                    //Projectile collides with environment
                    foreach (IProjectile proj in ProjectileList)
                    {
                        Rectangle projHitbox = proj.GetHitbox();
                        Rectangle intersectEnv = Rectangle.Intersect(projHitbox, envHitbox);
                        if (!intersectEnv.IsEmpty)
                        {
                            char side = DetermineSide(projHitbox, envHitbox, intersectPlayer);
                            collisionList.AddLast(new Collision(side, intersectEnv, proj, environment));
                        }
                    }
                }
            }

            //Only player collides with item
            foreach (IItem item in ItemList)
            {
                Rectangle itemHitbox = item.GetHitbox();
                Rectangle intersection = Rectangle.Intersect(itemHitbox, playerHitbox);
                if(!intersection.IsEmpty)
                {
                    char side = DetermineSide(playerHitbox, itemHitbox, intersection);
                    collisionList.AddLast(new Collision(side, intersection, player, item));
                }
            }

            
            foreach (IEnemy enemy in EnemyList)
            { // enemy attacking player
                Rectangle enemyHitbox = enemy.GetHitbox();
                Rectangle intersectPlayer = Rectangle.Intersect(enemyHitbox, playerHitbox);
                if (!intersectPlayer.IsEmpty) {
                    char side = DetermineSide(enemyHitbox, playerHitbox, intersectPlayer);
                    collisionList.AddLast(new Collision(side, intersectPlayer, enemy, player));
                }

                Rectangle intersectSword = Rectangle.Intersect(swordHitbox, enemyHitbox);
                if(!intersectSword.IsEmpty)
                {
                    char side = DetermineSide(swordHitbox, enemyHitbox, intersectSword);
                    collisionList.AddLast(new Collision(side, intersectSword, player, enemy));
                }
            }
            
            foreach (IProjectile proj in ProjectileList)
            { // projectile hits player
                Rectangle projHitbox = proj.GetHitbox();
                Rectangle intersectPlayer = Rectangle.Intersect(projHitbox, playerHitbox);
                if (!intersectPlayer.IsEmpty)
                {
                    char side = DetermineSide(projHitbox, playerHitbox, intersectPlayer);
                    collisionList.AddLast(new Collision(side, intersectPlayer, proj, player));
                }
                
                //projectile hits enemy
                foreach(IEnemy enemy in EnemyList)
                {
                    Rectangle enemyHitbox = enemy.GetHitbox();
                    Rectangle intersectEnemy = Rectangle.Intersect(enemyHitbox, projHitbox);
                    if(!intersectEnemy.IsEmpty)
                    {
                        char side = DetermineSide(projHitbox, enemyHitbox, intersectEnemy);
                        collisionList.AddLast(new Collision(side, intersectEnemy, proj, enemy));
                    }
                }

                //projectile hits item (only for boomerang)
                foreach(IItem item in ItemList)
                {
                    Rectangle itemHitbox = item.GetHitbox();
                    Rectangle interscetItem = Rectangle.Intersect(itemHitbox, projHitbox);
                    if(!interscetItem.IsEmpty)
                    {
                        char side = DetermineSide(projHitbox, itemHitbox, interscetItem);
                        collisionList.AddLast(new Collision(side, interscetItem, proj, item));
                    }
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
