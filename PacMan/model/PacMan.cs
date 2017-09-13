using System;

namespace PacMan.model
{
    public sealed class PacMan : Monster
    {
        private const double PacManStep = 0.3333333;

        internal PacMan(int y, int x, Cell[] field, int height, int width)
            : base(y, x, PacManStep, field, height, width)
        {
        }

        internal void Move()
        {
            double y = Y;
            double x = X;

            Point oldDirection = Direction.Current;
            Direction.Current = Direction.New;
            TryMove();

            if ((Math.Abs(y - Y) < 0.01) &&
                (Math.Abs(x - X) < 0.01))
            {
                Direction.Current = oldDirection;
                TryMove();
            }
        }

        public override string ToString()
        {
            return "PacMan";
        }
    }
}
