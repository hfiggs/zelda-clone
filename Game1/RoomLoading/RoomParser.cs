using Game1.Audio;
using Game1.Enemy;
using Game1.Environment;
using Game1.Item;
using Game1.RoomLoading.Puzzle;
using Game1.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Game1.RoomLoading
{
    class RoomParser
    {
        private readonly XMLLoader roomData;
        private readonly Game1 game;
        private readonly Room room;
        private const string xPositionTag = "X", yPositionTag = "Y", objectTypeTag = "Type", errorMessage = "Parameter cannot be null", errorParamName = "original", widthTag = "W", heightTag = "H";
        public RoomParser(Game1 game, Room room, String path, int difficulty)
        {
            roomData = new XMLLoader(path, difficulty);
            this.game = game;
            this.room = room;
        }

        public List<IItem> GetItems()
        {
            List<IItem> items = new List<IItem>();
            XmlNodeList itemsXMLNodes = roomData.getItemNodes();

            foreach(XmlNode n in itemsXMLNodes)
            {
                
                int x = int.Parse(n[xPositionTag].InnerText);
                int y = int.Parse(n[yPositionTag].InnerText);
                Vector2 position = new Vector2(x, y);
                IItem item = null;

                switch (n[objectTypeTag].InnerText)
                {
                    case "ArrowItem":
                        item = new ArrowItem(position);
                        break;
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
                    case "BlueCandle":
                        item = new BlueCandle(position);
                        break;
                    case "BluePotion":
                        item = new BluePotion(position);
                        break;
                    default:
                        throw new System.ArgumentException(errorMessage, errorParamName);
                }

                items.Add(item);
            }

            return items;
        }
        public List<IEnemy> GetEnemies()
        {
            List<IEnemy> enemies = new List<IEnemy>();
            XmlNodeList enemyXMLNodes = roomData.getEnemyNodes();

            foreach (XmlNode n in enemyXMLNodes)
            {

                int x = int.Parse(n[xPositionTag].InnerText);
                int y = int.Parse(n[yPositionTag].InnerText);
                Vector2 position = new Vector2(x, y);
                IEnemy enemy = null;

                switch (n[objectTypeTag].InnerText)
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
                    case "HardGoriya":
                        enemy = new HardGoriya(game, position);
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
                    case "HardSkeleton":
                        enemy = new HardSkeleton(game, position);
                        break;
                    case "ShootingSkeleton":
                        enemy = new ShootingSkeleton(game, position);
                        break;
                    case "SkeletonKey":
                        enemy = new Skeleton(game, room, position, new Key(position));
                        break;
                    case "Snake":
                        enemy = new Snake(game, position);
                        break;
                    case "SpikeTrap":
                        const int verticalrange = 40, horizontalRange = 80;
                        enemy = new SpikeTrap(game, position, verticalrange, horizontalRange);
                        break;
                    default:
                        throw new System.ArgumentException(errorMessage, errorParamName);
                }

                enemies.Add(enemy);
            }

            return enemies;
        }
        public LinkedList<IEnvironment> GetNonInteractableEnvinornment()
        {
            LinkedList<IEnvironment> nonInteractEnviornmentList = new LinkedList<IEnvironment>();
            XmlNodeList nonInteractEnviornmentNodes = roomData.getNonInteractEnviornmentNodes();

            foreach (XmlNode n in nonInteractEnviornmentNodes)
            {

                int x = int.Parse(n[xPositionTag].InnerText);
                int y = int.Parse(n[yPositionTag].InnerText);
                Vector2 position = new Vector2(x, y);
                IEnvironment nonInteractEnviornment = null;

                switch (n[objectTypeTag].InnerText)
                {
                    case "Black":
                        nonInteractEnviornment = new Black(position);
                        break;
                    case "BlackCover":
                        float existTime = float.Parse(n["Exist"].InnerText);
                        nonInteractEnviornment = new BlackSelfDestruct(position, existTime);
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
                        nonInteractEnviornment = new DoorFloor(position, CompassDirection.North);
                        break;
                    case "DoorEFloor":
                        nonInteractEnviornment = new DoorFloor(position, CompassDirection.East);
                        break;
                    case "DoorSFloor":
                        nonInteractEnviornment = new DoorFloor(position, CompassDirection.South);
                        break;
                    case "DoorWFloor":
                        nonInteractEnviornment = new DoorFloor(position, CompassDirection.West);
                        break;

                    case "OverworldFloor":
                        nonInteractEnviornment = new OverworldFloor(position);
                        break;
                    case "OverworldFloorTile":
                        nonInteractEnviornment = new OverworldFloorTile(position);
                        break;
                    case "OverworldPlank":
                        nonInteractEnviornment = new OverworldPlank(position);
                        break;

                    case "Stairs":
                        nonInteractEnviornment = new Stairs(position);
                        break;

                    default:
                        throw new System.ArgumentException(errorMessage, errorParamName);
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

                int x = int.Parse(n[xPositionTag].InnerText);
                int y = int.Parse(n[yPositionTag].InnerText);
                Vector2 position = new Vector2(x, y);
                IEnvironment interactEnviornment = null;

                switch (n[objectTypeTag].InnerText)
                {
                    case "Brick":
                        interactEnviornment = new Bricks(position);
                        break;
                    case "InvisibleWall":
                        int w = int.Parse(n[widthTag].InnerText);
                        int h = int.Parse(n[heightTag].InnerText);
                        interactEnviornment = new InvisibleWall(position, new Vector2(w,h));
                        break;
                    case "Block":
                        interactEnviornment = new Block(position);
                        break;
                    case "MovableBlock":
                        const char north = 'N';
                        interactEnviornment = new MovableBlock(position, north);
                        break;
                    case "DoorEBlank":
                        interactEnviornment = new DoorBlank(position, CompassDirection.East);
                        break;
                    case "DoorEClosed":
                        interactEnviornment = new DoorClosed(position, CompassDirection.East);
                        break;
                    case "DoorEHole":
                        interactEnviornment = new DoorBombable(position, CompassDirection.East, true);
                        break;
                    case "DoorEOpen":
                        interactEnviornment = new DoorOpen(position, CompassDirection.East);
                        break;
                    case "DoorELocked":
                        interactEnviornment = new DoorLocked(position, CompassDirection.East);
                        break;
                    case "DoorNBlank":
                        interactEnviornment = new DoorBlank(position, CompassDirection.North);
                        break;
                    case "DoorNClosed":
                        interactEnviornment = new DoorClosed(position, CompassDirection.North);
                        break;
                    case "DoorNHole":
                        interactEnviornment = new DoorBombable(position, CompassDirection.North, true);
                        break;
                    case "DoorNOpen":
                        interactEnviornment = new DoorOpen(position, CompassDirection.North);
                        break;
                    case "DoorNLocked":
                        interactEnviornment = new DoorLocked(position, CompassDirection.North);
                        break;
                    case "DoorSBlank":
                        interactEnviornment = new DoorBlank(position, CompassDirection.South);
                        break;
                    case "DoorSClosed":
                        interactEnviornment = new DoorClosed(position, CompassDirection.South);
                        break;
                    case "DoorSHole":
                        interactEnviornment = new DoorBombable(position, CompassDirection.South, true);
                        break;
                    case "DoorSOpen":
                        interactEnviornment = new DoorOpen(position, CompassDirection.South);
                        break;
                    case "DoorSLocked":
                        interactEnviornment = new DoorLocked(position, CompassDirection.South);
                        break;
                    case "DoorWBlank":
                        interactEnviornment = new DoorBlank(position, CompassDirection.West);
                        break;
                    case "DoorWClosed":
                        interactEnviornment = new DoorClosed(position, CompassDirection.West);
                        break;
                    case "DoorWHole":
                        interactEnviornment = new DoorBombable(position, CompassDirection.West, true);
                        break;
                    case "DoorWOpen":
                        interactEnviornment = new DoorOpen(position, CompassDirection.West);
                        break;
                    case "DoorWLocked":
                        interactEnviornment = new DoorLocked(position, CompassDirection.West);
                        break;
                    case "DoorSBombable":
                        interactEnviornment = new DoorBombable(position, CompassDirection.South, false);
                        break;
                    case "DoorNBombable":
                        interactEnviornment = new DoorBombable(position, CompassDirection.North, false);
                        break;
                    case "DoorEBombable":
                        interactEnviornment = new DoorBombable(position, CompassDirection.East, false);
                        break;
                    case "DoorWBombable":
                        interactEnviornment = new DoorBombable(position, CompassDirection.West, false);
                        break;
                    case "EnterBasementLoadZone":
                        interactEnviornment = new EnterBasementLoadZone(position);
                        break;
                    case "ExitBasementLoadZone":
                        interactEnviornment = new ExitBasementLoadZone(position);
                        break;
                    case "EnterDungeonLoadZone":
                        interactEnviornment = new EnterDungeonLoadZone(position);
                        break;
                    case "ExitDungeonLoadZone":
                        interactEnviornment = new ExitDungeonLoadZone(position);
                        break;
                    case "Fire":
                        interactEnviornment = new Fire(position);
                        break;
                    case "LoadZoneN":
                        interactEnviornment = new LoadZone(position, Util.CompassDirection.North);
                        break;
                    case "LoadZoneE":
                        interactEnviornment = new LoadZone(position, Util.CompassDirection.East);
                        break;
                    case "LoadZoneS":
                        interactEnviornment = new LoadZone(position, Util.CompassDirection.South);
                        break;
                    case "LoadZoneW":
                        interactEnviornment = new LoadZone(position, Util.CompassDirection.West);
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
                    case "RoomBorder":
                        interactEnviornment = new RoomBorder(position);
                        break;

                    case "OverworldWater":
                        interactEnviornment = new OverworldWater(position);
                        break;
                    case "OverworldRockTM":
                        interactEnviornment = new OverworldRockTM(position);
                        break;
                    case "OverworldRockBM":
                        interactEnviornment = new OverworldRockBM(position);
                        break;
                    case "OverworldTreeTL":
                        interactEnviornment = new OverworldTreeTL(position);
                        break;
                    case "OverworldTreeTM":
                        interactEnviornment = new OverworldTreeTM(position);
                        break;
                    case "OverworldTreeTR":
                        interactEnviornment = new OverworldTreeTR(position);
                        break;
                    case "OverworldTreeBL":
                        interactEnviornment = new OverworldTreeBL(position);
                        break;
                    case "OverworldTreeBR":
                        interactEnviornment = new OverworldTreeBR(position);
                        break;

                    case "PortalBlock":
                        interactEnviornment = new PortalBlock(position);
                        break;
                    case "LaserField":
                        interactEnviornment = new LaserField(position);
                        break;

                    default:
                        throw new System.ArgumentException(errorMessage, errorParamName);
                }

                interactEnviornmentList.AddLast(interactEnviornment);
            }

            return interactEnviornmentList;
        }

        public List<AmbientSound> GetAmbienceNode()
        {
            List<AmbientSound> soundList = new List<AmbientSound>();
            XmlNodeList soundNodes = roomData.getAmbientSounds();
            const string soundNameTag = "Name";

            foreach (XmlNode n in soundNodes)
            {
                XmlElement sound = n[soundNameTag];
                float deltaTime = float.Parse(sound.GetAttribute("delay"));
                float volume = float.Parse(sound.GetAttribute("volume"));
                bool loop = bool.Parse(sound.GetAttribute("looped"));
                AmbientSound newSound = new AmbientSound(sound.InnerText, deltaTime, volume, loop);
                soundList.Add(newSound);
            }

            return soundList;
        }

        public AmbientSound GetMusicNode(float baseVolume)
        {
            XmlNodeList musicNode = roomData.getMusic();
            //unused rooms do not have this node. Add if desired
            if (musicNode[0] != null)
            {
                string sound = musicNode[0].InnerText;
                return new AmbientSound(sound, 0.0f, baseVolume, true);
            } else
            {
                return null;
            }
        }

        public IPuzzle GetPuzzle()
        {
            IPuzzle puzzle = null;
            XmlNodeList puzzleNodes = roomData.getPuzzleNodes();

            foreach (XmlNode n in puzzleNodes)
            {
                switch (n[objectTypeTag].InnerText)
                {
                    case "PuzzleSpawnKey":
                        int x = int.Parse(n[xPositionTag].InnerText);
                        int y = int.Parse(n[yPositionTag].InnerText);
                        Vector2 position = new Vector2(x, y);
                        puzzle = new PuzzleSpawnKey(position);
                        break;
                    case "PuzzleOpenDoor":
                        puzzle = new PuzzleOpenDoor();
                        break;
                    case "PuzzleMoveBlock":
                        puzzle = new PuzzleMoveBlock();
                        break;
                    case "PuzzleMoveBlockNS":
                        puzzle = new PuzzleMoveBlockNS();
                        break;
                    case "PuzzleSpawnBoomerang":
                        puzzle = new PuzzleSpawnBoomerang();
                        break;
                    case "PuzzleShootFireballs":
                        puzzle = new PuzzleShootFireballs(game.Screen);
                        break;
                }
            }
            return puzzle;
        }
    }
}
