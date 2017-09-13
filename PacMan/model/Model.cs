using System;
using System.Collections.Generic;

namespace PacMan.model
{
    sealed class Model
    {
        public Cell[] Field { get; private set; }
        public int Height;
        public int Width;

        public readonly ITimer Timer;
        public IView View { private get; set; }

        private PacMan _pacMan;
        public PacMan PacMan() { return _pacMan; }
        public readonly List<Ghost> Ghosts;

        public int Score { get; private set; }
        public int Lifes { get; private set; }

        public int LevelNumber { get; private set; }
        public GameState GameState { get; private set; }

        public readonly HighScores HighScores;
        private readonly LevelLoader _levelLoader;

        private int _numberOfPills;
        private int _megaPillTime;
        
        internal const int StartNumberOfLifes = 3;

        private const int TickPeriod = 50;
        private const int MegaPillPeriod = 5000;

        private const int PillScore = 10;
        private const int MegaPillScore = 50;
        private const int FruitScore = 100;
        private const int GhostScore = 200;

        public Model(ITimer timer)
        {
            Ghosts = new List<Ghost>();
            _levelLoader = new LevelLoader();

            HighScores = new HighScores();
            SetNewGame();

            Timer = timer;
            Timer.SetInterval(new TimeSpan(0, 0, 0, 0, TickPeriod));

            Timer.Tick += Tick;
            Timer.Start();
        }

        private void SetNewGame()
        {
            LevelNumber = 1;
            Field = _levelLoader.LoadLevel(LevelNumber, out Height, out Width,
                Ghosts, out _numberOfPills, out _pacMan);

            _megaPillTime = 0;
            GameState = GameState.Playing;

            Score = 0;
            Lifes = StartNumberOfLifes;
            HighScores.IsNewRecord = false;
        }

        public void NewGame()
        {
            SetNewGame();
            View.NotifyNewLevel();
        }

        private void Tick(object sender, EventArgs e)
        {
            if (GameState != GameState.Playing)
            {
                return;
            }

            MovePacMan();
            MoveGhosts();
            CheckEating();

            if (_megaPillTime > 0)
            {
                _megaPillTime -= TickPeriod;
                if (_megaPillTime <= 0)
                {
                    foreach (var ghost in Ghosts)
                    {
                        ghost.IsSlowed = false;
                        ghost.Step = Ghost.GhostStep;
                    }
                }
            }
            View.NotifyModelChanges();
        }

        private void MovePacMan()
        {
            _pacMan.Move();

            if (Field[Monster.Int(_pacMan.Y) * Width + Monster.Int(_pacMan.X)] == Cell.Pill)
            {
                Score += PillScore;
                _numberOfPills--;
                if (_numberOfPills == 0)
                {
                    NextLevelOrVictory();
                }
            }
            else if (Field[Monster.Int(_pacMan.Y) * Width + Monster.Int(_pacMan.X)] == Cell.MegaPill)
            {
                Score += MegaPillScore;
                _megaPillTime = MegaPillPeriod;

                foreach (var ghost in Ghosts)
                {
                    ghost.IsSlowed = true;
                    ghost.Step = Ghost.SlowedStep;
                }
            }
            else if (Field[Monster.Int(_pacMan.Y) * Width + Monster.Int(_pacMan.X)] == Cell.Fruit)
            {
                Score += FruitScore;
            }
            Field[Monster.Int(_pacMan.Y) * Width + Monster.Int(_pacMan.X)] = Cell.Empty;
        }

        private void MoveGhosts()
        {
            foreach (var ghost in Ghosts)
            {
                ghost.Move();
            }
        }

        private void CheckEating()
        {
            foreach (var ghost in Ghosts)
            {
                if ((Math.Abs(ghost.Y - _pacMan.Y) < 0.5) &&
                    (Math.Abs(ghost.X - _pacMan.X) < 0.5))
                {
                    if (ghost.IsSlowed)
                    {
                        EatGhost(ghost);
                    }
                    else
                    {
                        EatPacMan();
                        return;
                    }
                }
            }
        }

        private void EatGhost(Ghost ghost)
        {
            Score += GhostScore;
            ghost.SetStartPosition();

            ghost.IsSlowed = false;
            ghost.Step = Ghost.GhostStep;
        }

        private void EatPacMan()
        {
            Lifes--;
            if (Lifes != 0)
            {
                _pacMan.SetStartPosition();
                _pacMan.Direction.Current = Direction.ByName["Up"];
                foreach (var ghost in Ghosts)
                {
                    ghost.SetStartPosition();
                }
            }
            else
            {
                GameState = GameState.Defeat;
                HighScores.CheckNewRecord(Score);
            }
        }

        private void NextLevelOrVictory()
        {
            if (LevelNumber != LevelLoader.TotalNumberOfLevels)
            {
                LevelNumber++;
                Field = _levelLoader.LoadLevel(LevelNumber, out Height, out Width,
                    Ghosts, out _numberOfPills, out _pacMan);
                _megaPillTime = 0;

                View.NotifyNewLevel();
            }
            else
            {
                GameState = GameState.Victory;
                HighScores.CheckNewRecord(Score);
            }
        }
    }
}
