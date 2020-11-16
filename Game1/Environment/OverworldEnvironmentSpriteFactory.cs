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
		private const string floorSpriteFilePath = "images/Environment/Overworld/OverworldFloor", floorTileSpriteFilePath = "images/Environment/Overworld/OverworldFloorTile";
		private const string waterSpriteFilePath = "images/Environment/Overworld/OverworldWater", RockSpriteFilePath = "images/Environment/Overworld/OverworldRock", TreeSpriteFilePath = "images/Environment/Overworld/OverworldTree";
		private const int floorColumns = 1, floorRows = 1, floorTileColumns = 1, floorTileRows = 1, waterColumns = 1, waterRows = 1, rockColumns = 3, rockRows = 2, treeColumns = 3, treeRows = 2;
		private const int floorSpriteColumn = 0, floorSpriteRow = 0, floorTileColumn = 0, floorTileRow = 0, waterColumn = 0, waterRow = 0, plankColumn = 1, plankRow = 1;
		private const int treeTLColumn = 0, treeTLRow = 0, treeTMColumn = 1, treeTMRow = 0, treeTRColumn = 2, treeTRRow = 0, treeBLColumn = 0, treeBLRow = 1, treeBRColumn = 2, treeBRRow = 1;
		private const int rockTLColumn = 0, rockTLRow = 0, rockTMColumn = 1, rockTMRow = 0, rockTRColumn = 2, rockTRRow = 0, rockBLColumn = 0, rockBLRow = 1, rockBMColumn = 1, rockBMRow = 1, rockBRColumn = 2, rockBRRow = 1;

		public static OverworldEnvironmentSpriteFactory instance = new OverworldEnvironmentSpriteFactory();

		private const int overworldId = 999;

		private OverworldEnvironmentSpriteFactory()
		{
		}

		public void LoadContent(ContentManager content)
		{
			Texture2D overworldFloor = content.Load<Texture2D>(floorSpriteFilePath);
			this.overworldFloor = new SpriteSheet(overworldFloor, floorColumns, floorRows);

			Texture2D overworldFloorTile = content.Load<Texture2D>(floorTileSpriteFilePath);
			this.overworldFloorTile = new SpriteSheet(overworldFloorTile, floorTileColumns, floorTileRows);

			Texture2D overworldWater = content.Load<Texture2D>(waterSpriteFilePath);
			this.overworldWater = new SpriteSheet(overworldWater, waterColumns, waterRows);

			Texture2D overworldRock = content.Load<Texture2D>(RockSpriteFilePath);
			this.overworldRock = new SpriteSheet(overworldRock, rockColumns, rockRows);

			Texture2D overworldTree = content.Load<Texture2D>(TreeSpriteFilePath);
			this.overworldTree = new SpriteSheet(overworldTree, treeColumns, treeRows);
		}	

		public ISprite CreateOverworldFloor()
        {
			return new EnvironmentSprite(overworldFloor, floorSpriteColumn, floorSpriteRow, overworldId, false);
		}

		public ISprite CreateOverworldFloorTile()
		{
			return new EnvironmentSprite(overworldFloorTile, floorTileColumn, floorTileRow, overworldId, false);
		}

		public ISprite CreateOverworldWater()
        {
			return new EnvironmentSprite(overworldWater, waterColumn, waterRow, overworldId, false);
		}

		#region OVERWORLD_ROCK
		
		public ISprite CreateOverworldRockTL()
		{
			return new EnvironmentSprite(overworldRock, rockTLColumn, rockTLRow, overworldId, false);
		}

        public ISprite CreateOverworldRockTM()
		{
			return new EnvironmentSprite(overworldRock, rockTMColumn, rockTMRow, overworldId, false);
		}

		public ISprite CreateOverworldRockTR()
		{
			return new EnvironmentSprite(overworldRock, rockTRColumn, rockTRRow, overworldId, false);
		}

		public ISprite CreateOverworldRockBL()
		{
			return new EnvironmentSprite(overworldRock, rockBLColumn, rockBLRow, overworldId, false);
		}

		public ISprite CreateOverworldRockBM()
		{
			return new EnvironmentSprite(overworldRock, rockBMColumn, rockBMRow, overworldId, false);
		}

		public ISprite CreateOverworldRockBR()
		{
			return new EnvironmentSprite(overworldRock, rockBRColumn, rockBRRow, overworldId, false);
		}

		#endregion

		#region OVERWORLD_TREE
		
        public ISprite CreateOverworldTreeTL()
		{
			return new EnvironmentSprite(overworldTree, treeTLColumn, treeTLRow, overworldId, false);
		}

		public ISprite CreateOverworldTreeTM()
		{
			return new EnvironmentSprite(overworldTree, treeTMColumn, treeTMRow, overworldId, false);
		}

		public ISprite CreateOverworldTreeTR()
		{
			return new EnvironmentSprite(overworldTree, treeTRColumn, treeTRRow, overworldId, false);
		}

		public ISprite CreateOverworldTreeBL()
		{
			return new EnvironmentSprite(overworldTree, treeBLColumn, treeBLRow, overworldId, false);
		}

		public ISprite CreateOverworldTreeBR()
		{
			return new EnvironmentSprite(overworldTree, treeBRColumn, treeBRRow, overworldId, false);
		}

		#endregion

		public ISprite CreateOverworldPlank()
		{
			return new EnvironmentSprite(overworldTree, plankColumn, plankRow, overworldId, false);
		}
	}
}
