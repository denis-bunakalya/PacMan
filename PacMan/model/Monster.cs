using System;

namespace PacMan.model
{
    public abstract class Monster
    {
        public double Y { get; private set; }
        public double X { get; private set; }

        public readonly Direction Direction;
        internal double Step { private get; set; }

        protected readonly int StartY;
        protected readonly int StartX;

        protected readonly Cell[] Field;
        protected readonly int Height;
        protected readonly int Width;

        protected Monster(int y, int x, double step, Cell[] field, int height, int width)
        {
            if (!IsInFieldAndNotWall(y, x, field, height, width))
            {
                throw new ArgumentException("coordinates must be in field and not wall");
            }
            Y = y;
            X = x;

            Direction = new Direction();
            Step = step;

            StartY = y;
            StartX = x;

            Field = field;
            Height = height;
            Width = width;
        }

        internal void SetStartPosition()
        {
            Y = StartY;
            X = StartX;
        }

        protected void TryMove()
        {
            double y = Y;
            double x = X;
            Direction.MovePointByStep(ref y, ref x, Step);

            if (IsInt(y) || IsInt(x))
            {
                if (!IsInt(y) && (
                    !IsInFieldAndNotWall((int)y, Int(x), Field, Height, Width) ||
                    !IsInFieldAndNotWall((int)(y + 1), Int(x), Field, Height, Width)))
                {
                    return;
                }
                if (!IsInt(x) && (
                    !IsInFieldAndNotWall(Int(y), (int)x, Field, Height, Width) ||
                    !IsInFieldAndNotWall(Int(y), (int)(x + 1), Field, Height, Width)))
                {
                    return;
                }
            }
            else if (
                !IsInFieldAndNotWall((int)y, (int)x, Field, Height, Width) ||
                !IsInFieldAndNotWall((int)(y + 1), (int)x, Field, Height, Width) ||

                !IsInFieldAndNotWall((int)y, (int)(x + 1), Field, Height, Width) ||
                !IsInFieldAndNotWall((int)(y + 1), (int)(x + 1), Field, Height, Width))
            {
                return;
            }
            Y = y;
            X = x;
        }

        protected static bool IsInt(double d)
        {
            return (Math.Abs(d - Math.Round(d)) < 0.01);
        }

        public static int Int(double d)
        {
            if (double.IsNaN(d) || double.IsInfinity(d))
            {
                throw new ArgumentException("d must be a number");
            }
            return ((int)Math.Round(d));
        }

        public static bool IsInFieldAndNotWall(int y, int x, Cell[] field, int height, int width)
        {
            if (field == null)
            {
                throw new ArgumentException("field must be not null");
            }
            if ((height <= 0) || (width <= 0))
            {
                throw new ArgumentException("height and width must be positive");
            }
            if (field.Length != height * width)
            {
                throw new ArgumentException("field size must be equal to product of height and width");
            }
            return
                ((y > 0) &&
                (y < height) &&

                (x > 0) &&
                (x < width) &&

                (field[y * width + x] != Cell.Wall));
        }
    }
}
