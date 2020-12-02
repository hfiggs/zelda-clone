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

		private Texture2D portalProjectileSpriteSheet;

		private static ProjectileSpriteFactory instance = new ProjectileSpriteFactory();
		private const string portalProjectileSpriteSheetFilePath = "Images/Projectile/PortalProjectile";

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
			portalProjectileSpriteSheet = content.Load<Texture2D>(portalProjectileSpriteSheetFilePath);
		}

		public ISprite CreatePortalProjectileSprite()
        {
			return new ParticleSprite(portalProjectileSpriteSheet, 0, 0, 1, 4, 4);
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
	}
}
