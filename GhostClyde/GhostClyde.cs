using System.Reflection;
using System.Runtime.InteropServices;
using PacMan.model;

[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.0.0.0")]

namespace GhostClyde
{
    sealed class GhostClyde : Ghost
    {
        private bool _goToPacMan;

        public GhostClyde(int y, int x, Cell[] field, int height, int width)
            : base(y, x, field, height, width)
        {
            _goToPacMan = true;
        }

        protected override void AiDirection()
        {
            int wayLength = SetShortestWayDirection(Int(PacMan.Y), Int(PacMan.X));
            if ((wayLength < 9) || !_goToPacMan)
            {
                SetShortestWayDirection(StartY, StartX);
                _goToPacMan = ((Int(Y) == StartY) && (Int(X) == StartX));
            }
        }

        public override string ToString()
        {
            return "GhostClyde";
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
        private GhostClyde _ghost;
        internal void StubMethod()
        {
            _ghost = new GhostClyde(0, 0, null, 0, 0);
            _ghost.Stub();
        }
    }
}
