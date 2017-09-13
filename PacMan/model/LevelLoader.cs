using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PacMan.Properties;

namespace PacMan.model
{
    sealed class LevelLoader
    {
        private readonly GhostLoader _ghostLoader;
        internal const int TotalNumberOfLevels = 3;

        public LevelLoader()
        {
            _ghostLoader = new GhostLoader();
        }

        public Cell[] LoadLevel(int levelNumber, out int height, out int width,
            List<Ghost> ghosts, out int numberOfPills, out PacMan pacMan)
        {
            if ((levelNumber < 1) || (levelNumber > TotalNumberOfLevels))
            {
                throw new ArgumentException("level number must be from 1 to " + TotalNumberOfLevels);
            }
            if (ghosts == null)
            {
                throw new ArgumentException("ghosts must be not null");
            }
            string level = Resources.ResourceManager.GetString("level" + levelNumber);
            if (level == null)
            {
                throw new FileNotFoundException();
            }
            width = level.IndexOf("\r\n", StringComparison.Ordinal);
            height = (int)Math.Round((double)level.Length / (width + 2));

            var field = new Cell[height * width];

            ghosts.Clear();
            numberOfPills = 0;
            pacMan = null;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    field[i * width + j] = new Cell();
                    switch (level.ElementAt(i * (width + 2) + j))
                    {
                        case ' ':
                            {
                                field[i * width + j] = Cell.Empty;
                                break;
                            }
                        case '#':
                            {
                                field[i * width + j] = Cell.Wall;
                                break;
                            }
                        case '.':
                            {
                                field[i * width + j] = Cell.Pill;
                                numberOfPills++;
                                break;
                            }
                        case '*':
                            {
                                field[i * width + j] = Cell.MegaPill;
                                break;
                            }
                        case '%':
                            {
                                field[i * width + j] = Cell.Fruit;
                                break;
                            }
                        case '\\':
                            {
                                pacMan = new PacMan(i, j, field, height, width);
                                break;
                            }
                        case '=':
                            {
                                ghosts.Add(_ghostLoader.ConstructNextGhost(i, j, field, height, width));
                                break;
                            }
                        default:
                            throw new FileFormatException();
                    }
                }
            }
            foreach (var ghost in ghosts)
            {
                ghost.PacMan = pacMan;
            }
            return field;
        }
    }
}
