using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Game1.Sprite;
using Game1.Controller;
using System.Collections.Generic;
using SharpDX.MediaFoundation;

namespace Game1.Environment
{
	public class EnvironmentSpriteFactory
	{
		private SpriteSheet doorsBelow;
		private SpriteSheet doorsAbove;
		private SpriteSheet tiles;
		private SpriteSheet room;
		private SpriteSheet doorfloors;
		private SpriteSheet roomBase;
		private SpriteSheet secretRoom;

		public static EnvironmentSpriteFactory instance = new EnvironmentSpriteFactory();

		private EnvironmentSpriteFactory()
		{
		}

		private const string doorSpriteFilePath = "images/Environment/ss_doors", tileSpriteFilePath = "images/Environment/ss_tiles", roomSpriteFilePath = "images/Environment/RoomBase";
		private const string doorFloorSpriteFilePath = "images/Environment/door_floors", roomBaseSpriteFilePath = "images/Environment/RoomFloor", secretRoomSpriteFilePath = "images/Environment/SecretRoom";
		private const int roomBaseColumns = 1, roomBaseRows = 1, secretRoomColumns = 1, secretRoomRows = 1, doorColumns = 5, doorRows = 4, tileColumns = 4, tileRows = 3, roomColumns = 1, roomRows = 1, doorfloorsColumns = 2, doorfloorsRows = 2;

		public void LoadContent(ContentManager content)
		{
			Texture2D doorsBelow = content.Load<Texture2D>("images/Environment/Doors_Below_Player");
			Texture2D doorsAbove = content.Load<Texture2D>("images/Environment/Doors_Above_Player");
			Texture2D tiles = content.Load<Texture2D>("images/Environment/ss_tiles");
			Texture2D room = content.Load<Texture2D>("images/Environment/RoomBase");
			Texture2D doorfloors = content.Load<Texture2D>("images/Environment/door_floors");
			Texture2D roomBase = content.Load<Texture2D>("images/Environment/RoomFloor");
			Texture2D secretRoom = content.Load<Texture2D>("images/Environment/SecretRoom");
			this.roomBase = new SpriteSheet(roomBase, 1, 1);
			this.secretRoom = new SpriteSheet(secretRoom, 1, 1);
			this.doorsBelow = new SpriteSheet(doorsBelow, 5, 4);
			this.doorsAbove = new SpriteSheet(doorsAbove, 5, 4);
			this.tiles = new SpriteSheet(tiles, 4, 3);
			this.room = new SpriteSheet(room, 1, 1);
			this.doorfloors = new SpriteSheet(doorfloors, 2, 2);
		}

		/*
		 * Factory methods for images found in ss_tiles.png
		 */
        #region constants
        private const int floorColumn = 0, floorRow = 0, floorID = 0, blockColumn = 1, blockRow = 0, blockID = 1, statueFishColumn = 2, statueFishRow = 0, statueFishID = 2, statueDragonColumn = 3, statueDragonRow = 0, statueDragonID = 3;
		private const int blackColumn = 0, blackRow = 1, blackID = 4, sandColumn = 1, sandRow = 1, sandID = 5, waterColumn = 2, waterRow = 1, waterID = 6, stairColumn = 3, stairRow = 1, stairID = 7;
		private const int brickColumn = 0, brickRow = 2, brickID = 8, ladderColumn = 1, ladderRow = 2, ladderID = 9, DoorNBlankColumn = 0, DoorNBlankRow = 0, DoorNBlankID = 10, DoorNOpenColumn = 1, DoorNOpenRow = 0, DoorNOpenID = 11;
		private const int DoorNLockedColumn = 2, DoorNLockedRow = 0, DoorNLockedID = 12, DoorNClosedColumn = 3, DoorNClosedRow = 0, DoorNClosedID = 13, DoorNHoleColumn = 4, DoorNHoleRow = 0, DoorNHoleID = 14;
		private const int DoorWBlankColumn = 0, DoorWBlankRow = 1, DoorWBlankID = 15, DoorWOpenColumn = 1, DoorWOpenRow = 1, DoorWOpenID = 16, DoorWLockedColumn = 2, DoorWLockedRow = 1, DoorWLockedID = 17;
		private const int DoorWClosedColumn = 3, DoorWClosedRow = 1, DoorWClosedID = 18, DoorWHoleColumn = 4, DoorWHoleRow = 1, DoorWHoleID = 19;
		private const int DoorEBlankColumn = 0, DoorEBlankRow = 2, DoorEBlankID = 20, DoorEOpenColumn = 1, DoorEOpenRow = 2, DoorEOpenID = 21, DoorELockedColumn = 2, DoorELockedRow = 2, DoorELockedID = 22;
		private const int DoorEClosedColumn = 3, DoorEClosedRow = 2, DoorEClosedID = 23, DoorEHoleColumn = 4, DoorEHoleRow = 2, DoorEHoleID = 24;
		private const int DoorSBlankColumn = 0, DoorSBlankRow = 3, DoorSBlankID = 25, DoorSOpenColumn = 1, DoorSOpenRow = 3, DoorSOpenID = 26, DoorSLockedColumn = 2, DoorSLockedRow = 3, DoorSLockedID = 27;
		private const int DoorSClosedColumn = 3, DoorSClosedRow = 3, DoorSClosedID = 28, DoorSHoleColumn = 4, DoorSHoleRow = 3, DoorSHoleID = 29;
		private const int roomColumn = 0, roomRow = 0, roomID = 30, roomBaseColumn = 0, roomBaseRow = 0, roomBaseID = 36, secretRoomColumn = 0, secretRoomRow = 0, secretRoomID = 37;
		private const int fireColumn = 2, fireRow = 2, fireID = 31, DoorNFloorColumn = 0, DoorNFloorRow = 0, DoorNFloorID = 32;
		private const int DoorEFloorColumn = 0, DoorEFloorRow = 1, DoorEFloorID = 33, DoorSFloorColumn = 1, DoorSFloorRow = 1, DoorSFloorID = 34, DoorWFloorColumn = 1, DoorWFloorRow = 0, DoorWFloorID = 35;
        #endregion constants

