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

		public EnvironmentSpriteFactory instance = new EnvironmentSpriteFactory();

		private EnvironmentSpriteFactory()
		{
		}

		public void LoadContent(ContentManager content)
		{
			Texture2D doors = content.Load<Texture2D>("images/ss_doors");
			Texture2D tiles = content.Load<Texture2D>("images/ss_tiles");
			this.doors = new SpriteSheet(doors, 5, 4);
			this.tiles = new SpriteSheet(tiles, 4, 3);
		}

		/*
		 * Factory methods for images found in ss_tiles.png
		 */

		public ISprite createFloor()
		{
			return new Floor(tiles);
		}

		public ISprite createBlock()
		{
			return new Block(tiles);
		}

		public ISprite createStatueFish()
		{
			return new StatueFish(tiles);
		}

		public ISprite createStatueDragon()
		{
			return new StatueDragon(tiles);
		}

		public ISprite createBlack()
		{
			return new Black(tiles);
		}

		public ISprite createSand()
        {
			return new Sand(tiles);
        }

		public ISprite createWater()
        {
			return new Water(tiles);
        }

		public ISprite createStairs()
        {
			return new Stairs(tiles);
        }

		public ISprite createBricks()
        {
			return new Bricks(tiles);
        }

		public ISprite createLadder()
        {
			return new Ladder(tiles);
        }
	}
}
