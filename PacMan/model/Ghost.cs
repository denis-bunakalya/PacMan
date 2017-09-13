using System;
using System.Collections.Generic;

namespace PacMan.model
{
    public abstract class Ghost : Monster
    {
        protected internal PacMan PacMan { protected get; set; }
        protected internal bool IsSlowed { get; set; }

        private readonly int[] _distances;
        private readonly HashSet<Point> _visited;
        private readonly Queue<Point> _queue;

        internal const double GhostStep = 0.1;
        internal const double SlowedStep = 0.05;

        protected Ghost(int y, int x, Cell[] field, int height, int width)
            : base(y, x, GhostStep, field, height, width)
        {
            _distances = new int[height * width];
            _visited = new HashSet<Point>();
            _queue = new Queue<Point>();
        }

        protected abstract void AiDirection();

        internal void Move()
        {
            if (IsInt(Y) && IsInt(X))
            {
                if (IsSlowed)
                {
                    while (!CanMove())
                    {
                        Direction.SetRandom();
                    }
                }
                else
                {
                    AiDirection();
                }
            }
            TryMove();
        }

        private bool CanMove()
        {
            int y = Int(Y);
            int x = Int(X);

            Direction.MovePoint(ref y, ref x);
            return IsInFieldAndNotWall(y, x, Field, Height, Width);
        }

        protected int SetShortestWayDirection(int endY, int endX)
        {
            if (!IsInFieldAndNotWall(endY, endX, Field, Height, Width))
            {
                throw new ArgumentException("coordinates must be in field and not wall");
            }
            if ((endY == Int(Y)) && (endX == Int(X)))
            {
                return 0;
            }
            CalculateDistances(Int(Y), Int(X), endY, endX);
            Backtrace(endY, endX);
            return _distances[endY * Width + endX];
        }

        private void CalculateDistances(int fromY, int fromX,
            int toY = int.MinValue, int toX = int.MinValue)
        {
            if (!IsInFieldAndNotWall(fromY, fromX, Field, Height, Width))
            {
                throw new ArgumentException("coordinates must be in field and not wall");
            }
            for (int i = 0; i < Height * Width; i++)
            {
                _distances[i] = int.MinValue;
            }

            _visited.Clear();
            _queue.Clear();

            _queue.Enqueue(new Point { Y = fromY, X = fromX });
            _distances[fromY * Width + fromX] = 0;

            while (_queue.Count != 0)
            {
                var point = _queue.Dequeue();
                int distance = _distances[point.Y * Width + point.X] + 1;
                _visited.Add(point);

                foreach (var direction in Direction.Array)
                {
                    int y = point.Y;
                    int x = point.X;
                    Direction.MovePoint(ref y, ref x, direction);

                    if (IsInFieldAndNotWall(y, x, Field, Height, Width) &&
                        !_visited.Contains(new Point { Y = y, X = x }))
                    {
                        _distances[y * Width + x] = distance;
                        if ((y == toY) && (x == toX))
                        {
                            return;
                        }
                        _queue.Enqueue(new Point { Y = y, X = x });
                    }
                }
            }
        }

        private void Backtrace(int endY, int endX)
        {
            if (!((endY > 0) &&
                (endY < Height - 1) &&

                (endX > 0) &&
                (endX < Width - 1)))
            {
                throw new ArgumentException("coordinates must be in field");
            }
            if ((endY == Int(Y)) && (endX == Int(X)))
            {
                return;
            }
            int y = endY;
            int x = endX;
            while (true)
            {
                foreach (var direction in Direction.Array)
                {
                    int tempY = y;
                    int tempX = x;
                    Direction.MovePoint(ref tempY, ref tempX, direction);

                    if ((tempY == Int(Y)) && (tempX == Int(X)))
                    {
                        Direction.SetReverseFrom(direction);
                        return;
                    }
                    if (IsInFieldAndNotWall(tempY, tempX, Field, Height, Width) &&
                        _distances[tempY * Width + tempX] == _distances[y * Width + x] - 1)
                    {
                        y = tempY;
                        x = tempX;
                        break;
                    }
                }
            }
        }
    }
}