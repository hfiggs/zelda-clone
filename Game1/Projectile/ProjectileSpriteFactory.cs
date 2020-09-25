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
		private Texture2D projectileSpritesheet;

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

		public ISprite CreateSwordBeamSprite()
		{
			return new SwordBeamSprite(projectileSpritesheet, 5, 4, 1);
		}

		public ISprite CreateArrowSprite()
		{
			return new ArrowSprite(projectileSpritesheet, 5, 4, 0);
		}

		public ISprite CreateBoomerangSprite()
		{
			return new BoomerangSprite(projectileSpritesheet, 5, 4, 3);
		}

		public ISprite CreateFireballsSprite()
		{
			return new FireballSprite(projectileSpritesheet, 5, 4, 4);
		}
	}
}
