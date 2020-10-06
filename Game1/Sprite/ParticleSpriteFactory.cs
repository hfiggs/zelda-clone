using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Sprite
{
    class ParticleSpriteFactory
    {
        private Texture2D cloudSpritesheet;

        private static ParticleSpriteFactory instance = new ParticleSpriteFactory();

        public static ParticleSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private ParticleSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            cloudSpritesheet = content.Load<Texture2D>("images/cloud");
        }

        public ISprite CreateCloudSprite()
        {
            return new ParticleSprite(cloudSpritesheet, 0, 0, 1, 3, 3);
        }
    }
}
