using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using PacMan.model;
using Point = PacMan.model.Point;

namespace PacMan.view
{
    sealed class View : Window, IView
    {
        private readonly Model _model;
        private readonly Field _field;

        private Label _score;
        private Label _lifes;

        private readonly Dictionary<Key, Point> _controlKeys;
        private MenuItem _pauseMenuItem;

        private DockPanel _dockPanel;
        private bool _resizeAllowed;

        public View(Model model)
        {
            if (model == null)
            {
                throw new ArgumentException("model must be not null");
            }
            _model = model;
            _model.View = this;
            _field = new Field(_model);

            Title = "PacMan";
            WindowStyle = WindowStyle.None;
            Icon = _field.GetPacManRightImage();

            MakePanels();
            _resizeAllowed = false;
            SetSizeAndLocation();

            _controlKeys = new Dictionary<Key, Point>();
            InitializeKeys();
            KeyDown += OnKeyDown;
        }

        private void SetSizeAndLocation()
        {
            SizeToContent = SizeToContent.WidthAndHeight;
            Top = (SystemParameters.FullPrimaryScreenHeight - _field.Grid.Height) / 2;
            Left = (SystemParameters.FullPrimaryScreenWidth - _field.Grid.Width) / 2;
        }

        private void MakePanels()
        {
            _dockPanel = new DockPanel { LastChildFill = true };
            ((IAddChild)this).AddChild(_dockPanel);

            var bottomPanel = new DockPanel();
            DockPanel.SetDock(bottomPanel, Dock.Bottom);
            _dockPanel.Children.Add(bottomPanel);

            _score = new Label { Content = "Score: 0", HorizontalAlignment = HorizontalAlignment.Left };
            bottomPanel.Children.Add(_score);

            _lifes = new Label { Content = "Lifes: " + Model.StartNumberOfLifes, 
                HorizontalAlignment = HorizontalAlignment.Right };
            bottomPanel.Children.Add(_lifes);

            var menu = new Menu();
            DockPanel.SetDock(menu, Dock.Top);
            _dockPanel.Children.Add(menu);

            var newGame = new MenuItem { Header = "New Game (F1)" };
            newGame.Click += OnNewGameClick;

            var changeKeys = new MenuItem { Header = "Change keys (F2)" };
            changeKeys.Click += OnChangeKeysClick;

            var highScores = new MenuItem { Header = "High Scores (F3)" };
            highScores.Click += OnHighScoresClick;

            _pauseMenuItem = new MenuItem { Header = "Pause (Space)" };
            _pauseMenuItem.Click += OnPauseClick;

            var exitMenuItem = new MenuItem { Header = "Exit (Esc)" };
            exitMenuItem.Click += OnExitClick;

            var list = new List<MenuItem> { newGame, changeKeys, highScores, _pauseMenuItem, exitMenuItem };
            menu.ItemsSource = list;

            _dockPanel.Children.Add(_field.Grid);
        }

        private static void OnExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void InitializeKeys()
        {
            _controlKeys[Key.Up] = Direction.ByName["Up"];
            _controlKeys[Key.Down] = Direction.ByName["Down"];
            _controlKeys[Key.Left] = Direction.ByName["Left"];
            _controlKeys[Key.Right] = Direction.ByName["Right"];
            _controlKeys[Key.Space] = KeyChanger.Pause;
        }

        public void NotifyModelChanges()
        {
            _field.UpdateField();
            UpdateScoresAndLifes();
            if (_model.GameState != GameState.Playing)
            {
                MessageBox.Show(_model.GameState + "!!!");
                if (_model.HighScores.IsNewRecord)
                {
                    var textBox = new TextBox { MaxLength = 30 };
                    (new EnterNameDialog(textBox) { Owner = this }).ShowDialog();

                    HighScores.SaveNewRecord(_model.Score,
                        string.IsNullOrEmpty(textBox.Text) ? "anonymous" : textBox.Text);
                    MessageBox.Show(HighScores.GetHighScoresOrNull(), "High Scores");
                }
            }
        }

        public void NotifyNewLevel()
        {
            _resizeAllowed = false;
            _dockPanel.Children.Remove(_field.Grid);

            _field.NewField();
            _dockPanel.Children.Add(_field.Grid);

            SetSizeAndLocation();
        }

        private void UpdateScoresAndLifes()
        {
            _score.Content = "Score: " + _model.Score;
            _lifes.Content = "Lifes: " + _model.Lifes;
        }

        private void OnNewGameClick(object sender, RoutedEventArgs e)
        {
            _model.NewGame();
            Play();
        }

        private void OnChangeKeysClick(object sender, RoutedEventArgs e)
        {
            SetPause();
            (new KeyChanger(_controlKeys) { Owner = this }).ShowDialog();

            _pauseMenuItem.Header = "Pause (" +
                _controlKeys.First(pair => pair.Value.Equals(KeyChanger.Pause)).Key + ")";
        }

        private void OnHighScoresClick(object sender, RoutedEventArgs e)
        {
            SetPause();
            string text = HighScores.GetHighScoresOrNull() ?? "No high scores, be the first!";
            MessageBox.Show(text, "High Scores");
        }

        private void OnPauseClick(object sender, RoutedEventArgs e)
        {
            if (_model.Timer.IsOn())
            {
                SetPause();
            }
            else
            {
                Play();
            }
        }

        private void SetPause()
        {
            if (_model.GameState != GameState.Playing)
            {
                return;
            }
            _model.Timer.Stop();
            Opacity = 0.5;
        }

        private void Play()
        {
            if (_model.GameState != GameState.Playing)
            {
                return;
            }
            _model.Timer.Start();
            Opacity = 1;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentException("e");
            }
            if (!_controlKeys.ContainsKey(e.Key))
            {
                if (e.Key == Key.F1)
                {
                    OnNewGameClick(null, null);
                }
                else if (e.Key == Key.F2)
                {
                    OnChangeKeysClick(null, null);
                }
                else if (e.Key == Key.F3)
                {
                    OnHighScoresClick(null, null);
                }
                else if (e.Key == Key.Escape)
                {
                    OnExitClick(null, null);
                }
                return;
            }

            if (_controlKeys[e.Key].Equals(KeyChanger.Pause))
            {
                OnPauseClick(null, null);
                return;
            }
            if (_model.Timer.IsOn() && (_model.GameState == GameState.Playing))
            {
                _model.PacMan().Direction.New = _controlKeys[e.Key];
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            if (_resizeAllowed)
            {
                if (!double.IsNaN(_field.Grid.Height))
                {
                    _field.Grid.Height = double.NaN;
                    _field.Grid.Width = double.NaN;
                }
                _field.CellHeight = _field.Grid.ActualHeight / _model.Height;
                _field.CellWidth = _field.Grid.ActualWidth / _model.Width;
                _field.UpdateField();
            }
            else
            {
                _resizeAllowed = true;
            }
        }
    }
}
