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
			//TODO: Concrete implementations of the ISprite class (should have position)
			//TODO: Using SpriteSheet, create instances of those concrete classes and return them(all in the sprite factory)
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
