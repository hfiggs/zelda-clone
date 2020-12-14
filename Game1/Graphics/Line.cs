using System;

namespace Game1.Graphics
{
    public class Line
    {
        public float X1 { get; set; }
        public float Y1 { get; set; }
        public float X2 { get; set; }
        public float Y2 { get; set; }

        private const double TWO = 2.0;

        public Line(float x1, float y1, float x2, float y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public float GetAngle()
        {
            return (float)Math.Atan2((double)Y2 - Y1, (double)X2 - X1);
        }

        public float GetLength()
        {
            return (float)Math.Sqrt(Math.Pow((double)X1 - X2, TWO) + Math.Pow((double)Y1 - Y2, TWO));
        }

        public void Rotate(float angle)
        {
            var hypotenuse = GetLength();

            var newAngle = GetAngle() + angle;

            var opposite = hypotenuse * (float)Math.Sin(newAngle);

            var adjacent = hypotenuse * (float)Math.Cos(newAngle);

            X2 = adjacent + X1;

            Y2 = opposite + Y1;
        }

        public void Elongate()
        {
            // TODO: make more better

            X2 += 100*(X2-X1);

            Y2 += 100*(Y2-Y1);
        }
    }
}
