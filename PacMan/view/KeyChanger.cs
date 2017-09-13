using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using PacMan.model;
using Point = PacMan.model.Point;

namespace PacMan.view
{
    sealed class KeyChanger : Window
    {
        private readonly Dictionary<Key, Point> _keys;
        private Label[] _labels;
        private Button _pressedButton;

        public static readonly Point Pause = new Point { Y = 0, X = 0 };

        public KeyChanger(Dictionary<Key, Point> keys)
        {
            if (keys == null)
            {
                throw new ArgumentException("keys must be not null");
            }
            _keys = keys;
            Title = "Change keys";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Width = 250;
            Height = 250;
            ResizeMode = ResizeMode.NoResize;

            var grid = new Grid();
            ((IAddChild)this).AddChild(grid);

            for (int i = 0; i <= keys.Count + 1; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < 3; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            var label = new Label { Content = "Action:", HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetRow(label, 0);
            Grid.SetColumn(label, 0);
            grid.Children.Add(label);

            label = new Label { Content = "Current key:", HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetRow(label, 0);
            Grid.SetColumn(label, 1);
            grid.Children.Add(label);

            FillGrid(grid);
            KeyDown += OnKeyDown;
        }

        private void FillGrid(Grid grid)
        {
            if (grid == null)
            {
                throw new ArgumentException("grid must be not null");
            }
            int j = 1;
            _labels = new Label[_keys.Count];

            foreach (var key in _keys)
            {
                var label = new Label
                {
                    Content = (key.Value.Equals(Pause)) ? "Pause" : Direction.GetName[key.Value],
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                Grid.SetRow(label, j);
                Grid.SetColumn(label, 0);
                grid.Children.Add(label);

                label = new Label { Content = key.Key, HorizontalAlignment = HorizontalAlignment.Center };
                _labels[j - 1] = label;

                Grid.SetRow(label, j);
                Grid.SetColumn(label, 1);
                grid.Children.Add(label);

                var button = new Button
                {
                    Content = "Change",
                    DataContext = key.Key,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                button.Click += OnClick;
                button.Focusable = false;

                Grid.SetRow(button, j);
                Grid.SetColumn(button, 2);
                grid.Children.Add(button);

                j++;
            }
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            if (sender == null)
            {
                throw new ArgumentException("sender must be not null");
            }
            Button button = sender as Button;
            if (button == null)
            {
                throw new ArgumentException("sender must be a button");
            }
            if (_pressedButton != null)
            {
                return;
            }
            _pressedButton = button;
            _pressedButton.Content = "Press a key...";
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentException("key must be not null");
            }
            if (_pressedButton == null)
            {
                if (e.Key == Key.F2)
                {
                    Close();
                }
                return;
            }
            if (!_keys.ContainsKey(e.Key) && (e.Key != Key.F1) && (e.Key != Key.F2) &&
                (e.Key != Key.F3) && (e.Key != Key.Escape))
            {
                var oldKey = (Key)_pressedButton.DataContext;
                var value = _keys[oldKey];

                _keys.Remove(oldKey);
                _keys[e.Key] = value;

                foreach (var label in _labels)
                {
                    if (label.Content.ToString() == oldKey.ToString())
                    {
                        label.Content = e.Key;
                    }
                }
                _pressedButton.DataContext = e.Key;
            }
            _pressedButton.Content = "Change";
            _pressedButton = null;
        }
    }
}
