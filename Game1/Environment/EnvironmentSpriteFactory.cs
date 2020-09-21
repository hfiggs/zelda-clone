using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game1.Sprite;


namespace Game1.Environment
{
	public class EnvironmentSpriteFactory
	{
		private Texture2D doors;
		private Texture2D tiles;

		public EnvironmentSpriteFactory instance = new EnvironmentSpriteFactory();

		private EnvironmentSpriteFactory()
		{
		}

		public LoadContent()
		{
			this.doors = content.Load<Texture2D>("images/ss_doors");
			this.tiles = content.Load<Texture2D>("images/ss_tiles");
		}

		/*
		 * Factory methods for images found in ss_tiles.png
		 */

		public ISprite createFloor()
		{
			return new ItemSprite(tiles, 0, 0);
		}

		public ISprite createBlock()
		{
			return new ItemSprite(tiles, 0, 1);
		}

		public ISprite createStatueFish()
		{
			return new ItemSprite(tiles, 0, 2);
		}

		public ISprite createStatueDragon()
		{
			return new ItemSprite(tiles, 0, 3);
		}

		public ISprite createStatueDragon()
		{
			return new ItemSprite(tiles, 0, 3);
		}
	}
}
