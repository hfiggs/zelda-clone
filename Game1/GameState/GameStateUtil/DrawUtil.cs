using Game1.Graphics;
using Game1.HUD;
using Game1.Player;
using Game1.ResolutionManager;
using Game1.RoomLoading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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

        public static void DrawScreen(Screen screen, SpriteBatch spriteBatch, IResolutionManager resolutionManager, Vector2 offset)
        {
            SetupDraw(spriteBatch, resolutionManager, offset.X, offset.Y);

            screen.Draw(spriteBatch, color);

            EndDraw(spriteBatch, resolutionManager);
        }

        public static void DrawRoom(Room room, SpriteBatch spriteBatch, IResolutionManager resolutionManager, Vector2 offset)
        {
            SetupDraw(spriteBatch, resolutionManager, offset.X, offset.Y);

            room.Draw(spriteBatch, color);

            EndDraw(spriteBatch, resolutionManager);
        }

        public static void DrawRoomAndPlayers(Room room, List<IPlayer> playerList, SpriteBatch spriteBatch, IResolutionManager resolutionManager, Vector2 offset)
        {
            SetupDraw(spriteBatch, resolutionManager, offset.X, offset.Y);

            room.Draw(spriteBatch, color);

            playerList.ForEach(p => p.Draw(spriteBatch, color));

            EndDraw(spriteBatch, resolutionManager);
        }

        public static void DrawHUD(HUDInterface hud, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            SetupDraw(spriteBatch, resolutionManager, zeroOffset, hudOffset);

            hud.Draw(spriteBatch, new Vector2(0, 0), color);

            EndDraw(spriteBatch, resolutionManager);
        }

        public static void DrawHUD(HUDInterface hud, SpriteBatch spriteBatch, IResolutionManager resolutionManager, Vector2 offset)
        {
            SetupDraw(spriteBatch, resolutionManager, offset.X, offset.Y);

            hud.Draw(spriteBatch, new Vector2(0, 0), color);

            EndDraw(spriteBatch, resolutionManager);
        }

        public static void DrawShadowMask(Texture2D shadowMask, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            SetupDraw(spriteBatch, resolutionManager, zeroOffset, roomOffset);

            spriteBatch.Draw(shadowMask, new Rectangle(0, 0, 256, 176), Color.White);

            EndDraw(spriteBatch, resolutionManager);

            ShadowMask.toDispose.ForEach(t => t.Dispose());

            ShadowMask.toDispose.Clear();

            shadowMask.Dispose();
        }

        public static void DrawShadowMask(Texture2D shadowMask, SpriteBatch spriteBatch, IResolutionManager resolutionManager, Vector2 offset)
        {
            SetupDraw(spriteBatch, resolutionManager, offset.X, offset.Y);

            spriteBatch.Draw(shadowMask, new Rectangle(0, 0, 256, 176), Color.White);

            EndDraw(spriteBatch, resolutionManager);

            ShadowMask.toDispose.ForEach(t => t.Dispose());

            ShadowMask.toDispose.Clear();

            shadowMask.Dispose();
        }

        /*                var bsInverter = new BlendState()
    {
        ColorSourceBlend = Blend.Zero,
        ColorDestinationBlend = Blend.InverseSourceColor,
    };

    var drawMatrix = resolutionManager.GetResolutionMatrix();
    drawMatrix.Translation = new Vector3(0, 40f * resolutionManager.GetResolutionScale(), 0);

    spriteBatch.Begin(blendState: bsInverter, samplerState: SamplerState.PointClamp, transformMatrix: drawMatrix);

    Raycast.DrawRaycastLines(Raycast.GetHitboxes(game.Screen.CurrentRoom), centerPoint, game.GraphicsDevice, spriteBatch);

    spriteBatch.End();*/

        public static void ClearScreen(Game1 game)
        {
            game.GraphicsDevice.Clear(Color.Black);
        }

        private static void EndDraw(SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            spriteBatch.End();

            var drawMatrix = resolutionManager.GetResolutionMatrix();

            drawMatrix.Translation = new Vector3(0, 0, 0);
        }

        private static void SetupDraw(SpriteBatch spriteBatch, IResolutionManager resolutionManager, float xOffset = 0f, float yOffset = 0f)
        {
            var drawMatrix = resolutionManager.GetResolutionMatrix();

            drawMatrix.Translation = new Vector3(xOffset * resolutionManager.GetResolutionScale(), yOffset * resolutionManager.GetResolutionScale(), 0);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, drawMatrix);
        }
    }
}
