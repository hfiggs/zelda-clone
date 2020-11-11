/* Author: Hunter Figgs.3 */

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game1.Sprite;

namespace Game1.Environment
{
	public class OverworldEnvironmentSpriteFactory
	{
		private SpriteSheet overworldFloor;
		private SpriteSheet overworldFloorTile;
		private SpriteSheet overworldWater;
		private SpriteSheet overworldRock;
		private SpriteSheet overworldTree;

		public static OverworldEnvironmentSpriteFactory instance = new OverworldEnvironmentSpriteFactory();

		private const int overworldId = 999;

		private OverworldEnvironmentSpriteFactory()
		{
		}

		public void LoadContent(ContentManager content)
		{
			Texture2D overworldFloor = content.Load<Texture2D>("images/Environment/Overworld/OverworldFloor");
			this.overworldFloor = new SpriteSheet(overworldFloor, 1, 1);

			Texture2D overworldFloorTile = content.Load<Texture2D>("images/Environment/Overworld/OverworldFloorTile");
			this.overworldFloorTile = new SpriteSheet(overworldFloorTile, 1, 1);

			Texture2D overworldWater = content.Load<Texture2D>("images/Environment/Overworld/OverworldWater");
			this.overworldWater = new SpriteSheet(overworldWater, 1, 1);

			Texture2D overworldRock = content.Load<Texture2D>("images/Environment/Overworld/OverworldRock");
			this.overworldRock = new SpriteSheet(overworldRock, 3, 2);

			Texture2D overworldTree = content.Load<Texture2D>("images/Environment/Overworld/OverworldTree");
			this.overworldTree = new SpriteSheet(overworldTree, 3, 2);
		}

		public ISprite CreateOverworldFloor()
        {
			return new EnvironmentSprite(overworldFloor, 0, 0, overworldId, false);
		}

		public ISprite CreateOverworldFloorTile()
		{
			return new EnvironmentSprite(overworldFloorTile, 0, 0, overworldId, false);
		}

		public ISprite CreateOverworldWater()
        {
			return new EnvironmentSprite(overworldWater, 0, 0, overworldId, false);
		}

		#region OVERWORLD_ROCK

		public ISprite CreateOverworldRockTL()
		{
			return new EnvironmentSprite(overworldRock, 0, 0, overworldId, false);
		}

        public ISprite CreateOverworldRockTM()
		{
			return new EnvironmentSprite(overworldRock, 1, 0, overworldId, false);
		}

		public ISprite CreateOverworldRockTR()
		{
			return new EnvironmentSprite(overworldRock, 2, 0, overworldId, false);
		}

		public ISprite CreateOverworldRockBL()
		{
			return new EnvironmentSprite(overworldRock, 0, 1, overworldId, false);
		}

		public ISprite CreateOverworldRockBM()
		{
			return new EnvironmentSprite(overworldRock, 1, 1, overworldId, false);
		}

		public ISprite CreateOverworldRockBR()
		{
			return new EnvironmentSprite(overworldRock, 2, 1, overworldId, false);
		}

        #endregion

        #region OVERWORLD_TREE

        public ISprite CreateOverworldTreeTL()
		{
			return new EnvironmentSprite(overworldTree, 0, 0, overworldId, false);
		}

		public ISprite CreateOverworldTreeTM()
		{
			return new EnvironmentSprite(overworldTree, 1, 0, overworldId, false);
		}

		public ISprite CreateOverworldTreeTR()
		{
			return new EnvironmentSprite(overworldTree, 2, 0, overworldId, false);
		}

		public ISprite CreateOverworldTreeBL()
		{
			return new EnvironmentSprite(overworldTree, 0, 1, overworldId, false);
		}

		public ISprite CreateOverworldTreeBR()
		{
			return new EnvironmentSprite(overworldTree, 2, 1, overworldId, false);
		}

		#endregion

		public ISprite CreateOverworldPlank()
		{
			return new EnvironmentSprite(overworldTree, 1, 1, overworldId, false);
		}
	}
}