        #region Tiles

        public ISprite CreateFloor()
		{
			return new EnvironmentSprite(tiles, floorColumn, floorRow, floorID, false);
		}

		public ISprite CreateBlock()
		{
			return new EnvironmentSprite(tiles, blockColumn, blockRow, blockID, false);
		}

		public ISprite CreateStatueFish()
		{
			return new EnvironmentSprite(tiles, statueFishColumn, statueFishRow, statueFishID, false);
		}

		public ISprite CreateStatueDragon()
		{
			return new EnvironmentSprite(tiles, statueDragonColumn, statueDragonRow, statueDragonID, false);
		}

		public ISprite CreateBlack()
		{
			return new EnvironmentSprite(tiles, blackColumn, blackRow, blackID, false);
		}

		public ISprite CreateSand()
        {
			return new EnvironmentSprite(tiles, sandColumn, sandRow, sandID, false);
		}

		public ISprite CreateWater()
        {
			return new EnvironmentSprite(tiles, waterColumn, waterRow, waterID, false);
		}

		public ISprite CreateStairs()
        {
			return new EnvironmentSprite(tiles, stairColumn, stairRow, stairID, false);
		}
		
		public ISprite CreateBricks()
        {
			return new EnvironmentSprite(tiles, brickColumn, brickRow, brickID, false);
		}

		public ISprite CreateLadder()
        {
			return new EnvironmentSprite(tiles, ladderColumn, ladderRow, ladderID, false);
		}

        #region Doors Below

        public ISprite createDoorNBlankBelow()
        {
			return new EnvironmentSprite(doorsBelow, 0, 0, 10, false);
		}

		public ISprite createDoorNOpenBelow()
		{
			return new EnvironmentSprite(doorsBelow, 1, 0, 11, false);
		}

		public ISprite createDoorNLockedBelow()
		{
			return new EnvironmentSprite(doorsBelow, 2, 0, 12, false);
		}

		public ISprite createDoorNClosedBelow()
		{
			return new EnvironmentSprite(doorsBelow, 3, 0, 13, false);
		}

		public ISprite createDoorNHoleBelow()
		{
			return new EnvironmentSprite(doorsBelow, 4, 0, 14, false);
		}

		public ISprite createDoorWBlankBelow()
		{
			return new EnvironmentSprite(doorsBelow, 0, 1, 15, false);
		}

		public ISprite createDoorWOpenBelow()
		{
			return new EnvironmentSprite(doorsBelow, 1, 1, 16, false);
		}

		public ISprite createDoorWLockedBelow()
		{
			return new EnvironmentSprite(doorsBelow, 2, 1, 17, false);
		}

		public ISprite createDoorWClosedBelow()
		{
			return new EnvironmentSprite(doorsBelow, 3, 1, 18, false);
		}

		public ISprite createDoorWHoleBelow()
		{
			return new EnvironmentSprite(doorsBelow, 4, 1, 19, false);
		}

		public ISprite createDoorEBlankBelow()
		{
			return new EnvironmentSprite(doorsBelow, 0, 2, 20, false);
		}

		public ISprite createDoorEOpenBelow()
		{
			return new EnvironmentSprite(doorsBelow, 1, 2, 21, false);
		}

		public ISprite createDoorELockedBelow()
		{
			return new EnvironmentSprite(doorsBelow, 2, 2, 22, false);
		}

		public ISprite createDoorEClosedBelow()
		{
			return new EnvironmentSprite(doorsBelow, 3, 2, 23, false);
		}

		public ISprite createDoorEHoleBelow()
		{
			return new EnvironmentSprite(doorsBelow, 4, 2, 24, false);
		}

		public ISprite createDoorSBlankBelow()
		{
			return new EnvironmentSprite(doorsBelow, 0, 3, 25, false);
		}

		public ISprite createDoorSOpenBelow()
		{
			return new EnvironmentSprite(doorsBelow, 1, 3, 26, false);
		}

