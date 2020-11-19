/* Author: Hunter Figgs */

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Game1.Util
{
    public class ColorIterator
    {
        private readonly Color[] colorList;

        private readonly float colorDuration; // ms
        private float colorTimer; // ms

        private int currentColor;

        public ColorIterator(List<Color> colorList, float colorDuration)
        {
            this.colorList = new Color[colorList.Count + 1];

            for(var i = 0; i < colorList.Count; i++)
            {
                this.colorList[i] = colorList.ElementAt(i);
            }

            this.colorDuration = colorDuration;

            colorTimer = colorDuration;

            currentColor = 0;
        }

        public void Update(GameTime gameTime)
        {
            colorTimer -= (float) gameTime.ElapsedGameTime.TotalMilliseconds;

            if (colorTimer <= 0)
            {
                currentColor = (currentColor + 1) % colorList.Length;

                colorTimer += colorDuration;
            }
        }

        public Color GetColor(Color defaultColor)
        {
            colorList[colorList.Length - 1] = defaultColor;

            return colorList[currentColor];
        }
    }
}
