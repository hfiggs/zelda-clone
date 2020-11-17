using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Sprite
{
    class ParticleSpriteFactory
    {
        private Texture2D cloudSpritesheet;
        private Texture2D beamExplosionSpritesheet;
        private Texture2D shieldDeflectSprite;
        private Texture2D waterfallSpritesheet;
        private Texture2D waterfallSpraySpritesheet;

        private const int CloudColumn = 0, CloudRow = 0, CloudMaxColumns = 1, CloudMaxRows = 3, CloudTotalFrames = 3;
        private const int BeamExplosionNWColumn = 0, BeamExplosionNWRow = 0, BeamExplosionMaxColumns = 4, BeamExplosionMaxRows = 4, BeamExplosionTotalFrames = 4;
        private const int BeamExplosionNEColumn = 1, BeamExplosionNERow = 0, BeamExplosionSEColumn = 2, BeamExplosionSERow = 0, BeamExplosionSWColumn = 3, BeamExplosionSWRow = 0;
        private const int ShieldDeflectColumn = 0, ShieldDeflectRow = 0, ShieldDeflectMaxColumns = 1, ShieldDeflectMaxRows = 1, ShieldDeflectTotalFrames = 1;
        private const int WaterfallColumn = 0, WaterfallRow = 0, WaterfallMaxColumns = 1, WaterfallMaxRows = 3, WaterfallTotalFrames = 3;
        private const int WaterfallSprayColumn = 0, WaterfallSprayRow = 0, WaterfallSprayMaxColumns = 1, WaterfallSprayMaxRows = 2, WaterfallSprayTotalFrames = 2;
        private const int CurtainAndFlashOverlayColumn = 1, CurtainAndFlashOverlayRow = 1, CurtainAndFlashOverlayMaxColumns = 1, CurtainAndFlashOverlayMaxRows = 1, CurtainAndFlashOverlayTotalFrames = 1;

        private const string cloudFilePath = "images/Particle/cloud", beamExplosionFilePath = "images/Particle/sword_beam_exp", shieldDeflectFilePath = "images/Particle/shield_deflect", waterfallFilePath = "images/Start/Waterfall", waterfallSprayFilePath = "images/Start/WaterfallSpray";

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
            cloudSpritesheet = content.Load<Texture2D>(cloudFilePath);
            beamExplosionSpritesheet = content.Load<Texture2D>(beamExplosionFilePath);
            shieldDeflectSprite = content.Load<Texture2D>(shieldDeflectFilePath);
            waterfallSpritesheet = content.Load<Texture2D>(waterfallFilePath);
            waterfallSpraySpritesheet = content.Load<Texture2D>(waterfallSprayFilePath);
            graphics = graphicsDevice;
        }
        
        public ISprite CreateCloudSprite()
        {
            return new ParticleSprite(cloudSpritesheet, CloudColumn, CloudRow, CloudMaxColumns, CloudMaxRows, CloudTotalFrames);
        }

        public ISprite CreateBeamExplosionNWSprite()
        {
            return new ParticleSprite(beamExplosionSpritesheet, BeamExplosionNWColumn, BeamExplosionNWRow, BeamExplosionMaxColumns, BeamExplosionMaxRows, BeamExplosionTotalFrames);
        }

        public ISprite CreateBeamExplosionNESprite()
        {
            return new ParticleSprite(beamExplosionSpritesheet, BeamExplosionNEColumn, BeamExplosionNERow, BeamExplosionMaxColumns, BeamExplosionMaxRows, BeamExplosionTotalFrames);
        }

        public ISprite CreateBeamExplosionSESprite()
        {
            return new ParticleSprite(beamExplosionSpritesheet, BeamExplosionSEColumn, BeamExplosionSERow, BeamExplosionMaxColumns, BeamExplosionMaxRows, BeamExplosionTotalFrames);
        }

        public ISprite CreateBeamExplosionSWSprite()
        {
            return new ParticleSprite(beamExplosionSpritesheet, BeamExplosionSWColumn, BeamExplosionSWRow, BeamExplosionMaxColumns, BeamExplosionMaxRows, BeamExplosionTotalFrames);
        }

        public ISprite CreateFlashOverlay(Color color)
        {
            Texture2D rect = new Texture2D(graphics, graphics.Viewport.Width, graphics.Viewport.Height);

            Color[] data = new Color[graphics.Viewport.Width * graphics.Viewport.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = color;
            rect.SetData(data);

            return new ParticleSprite(rect, CurtainAndFlashOverlayColumn, CurtainAndFlashOverlayRow, CurtainAndFlashOverlayMaxColumns, CurtainAndFlashOverlayMaxRows, CurtainAndFlashOverlayTotalFrames);
        }
        
        public ISprite CreateShieldDeflect()
        {
            return new ParticleSprite(shieldDeflectSprite, ShieldDeflectColumn, ShieldDeflectRow, ShieldDeflectMaxColumns, ShieldDeflectMaxRows, ShieldDeflectTotalFrames);
        }

        public ISprite CreateWaterfall()
        {
            return new ParticleSprite(waterfallSpritesheet, WaterfallColumn, WaterfallRow, WaterfallMaxColumns, WaterfallMaxRows, WaterfallTotalFrames);
        }

        public ISprite CreateWaterfallSpray()
        {
            return new ParticleSprite(waterfallSpraySpritesheet, WaterfallSprayColumn, WaterfallSprayRow, WaterfallSprayMaxColumns, WaterfallSprayMaxRows, WaterfallSprayTotalFrames);
        }
        
        public ISprite CreateCurtain(Color color, Rectangle area)
        {
            Texture2D rect = new Texture2D(graphics, area.Width, area.Height);

            Color[] data = new Color[area.Width * area.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = color;
            rect.SetData(data);

            return new ParticleSprite(rect, CurtainAndFlashOverlayColumn, CurtainAndFlashOverlayRow, CurtainAndFlashOverlayMaxColumns, CurtainAndFlashOverlayMaxRows, CurtainAndFlashOverlayTotalFrames);
        }
    }
}
