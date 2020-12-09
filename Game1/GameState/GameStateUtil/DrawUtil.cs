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

        private static readonly float zeroOffset = 0f;
        private static readonly float roomOffset = 40f;
        private static readonly float hudOffset = -136f;

        public static void DrawScreen(Screen screen, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            SetupDraw(spriteBatch, resolutionManager, zeroOffset, roomOffset);

            screen.Draw(spriteBatch, color);

            EndDraw(spriteBatch, resolutionManager);
        }

        public static void DrawRoom(Room room, SpriteBatch spriteBatch, IResolutionManager resolutionManager, Vector2 offset)
        {
            SetupDraw(spriteBatch, resolutionManager, offset.X, offset.Y);

            room.Draw(spriteBatch, color);

            EndDraw(spriteBatch, resolutionManager);
        }

        public static void DrawHUDOffset(HUDInterface hud, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            SetupDraw(spriteBatch, resolutionManager, zeroOffset, hudOffset);

            hud.Draw(spriteBatch, new Vector2(0, 0), color);

            EndDraw(spriteBatch, resolutionManager);
        }

        private static void EndDraw(SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            var drawMatrix = resolutionManager.GetResolutionMatrix();

            drawMatrix.Translation = new Vector3(0, 0, 0);

            spriteBatch.End();
        }

        private static void SetupDraw(SpriteBatch spriteBatch, IResolutionManager resolutionManager, float xOffset = 0f, float yOffset = 0f)
        {
            var drawMatrix = resolutionManager.GetResolutionMatrix();

            drawMatrix.Translation = new Vector3(xOffset * resolutionManager.GetResolutionScale(), yOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, drawMatrix);
        }
    }
}
