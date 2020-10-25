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

		public void LoadContent(ContentManager content)
		{
			Texture2D doors = content.Load<Texture2D>("images/ss_doors");
			Texture2D tiles = content.Load<Texture2D>("images/ss_tiles");
			Texture2D room = content.Load<Texture2D>("images/room_base");
			Texture2D doorfloors = content.Load<Texture2D>("images/door_floors");
			Texture2D roomBase = content.Load<Texture2D>("images/Rooms/RoomBase");
			Texture2D secretRoom = content.Load<Texture2D>("images/Rooms/SecretRoom");
			this.roomBase = new SpriteSheet(roomBase, 1, 1);
			this.secretRoom = new SpriteSheet(secretRoom, 1, 1);
			this.doors = new SpriteSheet(doors, 5, 4);
			this.tiles = new SpriteSheet(tiles, 4, 3);
			this.room = new SpriteSheet(room, 1, 1);
			this.doorfloors = new SpriteSheet(doorfloors, 2, 2);
		}

		/*
		 * Factory methods for images found in ss_tiles.png
		 */

		public ISprite createFloor()
		{
			return new EnvironmentSprite(tiles, 0, 0, 0, false);
		}

		public ISprite createBlock()
		{
			return new EnvironmentSprite(tiles, 1, 0, 1, false);
		}

		public ISprite createStatueFish()
		{
			return new EnvironmentSprite(tiles, 2, 0, 2, false);
		}

		public ISprite createStatueDragon()
		{
			return new EnvironmentSprite(tiles, 3, 0, 3, false);
		}

		public ISprite createBlack()
		{
			return new EnvironmentSprite(tiles, 0, 1, 4, false);
		}

		public ISprite createSand()
        {
			return new EnvironmentSprite(tiles, 1, 1, 5, false);
		}

		public ISprite createWater()
        {
			return new EnvironmentSprite(tiles, 2, 1, 6, false);
		}

		public ISprite createStairs()
        {
			return new EnvironmentSprite(tiles, 3, 1, 7, false);
		}

		public ISprite createBricks()
        {
			return new EnvironmentSprite(tiles, 0, 2, 8, false);
		}

		public ISprite createLadder()
        {
			return new EnvironmentSprite(tiles, 1, 2, 9, false);
		}

		public ISprite createDoorNBlank()
        {
			return new EnvironmentSprite(doors, 0, 0, 10, false);
		}

		public ISprite createDoorNOpen()
		{
			return new EnvironmentSprite(doors, 1, 0, 11, false);
		}

		public ISprite createDoorNLocked()
		{
			return new EnvironmentSprite(doors, 2, 0, 12, false);
		}

		public ISprite createDoorNClosed()
		{
			return new EnvironmentSprite(doors, 3, 0, 13, false);
		}

		public ISprite createDoorNHole()
		{
			return new EnvironmentSprite(doors, 4, 0, 14, false);
		}

		public ISprite createDoorWBlank()
		{
			return new EnvironmentSprite(doors, 0, 1, 15, false);
		}

		public ISprite createDoorWOpen()
		{
			return new EnvironmentSprite(doors, 1, 1, 16, false);
		}

		public ISprite createDoorWLocked()
		{
			return new EnvironmentSprite(doors, 2, 1, 17, false);
		}

		public ISprite createDoorWClosed()
		{
			return new EnvironmentSprite(doors, 3, 1, 18, false);
		}

		public ISprite createDoorWHole()
		{
			return new EnvironmentSprite(doors, 4, 1, 19, false);
		}

		public ISprite createDoorEBlank()
		{
			return new EnvironmentSprite(doors, 0, 2, 20, false);
		}

		public ISprite createDoorEOpen()
		{
			return new EnvironmentSprite(doors, 1, 2, 21, false);
		}

		public ISprite createDoorELocked()
		{
			return new EnvironmentSprite(doors, 2, 2, 22, false);
		}

		public ISprite createDoorEClosed()
		{
			return new EnvironmentSprite(doors, 3, 2, 23, false);
		}

		public ISprite createDoorEHole()
		{
			return new EnvironmentSprite(doors, 4, 2, 24, false);
		}

		public ISprite createDoorSBlank()
		{
			return new EnvironmentSprite(doors, 0, 3, 25, false);
		}

		public ISprite createDoorSOpen()
		{
			return new EnvironmentSprite(doors, 1, 3, 26, false);
		}

		public ISprite createDoorSLocked()
		{
			return new EnvironmentSprite(doors, 2, 3, 27, false);
		}

		public ISprite createDoorSClosed()
		{
			return new EnvironmentSprite(doors, 3, 3, 28, false);
		}

		public ISprite createDoorSHole()
		{
			return new EnvironmentSprite(doors, 4, 3, 29, false);
		}

		public ISprite createRoom()
        {
			return new EnvironmentSprite(room, 0, 0, 30, false);
        }

		public ISprite createBase()
		{
			return new EnvironmentSprite(roomBase, 0, 0, 36, false);
		}

		public ISprite createSecretRoom()
		{
			return new EnvironmentSprite(secretRoom, 0, 0, 37, false);
		}

		public ISprite createFire()
        {
			return new EnvironmentSprite(tiles, 2, 2, 31, true);
        }

		public ISprite createDoorNFloor()
		{
			return new EnvironmentSprite(doorfloors, 0, 0, 32, false);
		}

		public ISprite createDoorEFloor()
		{
			return new EnvironmentSprite(doorfloors, 0, 1, 33, false);
		}

		public ISprite createDoorSFloor()
		{
			return new EnvironmentSprite(doorfloors, 1, 1, 34, false);
		}

		public ISprite createDoorWFloor()
		{
			return new EnvironmentSprite(doorfloors, 1, 0, 35, false);
		}
	}
}
