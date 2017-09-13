using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PacMan.model;

namespace PacMan.view
{
    sealed class Field
    {
        public Grid Grid { get; private set; }
        private readonly Model _model;

        private Dictionary<string, BitmapImage> _images;
        private Dictionary<Cell, BitmapImage> _backgroundImages;
        private Image[] _field;

        private List<Monster> _monsters;
        private readonly Dictionary<Monster, Image> _monstersImages;

        public double CellHeight { private get; set; }
        public double CellWidth { private get; set; }

        private Grid _gridForFirstLevel;
        private Image[] _fieldForFirstLevel;
        private Canvas _canvasForFirstLevel;

        private const string GhostSlowed = "GhostSlowed";
        private const string PacMan = "PacMan";

        private const int StartCellHeight = 26;
        private const int StartCellWidth = 26;

        public Field(Model model)
        {
            if (model == null)
            {
                throw new ArgumentException("model must be not null");
            }

            _model = model;
            LoadImages();
            _monstersImages = new Dictionary<Monster, Image>();

            _gridForFirstLevel = null;
            _canvasForFirstLevel = null;

            NewField();
        }

        public void NewField()
        {
            if ((_model.LevelNumber == 1) && (_gridForFirstLevel != null))
            {
                Grid = _gridForFirstLevel;
                _field = _fieldForFirstLevel;
            }
            else
            {
                Grid = new Grid { MinHeight = 1, MinWidth = 1 };
                _field = new Image[_model.Height * _model.Width];

                for (int i = 0; i < _model.Height; i++)
                {
                    Grid.RowDefinitions.Add(new RowDefinition());
                }
                for (int i = 0; i < _model.Width; i++)
                {
                    Grid.ColumnDefinitions.Add(new ColumnDefinition());
                }
            }

            for (int i = 0; i < _model.Height; i++)
            {
                for (int j = 0; j < _model.Width; j++)
                {
                    var bmImage = _backgroundImages[_model.Field[i * _model.Width + j]];
                    var image = new Image
                    {
                        Stretch = Stretch.Fill,
                        Margin = new Thickness(-0.4, -0.4, -0.4, -0.4)
                    };
                    Grid.SetRow(image, i);
                    Grid.SetColumn(image, j);

                    if ((_model.LevelNumber == 1) && (_gridForFirstLevel != null))
                    {
                        if (!bmImage.Equals(_field[i * _model.Width + j].Source))
                        {
                            _field[i * _model.Width + j].Source = bmImage;
                        }
                    }
                    else
                    {
                        Grid.Children.Add(image);
                        _field[i * _model.Width + j] = image;
                        image.Source = bmImage;
                    }
                }
            }

            if ((_model.LevelNumber == 1) && (_gridForFirstLevel == null))
            {
                _gridForFirstLevel = Grid;
                _fieldForFirstLevel = _field;
            }

            CellHeight = StartCellHeight;
            CellWidth = StartCellWidth;

            Grid.Height = _model.Height * CellHeight;
            Grid.Width = _model.Width * CellWidth;

            NewMonsters();
        }

        private void NewMonsters()
        {
            var canvas = new Canvas();
            Grid.Children.Add(canvas);

            if ((_model.LevelNumber == 1) && (_gridForFirstLevel != null))
            {
                _gridForFirstLevel.Children.Remove(_canvasForFirstLevel);
                _canvasForFirstLevel = canvas;
            }

            _monsters = new List<Monster> { _model.PacMan() };
            _monsters.AddRange(_model.Ghosts);

            foreach (var monster in _monsters)
            {
                string monsterName = monster.ToString();
                if (monster.ToString() == PacMan)
                {
                    monsterName += _model.PacMan().Direction;
                }
                _monstersImages[monster] = new Image
                {
                    Source = _images[monsterName],
                    Stretch = Stretch.Fill,

                    Height = CellHeight,
                    Width = CellWidth,
                    Margin = new Thickness(-0.4, -0.4, -0.4, -0.4)
                };
                Canvas.SetTop(_monstersImages[monster], monster.Y * CellHeight);
                Canvas.SetLeft(_monstersImages[monster], monster.X * CellWidth);
                canvas.Children.Add(_monstersImages[monster]);
            }
        }

        public void UpdateField()
        {
            for (int i = 0; i < _model.Height; i++)
            {
                for (int j = 0; j < _model.Width; j++)
                {
                    var image = _backgroundImages[_model.Field[i * _model.Width + j]];
                    if (!image.Equals(_field[i * _model.Width + j].Source))
                    {
                        _field[i * _model.Width + j].Source = image;
                    }
                }
            }

            _monstersImages[_model.PacMan()].Source = _images[PacMan + _model.PacMan().Direction];
            foreach (var ghost in _model.Ghosts)
            {
                _monstersImages[ghost].Source = _images[ghost.IsSlowed ? GhostSlowed : ghost.ToString()];
            }

            foreach (var monster in _monsters)
            {
                var image = _monstersImages[monster];
                Canvas.SetTop(image, monster.Y * CellHeight);
                Canvas.SetLeft(image, monster.X * CellWidth);

                image.Height = CellHeight;
                image.Width = CellWidth;
            }
        }

        private void LoadImages()
        {
            _images = new Dictionary<string, BitmapImage>();

            foreach (var ghost in _model.Ghosts)
            {
                LoadImage(ghost.ToString());
            }
            LoadImage(GhostSlowed);

            foreach (var direction in Direction.ByName.Keys)
            {
                LoadImage(PacMan + direction);
            }

            _backgroundImages = new Dictionary<Cell, BitmapImage>();
            foreach (var cellContent in Enum.GetValues(typeof(Cell)))
            {
                LoadBackgroundImage((Cell)cellContent);
            }
        }

        private void LoadImage(string imageName)
        {
            if (imageName == null)
            {
                throw new ArgumentException("imageName");
            }
            _images[imageName] = new BitmapImage(new Uri("pack://application:,,,/resources/" + imageName + ".png"));
        }

        private void LoadBackgroundImage(Cell cellContent)
        {
            _backgroundImages[cellContent] =
                new BitmapImage(new Uri("pack://application:,,,/resources/" + cellContent + ".png"));
        }

        public BitmapImage GetPacManRightImage()
        {
            return _images[PacMan + "Right"];
        }
    }
}
