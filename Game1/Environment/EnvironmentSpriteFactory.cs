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
		private SpriteSheet doors;
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
			Texture2D doors = content.Load<Texture2D>(doorSpriteFilePath);
			Texture2D tiles = content.Load<Texture2D>(tileSpriteFilePath);
			Texture2D room = content.Load<Texture2D>(roomSpriteFilePath);
			Texture2D doorfloors = content.Load<Texture2D>(doorFloorSpriteFilePath);
			Texture2D roomBase = content.Load<Texture2D>(roomBaseSpriteFilePath);
			Texture2D secretRoom = content.Load<Texture2D>(secretRoomSpriteFilePath);
			this.roomBase = new SpriteSheet(roomBase, roomBaseColumns, roomBaseRows);
			this.secretRoom = new SpriteSheet(secretRoom, secretRoomColumns, secretRoomRows);
			this.doors = new SpriteSheet(doors, doorColumns, doorRows);
			this.tiles = new SpriteSheet(tiles, tileColumns, tileRows);
			this.room = new SpriteSheet(room, roomColumns, roomRows);
			this.doorfloors = new SpriteSheet(doorfloors, doorfloorsColumns, doorfloorsRows);
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

		public ISprite CreateRoom()
		{
			return new EnvironmentSprite(room, roomColumn, roomRow, roomID, false);
		}

		public ISprite CreateBase()
		{
			return new EnvironmentSprite(roomBase, roomBaseColumn, roomBaseRow, roomBaseID, false);
		}

		public ISprite CreateSecretRoom()
		{
			return new EnvironmentSprite(secretRoom, secretRoomColumn, secretRoomRow, secretRoomID, false);
		}

		public ISprite CreateFire()
		{
			return new EnvironmentSprite(tiles, fireColumn, fireRow, fireID, true);
		}

        #endregion Tiles

        #region Doors

        public ISprite CreateDoorNBlank()
        {
			return new EnvironmentSprite(doors, DoorNBlankColumn, DoorNBlankRow, DoorNBlankID, false);
		}

		public ISprite CreateDoorNOpen()
		{
			return new EnvironmentSprite(doors, DoorNOpenColumn, DoorNOpenRow, DoorNOpenID, false);
		}

		public ISprite CreateDoorNLocked()
		{
			return new EnvironmentSprite(doors, DoorNLockedColumn, DoorNLockedRow, DoorNLockedID, false);
		}

		public ISprite CreateDoorNClosed()
		{
			return new EnvironmentSprite(doors, DoorNClosedColumn, DoorNClosedRow, DoorNClosedID, false);
		}

		public ISprite CreateDoorNHole()
		{
			return new EnvironmentSprite(doors, DoorNHoleColumn, DoorNHoleRow, DoorNHoleID, false);
		}

		public ISprite CreateDoorNFloor()
		{
			return new EnvironmentSprite(doorfloors, DoorNFloorColumn, DoorNFloorRow, DoorNFloorID, false);
		}

		public ISprite CreateDoorWBlank()
		{
			return new EnvironmentSprite(doors, DoorWBlankColumn, DoorWBlankRow, DoorWBlankID, false);
		}

		public ISprite CreateDoorWOpen()
		{
			return new EnvironmentSprite(doors, DoorWOpenColumn, DoorWOpenRow, DoorWOpenID, false);
		}

		public ISprite CreateDoorWLocked()
		{
			return new EnvironmentSprite(doors, DoorWLockedColumn, DoorWLockedRow, DoorWLockedID, false);
		}

		public ISprite CreateDoorWClosed()
		{
			return new EnvironmentSprite(doors, DoorWClosedColumn, DoorWClosedRow, DoorWClosedID, false);
		}

		public ISprite CreateDoorWHole()
		{
			return new EnvironmentSprite(doors, DoorWHoleColumn, DoorWHoleRow, DoorWHoleID, false);
		}
		
		public ISprite CreateDoorEBlank()
		{
			return new EnvironmentSprite(doors, DoorEBlankColumn, DoorEBlankRow, DoorEBlankID, false);
		}

		public ISprite CreateDoorWFloor()
		{
			return new EnvironmentSprite(doorfloors, DoorWFloorColumn, DoorWFloorRow, DoorWFloorID, false);
		}

		public ISprite CreateDoorEOpen()
		{
			return new EnvironmentSprite(doors, DoorEOpenColumn, DoorEOpenRow, DoorEOpenID, false);
		}

		public ISprite CreateDoorELocked()
		{
			return new EnvironmentSprite(doors, DoorELockedColumn, DoorELockedRow, DoorELockedID, false);
		}

		public ISprite CreateDoorEClosed()
		{
			return new EnvironmentSprite(doors, DoorEClosedColumn, DoorEClosedRow, DoorEClosedID, false);
		}

		public ISprite CreateDoorEHole()
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
