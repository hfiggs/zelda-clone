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
        private Texture2D shieldDeflectSprite;

        private GraphicsDevice graphics;

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

        public void LoadAllTextures(ContentManager content, GraphicsDevice graphicsDevice)
        {
            cloudSpritesheet = content.Load<Texture2D>("images/Particle/cloud");
            beamExplosionSpritesheet = content.Load<Texture2D>("images/Particle/sword_beam_exp");
            shieldDeflectSprite = content.Load<Texture2D>("images/Particle/shield_deflect");
            graphics = graphicsDevice;
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

        public ISprite CreateFlashOverlay(Color color)
        {
            Texture2D rect = new Texture2D(graphics, graphics.Viewport.Width, graphics.Viewport.Height);

            Color[] data = new Color[graphics.Viewport.Width * graphics.Viewport.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = color;
            rect.SetData(data);

            return new ParticleSprite(rect, 1, 1, 1, 1, 1);
        }

        public ISprite CreateShieldDeflect()
        {
            return new ParticleSprite(shieldDeflectSprite, 0, 0, 1, 1, 1);
        }
    }
}
