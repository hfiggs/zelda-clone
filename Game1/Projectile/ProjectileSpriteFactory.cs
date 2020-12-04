using Game1.Sprite;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Projectile
{
	class ProjectileSpriteFactory
	{
		private Texture2D projectileSpriteSheet;
		private const int projectileColumns = 6, projectileRows = 4, swordBeamColumn = 1, arrowColumn = 0, boomerangColumn = 3, fireballColumn = 4, bombColumn = 5, bombRow = 0, bombTotalFrames = 1;
		private const string projectileSpriteFilePath = "Images/Projectile/Projectiles";

		private Texture2D portalBlueProjectileSpriteSheet;
		private Texture2D portalOrangeProjectileSpriteSheet;
		private const string portalBlueProjectileSpriteSheetFilePath = "Images/Projectile/PortalBlueProjectile";
		private const string portalOrangeProjectileSpriteSheetFilePath = "Images/Projectile/PortalOrangeProjectile";

		private static ProjectileSpriteFactory instance = new ProjectileSpriteFactory();
		

		public static ProjectileSpriteFactory Instance
		{
			get
			{
				return instance;
			}
		}

		private ProjectileSpriteFactory()
		{
		}

		public void LoadAllTextures(ContentManager content)
		{
			projectileSpriteSheet = content.Load<Texture2D>(projectileSpriteFilePath);
			portalBlueProjectileSpriteSheet = content.Load<Texture2D>(portalBlueProjectileSpriteSheetFilePath);
			portalOrangeProjectileSpriteSheet = content.Load<Texture2D>(portalOrangeProjectileSpriteSheetFilePath);
		}

		public ProjectileSpriteSheet CreateSwordBeamSprite()
		{
			return new ProjectileSpriteSheet(projectileSpriteSheet, projectileColumns, projectileRows, swordBeamColumn);
		}

		public ProjectileSpriteSheet CreateArrowSprite()
		{
			return new ProjectileSpriteSheet(projectileSpriteSheet, projectileColumns, projectileRows, arrowColumn);
		}

		public ProjectileSpriteSheet CreateBoomerangSprite()
		{
			return new ProjectileSpriteSheet(projectileSpriteSheet, projectileColumns, projectileRows, boomerangColumn);
		}

		public ProjectileSpriteSheet CreateFireballsSprite()
		{
			return new ProjectileSpriteSheet(projectileSpriteSheet, projectileColumns, projectileRows, fireballColumn);
		}
		public ISprite CreateBombProjectileSprite()
		{
			return new ProjectileSprite(projectileSpriteSheet, bombColumn, bombRow, projectileColumns, projectileRows, bombTotalFrames);
		}

		#region Portal Projectiles

		// Blue Portal

		public ISprite CreatePortalBlueDownProjectileSprite()
		{
			return new ParticleSprite(portalBlueProjectileSpriteSheet, 0, 0, 4, 4, 4);
		}

		public ISprite CreatePortalBlueLeftProjectileSprite()
		{
			return new ParticleSprite(portalBlueProjectileSpriteSheet, 1, 0, 4, 4, 4);
		}

		public ISprite CreatePortalBlueRightProjectileSprite()
		{
			return new ParticleSprite(portalBlueProjectileSpriteSheet, 2, 0, 4, 4, 4);
		}

		public ISprite CreatePortalBlueUpProjectileSprite()
		{
			return new ParticleSprite(portalBlueProjectileSpriteSheet, 3, 0, 4, 4, 4);
		}

		// Blue Orange

		public ISprite CreatePortalOrangeDownProjectileSprite()
		{
			return new ParticleSprite(portalOrangeProjectileSpriteSheet, 0, 0, 4, 4, 4);
		}

		public ISprite CreatePortalOrangeLeftProjectileSprite()
		{
			return new ParticleSprite(portalOrangeProjectileSpriteSheet, 1, 0, 4, 4, 4);
		}

		public ISprite CreatePortalOrangeRightProjectileSprite()
		{
			return new ParticleSprite(portalOrangeProjectileSpriteSheet, 2, 0, 4, 4, 4);
		}

		public ISprite CreatePortalOrangeUpProjectileSprite()
		{
			return new ParticleSprite(portalOrangeProjectileSpriteSheet, 3, 0, 4, 4, 4);
		}

		#endregion

	}
}
