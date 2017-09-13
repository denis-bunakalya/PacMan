using System;
using System.IO;

namespace PacMan.model
{
    sealed class HighScores
    {
        public bool IsNewRecord { get; set; }
        private const string FileName = "HighScores.pmhs";

        public void CheckNewRecord(int score)
        {
            if (score < 0)
            {
                throw  new ArgumentException("score must be not negative");
            }
            string highScores = GetHighScoresOrNull();

            if ((highScores == null) ||
                (score > int.Parse(highScores.Substring(0, highScores.IndexOf("\t", StringComparison.Ordinal)))))
            {
                IsNewRecord = true;
            }
        }

        public static string GetHighScoresOrNull()
        {
            try
            {
                using (var file = new StreamReader(FileName))
                {
                    return file.ReadToEnd();
                }
            }
            catch
            {
                return null;
            }
        }

        public static void SaveNewRecord(int score, string name)
        {
            if (name == null)
            {
                throw new ArgumentException("name must be not null");
            }
            if (score < 0)
            {
                throw new ArgumentException("score must be not negative");
            }
            string oldHighScores = GetHighScoresOrNull();

            using (var file = new StreamWriter(FileName))
            {
                file.WriteLine(score + "\t" + name);
                file.Write(oldHighScores);
            }
        }
    }
}
