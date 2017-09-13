using System;
using System.Collections.Generic;
using System.Linq;

namespace PacMan.model
{
    public sealed class Direction
    {
        internal static readonly Dictionary<string, Point> ByName = new Dictionary<string, Point>
            {
                { "Up", new Point { Y = -1, X = 0 } },
                { "Right", new Point { Y = 0, X = 1 } },
                { "Left", new Point { Y = 0, X = -1 } },
                { "Down", new Point { Y = 1, X = 0 } }
            };

        internal static readonly Dictionary<Point, string> GetName = new Dictionary<Point, string>();
        internal static readonly Point[] Array = ByName.Values.ToArray();
        private static readonly Random Random = new Random();

        static Direction()
        {
            foreach (var pair in ByName)
            {
                GetName[pair.Value] = pair.Key;
            }
        }

        public Point Current { get; internal set; }
        internal Point New { get; set; }

        internal Direction()
        {
            Current = ByName["Up"];
            New = ByName["Up"];
        }

        internal void MovePointByStep(ref double y, ref double x, double step)
        {
            y += Current.Y * step;
            x += Current.X * step;
        }

        internal void MovePoint(ref int y, ref int x)
        {
            y += Current.Y;
            x += Current.X;
        }

        public static void MovePoint(ref int y, ref int x, Point direction)
        {
            y += direction.Y;
            x += direction.X;
        }

        internal void SetRandom()
        {
            Current = Array[Random.Next(Array.Length)];
        }

        internal void SetReverseFrom(Point direction)
        {
            Current = new Point { Y = -direction.Y, X = -direction.X };
        }

        public static Point Reverse(Point direction)
        {
            return (new Point { Y = -direction.Y, X = -direction.X });
        }

        public override string ToString()
        {
            return GetName[Current];
        }
    }
}
