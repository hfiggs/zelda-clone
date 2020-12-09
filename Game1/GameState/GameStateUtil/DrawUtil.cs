using Game1.HUD;
using Game1.ResolutionManager;
using Game1.RoomLoading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.GameState.GameStateUtil
{
    public static class DrawUtil
    {
        private static readonly Color color = Color.White;

        private const float roomOffset = 40f;
        private static readonly float hudOffset = -136f;

        public static void DrawScreen(Screen screen, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            var drawMatrix = resolutionManager.GetResolutionMatrix();

            drawMatrix.Translation = new Vector3(0, roomOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            screen.Draw(spriteBatch, color);

            spriteBatch.End();

            ResetTranslation(drawMatrix);
        }

        public static void DrawHUDOffset(HUDInterface hud, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            var drawMatrix = resolutionManager.GetResolutionMatrix();

            drawMatrix.Translation = new Vector3(0, hudOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, drawMatrix);

            hud.Draw(spriteBatch, new Vector2(0, 0), color);

            spriteBatch.End();

            ResetTranslation(drawMatrix);
        }

        private static void ResetTranslation(Matrix drawMatrix)
        {
            drawMatrix.Translation = new Vector3(0, 0, 0);
        }
    }
}