		public ISprite createDoorSLockedBelow()
		{
			return new EnvironmentSprite(doorsBelow, 2, 3, 27, false);
		}

		public ISprite createDoorSClosedBelow()
		{
			return new EnvironmentSprite(doorsBelow, 3, 3, 28, false);
		}

		public ISprite createDoorSHoleBelow()
		{
			return new EnvironmentSprite(doorsBelow, 4, 3, 29, false);
		}

		#endregion

		#region Doors Above

		public ISprite createDoorNBlankAbove()
		{
			return new EnvironmentSprite(doorsAbove, 0, 0, 10, false);
		}

		public ISprite createDoorNOpenAbove()
		{
			return new EnvironmentSprite(doorsAbove, 1, 0, 11, false);
		}

		public ISprite createDoorNLockedAbove()
		{
			return new EnvironmentSprite(doorsAbove, 2, 0, 12, false);
		}

		public ISprite createDoorNClosedAbove()
		{
			return new EnvironmentSprite(doorsAbove, 3, 0, 13, false);
		}

		public ISprite createDoorNHoleAbove()
		{
			return new EnvironmentSprite(doorsAbove, 4, 0, 14, false);
		}

		public ISprite createDoorWBlankAbove()
		{
			return new EnvironmentSprite(doorsAbove, 0, 1, 15, false);
		}

		public ISprite createDoorWOpenAbove()
		{
			return new EnvironmentSprite(doorsAbove, 1, 1, 16, false);
		}

		public ISprite createDoorWLockedAbove()
		{
			return new EnvironmentSprite(doorsAbove, 2, 1, 17, false);
		}

		public ISprite createDoorWClosedAbove()
		{
			return new EnvironmentSprite(doorsAbove, 3, 1, 18, false);
		}

		public ISprite createDoorWHoleAbove()
		{
			return new EnvironmentSprite(doorsAbove, 4, 1, 19, false);
		}

		public ISprite createDoorEBlankAbove()
		{
			return new EnvironmentSprite(doorsAbove, 0, 2, 20, false);
		}

		public ISprite createDoorEOpenAbove()
		{
			return new EnvironmentSprite(doorsAbove, 1, 2, 21, false);
		}

		public ISprite createDoorELockedAbove()
		{
			return new EnvironmentSprite(doorsAbove, 2, 2, 22, false);
		}

		public ISprite createDoorEClosedAbove()
		{
			return new EnvironmentSprite(doorsAbove, 3, 2, 23, false);
		}

		public ISprite createDoorEHoleAbove()
		{
			return new EnvironmentSprite(doorsAbove, 4, 2, 24, false);
		}

		public ISprite createDoorSBlankAbove()
		{
			return new EnvironmentSprite(doorsAbove, 0, 3, 25, false);
		}

		public ISprite createDoorSOpenAbove()
		{
			return new EnvironmentSprite(doorsAbove, 1, 3, 26, false);
		}

		public ISprite createDoorSLockedAbove()
		{
			return new EnvironmentSprite(doorsAbove, 2, 3, 27, false);
		}

		public ISprite createDoorSClosedAbove()
		{
			return new EnvironmentSprite(doorsAbove, 3, 3, 28, false);
		}

		public ISprite createDoorSHoleAbove()
		{
			return new EnvironmentSprite(doorsAbove, 4, 3, 29, false);
		}

		#endregion

		public ISprite createRoom()
        {
			return new EnvironmentSprite(room, 0, 0, 30, false);
        }

		public ISprite createBase()
		{
			return new EnvironmentSprite(doors, DoorEHoleColumn, DoorEHoleRow, DoorEHoleID, false);
		}

		public ISprite CreateDoorEFloor()
		{
			return new EnvironmentSprite(doorfloors, DoorEFloorColumn, DoorEFloorRow, DoorEFloorID, false);
		}

		public ISprite CreateDoorSBlank()
		{
			return new EnvironmentSprite(doors, DoorSBlankColumn, DoorSBlankRow, DoorSBlankID, false);
		}
		
		public ISprite CreateDoorSOpen()
		{
			return new EnvironmentSprite(doors, DoorSOpenColumn, DoorSOpenRow, DoorSOpenID, false);
		}

		public ISprite CreateDoorSLocked()
		{
			return new EnvironmentSprite(doors, DoorSLockedColumn, DoorSLockedRow, DoorSLockedID, false);
		}

		public ISprite CreateDoorSClosed()
		{
			return new EnvironmentSprite(doors, DoorSClosedColumn, DoorSClosedRow, DoorSClosedID, false);
		}

		public ISprite CreateDoorSHole()
		{
			return new EnvironmentSprite(doors, DoorSHoleColumn, DoorSHoleRow, DoorSHoleID, false);
		}

		public ISprite CreateDoorSFloor()
		{
			return new EnvironmentSprite(doorfloors, DoorSFloorColumn, DoorSFloorRow, DoorSFloorID, false);
		}

        #endregion Doors
    }
}
