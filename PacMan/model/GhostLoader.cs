using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace PacMan.model
{
    sealed class GhostLoader
    {
        private readonly List<ConstructorInfo> _constructors;
        private int _ghostNumber;

        private const string VerificationDll = "mscoree.dll";

        public GhostLoader()
        {
            _constructors = new List<ConstructorInfo>();
            LoadGhosts();
            _ghostNumber = 0;
        }

        private static string ByteArrayToString(byte[] byteArray)
        {
            if (byteArray == null)
            {
                throw new ArgumentException("byte array must be not null");
            }
            var sb = new StringBuilder();
            foreach (var b in byteArray)
            {
                sb.Append(b);
            }
            return sb.ToString();
        }

        static class NativeMethods
        {
            [DllImport(VerificationDll, CharSet = CharSet.Unicode)]
            internal static extern byte StrongNameSignatureVerificationEx(string wszFilePath,
                byte fForceVerification,
                ref byte pfWasVerified);
        }

        private void LoadGhosts()
        {
            string[] files = Directory.GetFiles(".", "*.dll");
            foreach (var file in files)
            {
                byte pfWasVerified = 0;
                try
                {
                    if (0 == NativeMethods.StrongNameSignatureVerificationEx(file, 1, ref pfWasVerified))
                    {
                        continue;
                    }
                }
                catch
                {
                    throw new DllNotFoundException("Failed to verify dll's with ghosts. Please," +
                                                   "ensure that the correct " + VerificationDll + " is available.");
                }
                Assembly assembly;
                try
                {
                    assembly = Assembly.LoadFrom(file);
                }
                catch
                {
                    continue;
                }
                if (ByteArrayToString(assembly.GetName().GetPublicKeyToken()) !=
                    ByteArrayToString(Assembly.GetExecutingAssembly().GetName().GetPublicKeyToken()))
                {
                    continue;
                }
                foreach (var type in assembly.DefinedTypes)
                {
                    if (type.IsSubclassOf(typeof(Ghost)))
                    {
                        var ghostConstructor = type.GetConstructor(
                            new[] { typeof(int), typeof(int), typeof(Cell[]), typeof(int), typeof(int) });
                        if (ghostConstructor != null)
                        {
                            _constructors.Add(ghostConstructor);
                        }
                    }
                }
            }
            if (_constructors.Count == 0)
            {
                throw new FileNotFoundException("Failed to load ghosts. Please, ensure that dll's " +
                                                "with ghosts are in the same directory as this exe-file and " +
                                                "you have access permission for them.");
            }
        }

        internal Ghost ConstructNextGhost(int y, int x, Cell[] field, int height, int width)
        {
            if (!Monster.IsInFieldAndNotWall(y, x, field, height, width))
            {
                throw new ArgumentException("coordinates must be in field and not wall");
            }
            try
            {
                var ghost = ((Ghost)_constructors[_ghostNumber].Invoke(new object[] { y, x, field, height, width }));
                _ghostNumber++;

                _ghostNumber %= _constructors.Count;
                return ghost;
            }
            catch
            {
                throw new FormatException("An error occured while creating ghosts. Please, ensure " +
                                          "that you use the correct dll's with ghosts.");
            }
        }
    }
}
