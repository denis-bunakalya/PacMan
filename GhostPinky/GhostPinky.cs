using System.Reflection;
using System.Runtime.InteropServices;
using PacMan.model;

[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.0.0.0")]

namespace GhostPinky
{
    sealed class GhostPinky : Ghost
    {
        public GhostPinky(int y, int x, Cell[] field, int height, int width)
            : base(y, x, field, height, width)
        {
        }

        protected override void AiDirection()
        {
            int y = Int(PacMan.Y);
            int x = Int(PacMan.X);
            for (int i = 0; i < 4; i++)
            {
                Direction.MovePoint(ref y, ref x, PacMan.Direction.Current);
                if (!IsInFieldAndNotWall(y, x, Field, Height, Width))
                {
                    Direction.MovePoint(ref y, ref x, Direction.Reverse(PacMan.Direction.Current));
                    break;
                }
            }
            SetShortestWayDirection(y, x);
        }

        public override string ToString()
        {
            return "GhostPinky";
        }

        private Stub _stub;
        internal void Stub()
        {
            _stub = new Stub();
            _stub.StubMethod();
        }
    }

    sealed class Stub
    {
        private GhostPinky _ghost;
        internal void StubMethod()
        {
            _ghost = new GhostPinky(0, 0, null, 0, 0);
            _ghost.Stub();
        }
    }
}
