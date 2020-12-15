using Game1.GameState.GameStateUtil;
using Game1.ResolutionManager;
using Game1.RoomLoading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game1.Graphics
{
    public static class ShadowMask
    {
        private static readonly float gradientStep = 120f;
        private static readonly float lineTolerance = 3f;
        private static readonly float lineStep = 1f;

        private static Texture2D textureTemp;

        private static Texture2D simpleTexture = null;

        public static List<Texture2D> toDispose = new List<Texture2D>();

        private static List<Line> lineList = new List<Line>();

        // Blend state source: https://gist.github.com/Jjagg/db34a25897e20dbdb0f16cb5bcb75493

        private static readonly BlendState blendState = new BlendState
        {
            ColorSourceBlend = Blend.Zero, // multiplier of the source color
            ColorBlendFunction = BlendFunction.Add, // function to combine colors
            ColorDestinationBlend = Blend.SourceAlpha, // multiplier of the destination color
            AlphaSourceBlend = Blend.One, // multiplier of the source alpha
            AlphaBlendFunction = BlendFunction.Add, // function to combine alpha
            AlphaDestinationBlend = Blend.Zero, // multiplier of the destination alpha
        };

        public static void SetMaskData(Screen screen)
        {
            if (!screen.CurrentRoom.RoomMeta.IsLit)
                lineList = Raycast.GetRaycastLines(screen);
        }

        public static Texture2D GetShadowMask(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            var renderTarget = new RenderTarget2D(graphicsDevice, 256, 176);

            InitSimpleTexture(graphicsDevice);

            graphicsDevice.SetRenderTarget(renderTarget);

            graphicsDevice.Clear(new Color(Color.Black, Util.ShadowMaskAlpha));

            spriteBatch.Begin(blendState: blendState);

            foreach (Line line in lineList)
            {
                SetRayTexture(graphicsDevice, line);

                spriteBatch.Draw(textureTemp, new Rectangle(new Point((int)line.X1, (int)line.Y1), new Point((int)line.GetLength(), 1)), null, Color.White, line.GetAngle(), new Vector2(0, 0), SpriteEffects.None, 1.0f);

                toDispose.Add(textureTemp);
            }

            var sortedLines = lineList;

            sortedLines.Sort(new LineSorter());

            for (var i = 0; i < sortedLines.Count(); i++)
            {
                var line0 = sortedLines.ElementAt<Line>(i);
                var line1 = sortedLines.ElementAt<Line>((i + 1) % sortedLines.Count);

                if (Math.Abs(line0.X2 - line1.X2) < lineTolerance)
                {
                    for (var k = 0f; k < Math.Abs(line0.Y2 - line1.Y2) / lineStep; k++)
                    {
                        var line = new Line(line0.X1, line0.Y1, line0.X2, Math.Min(line0.Y2, line1.Y2) + k * lineStep);

                        SetRayTexture(graphicsDevice, line);

                        spriteBatch.Draw(textureTemp, new Rectangle(new Point((int)line.X1, (int)line.Y1), new Point((int)line.GetLength(), 1)), null, Color.White, line.GetAngle(), new Vector2(0, 0), SpriteEffects.None, 1.0f);

                        toDispose.Add(textureTemp);
                    }
                }
                
                if (Math.Abs(line0.Y2 - line1.Y2) < lineTolerance)
                {
                    for (var k = 0f; k < Math.Abs(line0.X2 - line1.X2) / lineStep; k++)
                    {
                        var line = new Line(line0.X1, line0.Y1, Math.Min(line0.X2, line1.X2) + k * lineStep, line0.Y2);

                        SetRayTexture(graphicsDevice, line);

                        spriteBatch.Draw(textureTemp, new Rectangle(new Point((int)line.X1, (int)line.Y1), new Point((int)line.GetLength(), 1)), null, Color.White, line.GetAngle(), new Vector2(0, 0), SpriteEffects.None, 1.0f);

                        toDispose.Add(textureTemp);
                    }
                }
            }

            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);

            return renderTarget;
        }

        public static Texture2D GetBlankShadowMask(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            var targets = graphicsDevice.GetRenderTargets();

            var renderTarget = new RenderTarget2D(graphicsDevice, 256, 176);

            graphicsDevice.SetRenderTarget(renderTarget);

            graphicsDevice.Clear(new Color(Color.Black, Util.ShadowMaskAlpha));

            graphicsDevice.SetRenderTargets(targets);

            return renderTarget;
        }

        private static void SetRayTexture(GraphicsDevice graphicsDevice, Line line)
        {
            textureTemp = new Texture2D(graphicsDevice, (int)line.GetLength(), 1, false, SurfaceFormat.Color);
            var gradientData = new Color[textureTemp.Width];
            for (var x = 0; x < gradientData.Length; x++)
            {
                var alpha = x / gradientStep;

                if (alpha > Util.ShadowMaskAlpha)
                    alpha = Util.ShadowMaskAlpha;

                gradientData[x] = new Color(Color.Black, alpha);
            }

            textureTemp.SetData(gradientData);
        }

        private static void InitSimpleTexture(GraphicsDevice graphicsDevice)
        {
            if (simpleTexture == null)
            {
                simpleTexture = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
                simpleTexture.SetData(new[] { new Color(Color.Black, 0f) });
            }
        }
    }
}
