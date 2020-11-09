using Game1.Enemy;
using Game1.Environment;
using Game1.HUD;
using Game1.Player;
using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    public static class ContentUtil
    {
        public static void LoadAllContent(ContentManager content)
        {
            PlayerSpriteFactory.Instance.LoadAllTextures(content);

            ProjectileSpriteFactory.Instance.LoadAllTextures(content);

            ItemSpriteFactory.Instance.LoadAllTextures(content);

            EnvironmentSpriteFactory.instance.LoadContent(content);

            EnemySpriteFactory.Instance.LoadAllTextures(content);

            ParticleSpriteFactory.Instance.LoadAllTextures(content);

            HUDItemFactory.Instance.LoadAllTextures(content);

            AudioManager.LoadContent(content);
        }
    }
}
