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

		public Instance = new EnvironmentSpriteFactory();

		private EnvironmentSpriteFactory()
		{
		}

		public LoadContent()
		{
			this.doors = content.Load<Texture2D>("images/ss_doors");
			this.tiles = content.Load<Texture2D>("images/ss_tiles");
		}

		public ISprite createFloor(Vector2 position)
        {
			return new NotAnimatedNotMovingSprite(tiles, 0, 0, position);
        }
	}
}
