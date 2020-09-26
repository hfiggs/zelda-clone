using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Game1.Sprite;
using Game1.Controller;
using System.Collections.Generic;

namespace Game1.Environment
{
	public class EnvironmentSpriteFactory
	{
		private SpriteSheet doors;
		private SpriteSheet tiles;
		private SpriteSheet room;

		public static EnvironmentSpriteFactory instance = new EnvironmentSpriteFactory();

		private EnvironmentSpriteFactory()
		{
		}

		public void LoadContent(ContentManager content)
		{
			Texture2D doors = content.Load<Texture2D>("images/ss_doors");
			Texture2D tiles = content.Load<Texture2D>("images/ss_tiles");
			Texture2D room = content.Load<Texture2D>("images/room_base");
			this.doors = new SpriteSheet(doors, 5, 4);
			this.tiles = new SpriteSheet(tiles, 4, 3);
			this.room = new SpriteSheet(room, 1, 1);
		}

		/*
		 * Factory methods for images found in ss_tiles.png
		 */

		public ISprite createFloor()
		{
			return new EnvironmentSprite(tiles, 0, 0);
		}

		public ISprite createBlock()
		{
			return new EnvironmentSprite(tiles, 1, 0);
		}

		public ISprite createStatueFish()
		{
			return new EnvironmentSprite(tiles, 2, 0);
		}

		public ISprite createStatueDragon()
		{
			return new EnvironmentSprite(tiles, 3, 0);
		}

		public ISprite createBlack()
		{
			return new EnvironmentSprite(tiles, 0, 1);
		}

		public ISprite createSand()
        {
			return new EnvironmentSprite(tiles, 1, 1);
		}

		public ISprite createWater()
        {
			return new EnvironmentSprite(tiles, 2, 1);
		}

		public ISprite createStairs()
        {
			return new EnvironmentSprite(tiles, 3, 1);
		}

		public ISprite createBricks()
        {
			return new EnvironmentSprite(tiles, 0, 2);
		}

		public ISprite createLadder()
        {
			return new EnvironmentSprite(tiles, 1, 2);
		}

		public ISprite createDoorNBlank()
        {
			return new EnvironmentSprite(doors, 0, 0);
		}

		public ISprite createDoorNOpen()
		{
			return new EnvironmentSprite(doors, 1, 0);
		}

		public ISprite createDoorNLocked()
		{
			return new EnvironmentSprite(doors, 2, 0);
		}

		public ISprite createDoorNClosed()
		{
			return new EnvironmentSprite(doors, 3, 0);
		}

		public ISprite createDoorNHole()
		{
			return new EnvironmentSprite(doors, 4, 0);
		}

		public ISprite createDoorWBlank()
		{
			return new EnvironmentSprite(doors, 0, 1);
		}

		public ISprite createDoorWOpen()
		{
			return new EnvironmentSprite(doors, 1, 1);
		}

		public ISprite createDoorWLocked()
		{
			return new EnvironmentSprite(doors, 2, 1);
		}

		public ISprite createDoorWClosed()
		{
			return new EnvironmentSprite(doors, 3, 1);
		}

		public ISprite createDoorWHole()
		{
			return new EnvironmentSprite(doors, 4, 1);
		}

		public ISprite createDoorEBlank()
		{
			return new EnvironmentSprite(doors, 0, 2);
		}

		public ISprite createDoorEOpen()
		{
			return new EnvironmentSprite(doors, 1, 2);
		}

		public ISprite createDoorELocked()
		{
			return new EnvironmentSprite(doors, 2, 2);
		}

		public ISprite createDoorEClosed()
		{
			return new EnvironmentSprite(doors, 3, 2);
		}

		public ISprite createDoorEHole()
		{
			return new EnvironmentSprite(doors, 4, 2);
		}

		public ISprite createDoorSBlank()
		{
			return new EnvironmentSprite(doors, 0, 3);
		}

		public ISprite createDoorSOpen()
		{
			return new EnvironmentSprite(doors, 1, 3);
		}

		public ISprite createDoorSLocked()
		{
			return new EnvironmentSprite(doors, 2, 3);
		}

		public ISprite createDoorSClosed()
		{
			return new EnvironmentSprite(doors, 3, 3);
		}

		public ISprite createDoorSHole()
		{
			return new EnvironmentSprite(doors, 4, 3);
		}

		public ISprite createRoom()
        {
			return new EnvironmentSprite(room, 0, 0);
        }
	}
}
