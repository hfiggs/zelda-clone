using Game1.Sprite;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
			Texture2D doors = content.Load<Texture2D>("images/Environment/ss_doors");
			Texture2D tiles = content.Load<Texture2D>("images/Environment/ss_tiles");
			Texture2D room = content.Load<Texture2D>("images/Environment/RoomBase");
			Texture2D doorfloors = content.Load<Texture2D>("images/Environment/door_floors");
			Texture2D roomBase = content.Load<Texture2D>("images/Environment/RoomFloor");
			Texture2D secretRoom = content.Load<Texture2D>("images/Environment/SecretRoom");
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
        #region tiles
        public ISprite CreateFloor()
		{
			return new EnvironmentSprite(tiles, 0, 0, 0, false);
		}

		public ISprite CreateBlock()
		{
			return new EnvironmentSprite(tiles, 1, 0, 1, false);
		}

		public ISprite CreateStatueFish()
		{
			return new EnvironmentSprite(tiles, 2, 0, 2, false);
		}

		public ISprite CreateStatueDragon()
		{
			return new EnvironmentSprite(tiles, 3, 0, 3, false);
		}

		public ISprite CreateBlack()
		{
			return new EnvironmentSprite(tiles, 0, 1, 4, false);
		}

		public ISprite CreateSand()
        {
			return new EnvironmentSprite(tiles, 1, 1, 5, false);
		}

		public ISprite CreateWater()
        {
			return new EnvironmentSprite(tiles, 2, 1, 6, false);
		}

		public ISprite CreateStairs()
        {
			return new EnvironmentSprite(tiles, 3, 1, 7, false);
		}

		public ISprite CreateBricks()
        {
			return new EnvironmentSprite(tiles, 0, 2, 8, false);
		}

		public ISprite CreateLadder()
        {
			return new EnvironmentSprite(tiles, 1, 2, 9, false);
		}

		public ISprite CreateFire()
		{
			return new EnvironmentSprite(tiles, 2, 2, 31, true);
		}
		#endregion tiles

		#region doors
		public ISprite CreateDoorNBlank()
        {
			return new EnvironmentSprite(doors, 0, 0, 10, false);
		}

		public ISprite CreateDoorNOpen()
		{
			return new EnvironmentSprite(doors, 1, 0, 11, false);
		}

		public ISprite CreateDoorNLocked()
		{
			return new EnvironmentSprite(doors, 2, 0, 12, false);
		}

		public ISprite CreateDoorNClosed()
		{
			return new EnvironmentSprite(doors, 3, 0, 13, false);
		}

		public ISprite CreateDoorNHole()
		{
			return new EnvironmentSprite(doors, 4, 0, 14, false);
		}

		public ISprite CreateDoorWBlank()
		{
			return new EnvironmentSprite(doors, 0, 1, 15, false);
		}

		public ISprite CreateDoorWOpen()
		{
			return new EnvironmentSprite(doors, 1, 1, 16, false);
		}

		public ISprite CreateDoorWLocked()
		{
			return new EnvironmentSprite(doors, 2, 1, 17, false);
		}

		public ISprite CreateDoorWClosed()
		{
			return new EnvironmentSprite(doors, 3, 1, 18, false);
		}

		public ISprite CreateDoorWHole()
		{
			return new EnvironmentSprite(doors, 4, 1, 19, false);
		}

		public ISprite CreateDoorEBlank()
		{
			return new EnvironmentSprite(doors, 0, 2, 20, false);
		}

		public ISprite CreateDoorEOpen()
		{
			return new EnvironmentSprite(doors, 1, 2, 21, false);
		}

		public ISprite CreateDoorELocked()
		{
			return new EnvironmentSprite(doors, 2, 2, 22, false);
		}

		public ISprite CreateDoorEClosed()
		{
			return new EnvironmentSprite(doors, 3, 2, 23, false);
		}

		public ISprite CreateDoorEHole()
		{
			return new EnvironmentSprite(doors, 4, 2, 24, false);
		}

		public ISprite CreateDoorSBlank()
		{
			return new EnvironmentSprite(doors, 0, 3, 25, false);
		}

		public ISprite CreateDoorSOpen()
		{
			return new EnvironmentSprite(doors, 1, 3, 26, false);
		}

		public ISprite CreateDoorSLocked()
		{
			return new EnvironmentSprite(doors, 2, 3, 27, false);
		}

		public ISprite CreateDoorSClosed()
		{
			return new EnvironmentSprite(doors, 3, 3, 28, false);
		}

		public ISprite CreateDoorSHole()
		{
			return new EnvironmentSprite(doors, 4, 3, 29, false);
		}

		public ISprite CreateDoorNFloor()
		{
			return new EnvironmentSprite(doorfloors, 0, 0, 32, false);
		}

		public ISprite CreateDoorEFloor()
		{
			return new EnvironmentSprite(doorfloors, 0, 1, 33, false);
		}

		public ISprite CreateDoorSFloor()
		{
			return new EnvironmentSprite(doorfloors, 1, 1, 34, false);
		}

		public ISprite CreateDoorWFloor()
		{
			return new EnvironmentSprite(doorfloors, 1, 0, 35, false);
		}
        #endregion doors

        public ISprite CreateRoom()
        {
			return new EnvironmentSprite(room, 0, 0, 30, false);
        }

		public ISprite CreateBase()
		{
			return new EnvironmentSprite(roomBase, 0, 0, 36, false);
		}

		public ISprite CreateSecretRoom()
		{
			return new EnvironmentSprite(secretRoom, 0, 0, 37, false);
		}
	}
}
