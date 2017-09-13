using System.Reflection;
using System.Runtime.InteropServices;
using PacMan.model;

[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.0.0.0")]

namespace GhostBlinky
{
    sealed class GhostBlinky : Ghost
    {
        public GhostBlinky(int y, int x, Cell[] field, int height, int width)
            : base(y, x, field, height, width)
        {
        }

        protected override void AiDirection()
        {
            SetShortestWayDirection(Int(PacMan.Y), Int(PacMan.X));
        }

        public override string ToString()
        {
            return "GhostBlinky";
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
        private GhostBlinky _ghost;
        internal void StubMethod()
        {
            _ghost = new GhostBlinky(0, 0, null, 0, 0);
            _ghost.Stub();
        }
    }
}
