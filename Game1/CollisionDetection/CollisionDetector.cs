﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Game1;
using Game1.Item;
using Game1.Environment;
using Game1.Enemy;
using Game1.Projectile;
using Game1.Player;
using Game1.Collision_Handling;
using Game1.RoomLoading;
using System.Linq;

namespace Game1.CollisionDetection
{
    class CollisionDetector
    {
        private Screen screen;
        private List<Collision> collisionList;

        public CollisionDetector(Screen screen)
        {
            this.screen = screen;
        }

        public List<Collision> GetCollisionList()
        {
            collisionList = new LinkedList<Collision>();
            List<IItem> ItemList = room.ItemList;
            List<IEnvironment> EnvironmentList = room.EnvironmentList;
            List<IEnemy> EnemyList = room.EnemyList;
            List<IProjectile> ProjectileList = room.ProjectileList;
            IPlayer player = room.Link;
            Rectangle playerHitbox = room.Link.GetPlayerHitbox();
            Rectangle swordHitbox = room.Link.GetSwordHitbox();

            bool collision = false;
            foreach (IEnvironment environment in EnvironmentList)
            { // player colliding with environment


                //some environment objects have multiple hitboxes
                if (collision)
                    break;
                foreach (Rectangle envHitbox in environment.GetHitboxes())
                {
                    //Environment collides with player
                    Rectangle intersectPlayer = Rectangle.Intersect(playerHitbox, envHitbox);
                    if (!intersectPlayer.IsEmpty)
                    {
                        char side = DetermineSide(playerHitbox, envHitbox, intersectPlayer);
                        collisionList.Add(new Collision(side, intersectPlayer, player, environment));
                        collision = true;
                        break;
                    }

                    //Projectile collides with environment
                    foreach (IProjectile proj in ProjectileList)
                    {
                        Rectangle projHitbox = proj.GetHitbox();
                        Rectangle intersectEnv = Rectangle.Intersect(projHitbox, envHitbox);
                        if (!intersectEnv.IsEmpty)
                        {
                            char side = DetermineSide(projHitbox, envHitbox, intersectEnv);
                            collisionList.Add(new Collision(side, intersectEnv, proj, environment));
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
                    collisionList.Add(new Collision(side, intersection, player, item));
                }
            }

            
            foreach (IEnemy enemy in EnemyList)
            { // enemy attacking player
                Rectangle enemyHitbox = enemy.GetHitbox();
                Rectangle intersectPlayer = Rectangle.Intersect(enemyHitbox, playerHitbox);
                if (!intersectPlayer.IsEmpty) {
                    char side = DetermineSide(enemyHitbox, playerHitbox, intersectPlayer);
                    collisionList.Add(new Collision(side, intersectPlayer, enemy, player));
                }

                Rectangle intersectSword = Rectangle.Intersect(swordHitbox, enemyHitbox);
                if(!intersectSword.IsEmpty)
                {
                    char side = DetermineSide(swordHitbox, enemyHitbox, intersectSword);
                    collisionList.Add(new Collision(side, intersectSword, player, enemy));
                }

                collision = false;
                foreach(IEnvironment envi in EnvironmentList)
                {
                    List<Rectangle> envHitbox = envi.GetHitboxes();
                    if (collision)
                        break;
                    foreach (Rectangle erect in envHitbox)
                    {
                        Rectangle intersectEnemy = Rectangle.Intersect(enemyHitbox, erect);
                        if (intersectEnemy.Width != 0 || intersectEnemy.Height != 0)
                        {
                            char side = DetermineSide(enemyHitbox, erect, intersectEnemy);
                            collisionList.Add(new Collision(side, intersectEnemy, enemy, envi));
                            collision = true;
                            break;
                        }
                    }
                }
            }
            
            foreach (IProjectile proj in ProjectileList)
            { // projectile hits player
                Rectangle projHitbox = proj.GetHitbox();
                Rectangle intersectPlayer = Rectangle.Intersect(projHitbox, playerHitbox);
                char side;
                if (!intersectPlayer.IsEmpty)
                {
                    char side = DetermineSide(projHitbox, playerHitbox, intersectPlayer);
                    collisionList.Add(new Collision(side, intersectPlayer, proj, player));
                }
                
                //projectile hits enemy
                foreach(IEnemy enemy in EnemyList)
                {
                    Rectangle enemyHitbox = enemy.GetHitbox();
                    Rectangle intersectEnemy = Rectangle.Intersect(enemyHitbox, projHitbox);
                    if(!intersectEnemy.IsEmpty)
                    {
                        char side = DetermineSide(projHitbox, enemyHitbox, intersectEnemy);
                        collisionList.Add(new Collision(side, intersectEnemy, proj, enemy));
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
                        collisionList.Add(new Collision(side, interscetItem, proj, item));
                    }
                }

                

            }
            System.Console.WriteLine(collisionList.Count);
            return collisionList;
        }

        private char DetermineSide(Rectangle colider, Rectangle colidee, Rectangle intersectionRec)
        {
            int xOverlap = intersectionRec.Width;
            int yOverlap = intersectionRec.Height;
            Console.WriteLine("xover: " + xOverlap);
            Console.WriteLine("yover: " + yOverlap);
            Console.WriteLine("______________________________________");
            char side;

            if (xOverlap < yOverlap) {
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

        private char RectangleCollision(Rectangle colider, Rectangle colidee, Rectangle intersectionRec) {
            char side;

            if (colider.Height > colider.Width) {
                if (intersectionRec.Contains(colidee.X, colidee.Y) || intersectionRec.Contains(colidee.Right, colidee.Y)) {
                    side = 'S';
                } else {
                    side = 'N';
                }
            } else {
                if (intersectionRec.Contains(colidee.Right, colidee.Y) || intersectionRec.Contains(colidee.Right, colidee.Bottom)) {
                    side = 'W';
                } else {
                    side = 'E';
                }
            }

            return side;
        }
    }
}
