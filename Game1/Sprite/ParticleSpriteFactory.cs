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
        private Texture2D beamExplosionSpritesheet;

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
            beamExplosionSpritesheet = content.Load<Texture2D>("images/sword_beam_exp");
        }

        public ISprite CreateCloudSprite()
        {
            return new ParticleSprite(cloudSpritesheet, 0, 0, 1, 3, 3);
        }

        public ISprite CreateBeamExplosionNWSprite()
        {
            return new ParticleSprite(beamExplosionSpritesheet, 0, 0, 4, 4, 4);
        }

        public ISprite CreateBeamExplosionNESprite()
        {
            return new ParticleSprite(beamExplosionSpritesheet, 1, 0, 4, 4, 4);
        }

        public ISprite CreateBeamExplosionSESprite()
        {
            return new ParticleSprite(beamExplosionSpritesheet, 2, 0, 4, 4, 4);
        }

        public ISprite CreateBeamExplosionSWSprite()
        {
            return new ParticleSprite(beamExplosionSpritesheet, 3, 0, 4, 4, 4);
        }
    }
}
