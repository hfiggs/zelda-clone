using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Projectile
{
	class ProjectileSpriteFactory
	{
		private Texture2D projectileSpriteSheet;

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
			projectileSpriteSheet = content.Load<Texture2D>("images/projectiles");
		}

		public ProjectileSpriteSheet CreateSwordBeamSprite()
		{
			return new ProjectileSpriteSheet(projectileSpriteSheet, 5, 4, 1);
		}

		public ProjectileSpriteSheet CreateArrowSprite()
		{
			return new ProjectileSpriteSheet(projectileSpriteSheet, 5, 4, 0);
		}

		public ProjectileSpriteSheet CreateBoomerangSprite()
		{
			return new ProjectileSpriteSheet(projectileSpriteSheet, 5, 4, 3);
		}

		public ProjectileSpriteSheet CreateFireballsSprite()
		{
			return new ProjectileSpriteSheet(projectileSpriteSheet, 5, 4, 4);
		}
	}
}
