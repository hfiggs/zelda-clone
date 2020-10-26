using Game1.Enemy;
using Game1.Environment;
using Game1.Item;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1.RoomLoading
{
    class RoomParser
    {
        readonly XMLLoader roomData;
        readonly Game1 game;
        public RoomParser(Game1 game, String path)
        {
            this.roomData = new XMLLoader(path);
            this.game = game;
        }

        public LinkedList<IItem> GetItems()
        {
            LinkedList<IItem> items = new LinkedList<IItem>();
            XmlNodeList itemsXMLNodes = roomData.getItemNodes();

            foreach(XmlNode n in itemsXMLNodes)
            {
                
                int x = int.Parse(n["X"].InnerText);
                int y = int.Parse(n["Y"].InnerText);
                Vector2 position = new Vector2(x, y);
                IItem item = null;

                switch (n["Type"].InnerText)
                {
                    case "Bomb":
                        item = new Bomb(position);
                        break;
                    case "Bow":
                        item = new Bow(position);
                        break;
                    case "Clock":
                        item = new Clock(position);
                        break;
                    case "Compass":
                        item = new Compass(position);
                        break;
                    case "Fairy":
                        item = new Fairy(position);
                        break;
                    case "Heart":
                        item = new Heart(position);
                        break;
                    case "HeartContainer":
                        item = new HeartContainer(position);
                        break;
                    case "ItemBoomerang":
                        item = new ItemBoomerang(position);
                        break;
                    case "Key":
                        item = new Key(position);
                        break;
                    case "Map":
                        item = new Map(position);
                        break;
                    case "RupeeBlue":
                        item = new RupeeBlue(position);
                        break;
                    case "RupeeYellow":
                        item = new RupeeYellow(position);
                        break;
                    case "Triforce":
                        item = new Triforce(position);
                        break;
                    default:
                        throw new System.ArgumentException("Parameter cannot be null", "original");
                }

                items.AddLast(item);
            }

            return items;
        }
        public LinkedList<IEnemy> GetEnemies()
        {
            LinkedList<IEnemy> enemies = new LinkedList<IEnemy>();
            XmlNodeList enemyXMLNodes = roomData.getEnemyNodes();

            foreach (XmlNode n in enemyXMLNodes)
            {

                int x = int.Parse(n["X"].InnerText);
                int y = int.Parse(n["Y"].InnerText);
                Vector2 position = new Vector2(x, y);
                IEnemy enemy = null;

                switch (n["Type"].InnerText)
                {
                    case "Aquamentus":
                        enemy = new Aquamentus(game, position);
                        break;
                    case "Bat":
                        enemy = new Bat(game, position);
                        break;
                    case "Dodongo":
                        enemy = new Dodongo(game, position);
                        break;
                    case "Goriya":
                        enemy = new Goriya(game, position);
                        break;
                    case "Hand":
                        enemy = new Hand(game, position);
                        break;
                    case "Jelly":
                        enemy = new Jelly(game, position);
                        break;
                    case "Merchant":
                        enemy = new Merchant(position);
                        break;
                    case "OldMan":
                        enemy = new OldMan(position);
                        break;
                    case "Skeleton":
                        enemy = new Skeleton(game, position);
                        break;
                    case "SkeletonKey":
                        enemy = new Skeleton(game, position, new Key(position));
                        break;
                    case "Snake":
                        enemy = new Snake(game, position);
                        break;
                    case "SpikeTrap":
                        enemy = new SpikeTrap(game, position, 50, 50);
                        break;
                    default:
                        throw new System.ArgumentException("Parameter cannot be null", "original");
                }

                enemies.AddLast(enemy);
            }

            return enemies;
        }
        public LinkedList<IEnvironment> GetNonInteractableEnvinornment()
        {
            LinkedList<IEnvironment> nonInteractEnviornmentList = new LinkedList<IEnvironment>();
            XmlNodeList nonInteractEnviornmentNodes = roomData.getNonInteractEnviornmentNodes();

            foreach (XmlNode n in nonInteractEnviornmentNodes)
            {

                int x = int.Parse(n["X"].InnerText);
                int y = int.Parse(n["Y"].InnerText);
                Vector2 position = new Vector2(x, y);
                IEnvironment nonInteractEnviornment = null;

                switch (n["Type"].InnerText)
                {
                    case "Black":
                        nonInteractEnviornment = new Black(position);
                        break;
                    case "Bricks":
                        nonInteractEnviornment = new Bricks(position);
                        break;
                    case "Floor":
                        nonInteractEnviornment = new Floor(position);
                        break;
                    case "Ladder":
                        nonInteractEnviornment = new Ladder(position);
                        break;
                    case "RoomBase":
                        nonInteractEnviornment = new RoomBase(position);
                        break;
                    case "Sand":
                        nonInteractEnviornment = new Sand(position);
                        break;
                    case "SecretRoom":
                        nonInteractEnviornment = new SecretRoom(position);
                        break;
                    case "DoorNFloor":
                        nonInteractEnviornment = new DoorNFloor(position);
                        break;
                    case "DoorEFloor":
                        nonInteractEnviornment = new DoorEFloor(position);
                        break;
                    case "DoorSFloor":
                        nonInteractEnviornment = new DoorSFloor(position);
                        break;
                    case "DoorWFloor":
                        nonInteractEnviornment = new DoorWFloor(position);
                        break;
                    default:
                        throw new System.ArgumentException("Parameter cannot be null", "original");
                }

                nonInteractEnviornmentList.AddLast(nonInteractEnviornment);
            }

            return nonInteractEnviornmentList;
        }

        public LinkedList<IEnvironment> GetInteractableEnvinornment()
        {
            LinkedList<IEnvironment> interactEnviornmentList = new LinkedList<IEnvironment>();
            XmlNodeList interactEnviornmentNodes = roomData.getInteractEnviornmentNodes();

            foreach (XmlNode n in interactEnviornmentNodes)
            {

                int x = int.Parse(n["X"].InnerText);
                int y = int.Parse(n["Y"].InnerText);
                Vector2 position = new Vector2(x, y);
                IEnvironment interactEnviornment = null;

                switch (n["Type"].InnerText)
                {
                    case "Block":
                        interactEnviornment = new Block(position);
                        break;
                    case "DoorEBlank":
                        interactEnviornment = new DoorEBlank(position);
                        break;
                    case "DoorEClosed":
                        interactEnviornment = new DoorEClosed(position);
                        break;
                    case "DoorEHole":
                        interactEnviornment = new DoorEHole(position);
                        break;
                    case "DoorEOpen":
                        interactEnviornment = new DoorEOpen(position);
                        break;
                    case "DoorELocked":
                        interactEnviornment = new DoorELocked(position);
                        break;
                    case "DoorNBlank":
                        interactEnviornment = new DoorNBlank(position);
                        break;
                    case "DoorNClosed":
                        interactEnviornment = new DoorNClosed(position);
                        break;
                    case "DoorNHole":
                        interactEnviornment = new DoorNHole(position);
                        break;
                    case "DoorNOpen":
                        interactEnviornment = new DoorNOpen(position);
                        break;
                    case "DoorNLocked":
                        interactEnviornment = new DoorNLocked(position);
                        break;
                    case "DoorSBlank":
                        interactEnviornment = new DoorSBlank(position);
                        break;
                    case "DoorSClosed":
                        interactEnviornment = new DoorSClosed(position);
                        break;
                    case "DoorSHole":
                        interactEnviornment = new DoorSHole(position);
                        break;
                    case "DoorSOpen":
                        interactEnviornment = new DoorSOpen(position);
                        break;
                    case "DoorSLocked":
                        interactEnviornment = new DoorSLocked(position);
                        break;
                    case "DoorWBlank":
                        interactEnviornment = new DoorWBlank(position);
                        break;
                    case "DoorWClosed":
                        interactEnviornment = new DoorWClosed(position);
                        break;
                    case "DoorWHole":
                        interactEnviornment = new DoorWHole(position);
                        break;
                    case "DoorWOpen":
                        interactEnviornment = new DoorWOpen(position);
                        break;
                    case "DoorWLocked":
                        interactEnviornment = new DoorWLocked(position);
                        break;
                    case "Fire":
                        interactEnviornment = new Fire(position);
                        break;
                    case "StatueDragon":
                        interactEnviornment = new StatueDragon(position);
                        break;
                    case "StatueFish":
                        interactEnviornment = new StatueFish(position);
                        break;
                    case "Water":
                        interactEnviornment = new Water(position);
                        break;
                    case "Stairs":
                        interactEnviornment = new Stairs(position);
                        break;
                    case "RoomBorder":
                        interactEnviornment = new RoomBorder(position);
                        break;
                    default:
                        throw new System.ArgumentException("Parameter cannot be null", "original");
                }

                interactEnviornmentList.AddLast(interactEnviornment);
            }

            return interactEnviornmentList;
        }
    }
}
