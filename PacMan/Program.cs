using System;
using System.Windows;
using PacMan.model;
using PacMan.view;

namespace PacMan
{
    sealed class Program : Application
    {
        [STAThread()]
        static void Main()
        {
            try
            {
                var timer = new Timer();
                var model = new Model(timer);
                (new Program()).Run(new View(model));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
