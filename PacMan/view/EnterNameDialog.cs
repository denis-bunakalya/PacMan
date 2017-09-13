using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace PacMan.view
{
    sealed class EnterNameDialog : Window
    {
        public EnterNameDialog(TextBox textBox)
        {
            if (textBox == null)
            {
                throw new ArgumentException("text box must be not null");
            }
            Title = "Enter your name";
            textBox.Width = 200;
            textBox.Height = 25;

            textBox.Focus();
            ((IAddChild)this).AddChild(textBox);

            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.NoResize;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentException("key must be not null");
            }
            if (e.Key == Key.Enter)
            {
                Close();
            }
        }
    }
}
