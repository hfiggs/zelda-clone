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
