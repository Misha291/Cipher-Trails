using Cipher_Trails;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace MazeGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics; // объект который настраивает графику игры
        private SpriteBatch _spriteBatch; // рисует текстуры (картинки) - мы рисуем каждый кадр 
        private Player _player;
        private Level _level;

        private GameController _gameController;
        private GameView _gameView;

        private Texture2D _playerTexture;
        private Texture2D _wallTexture;
        private Texture2D _exitTexture;

        private Level[] _levels;
        private int _currentLevel = 0;

        private int _tileSize;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _tileSize = 32;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _levels = new Level[3];

            _levels[0] = new Level(10, 10, 32, 7, 7, 8f, new Vector2(100, 100));
            _levels[1] = new Level(12, 12, 32, 9, 9, 8f, new Vector2(100, 100));
            _levels[2] = new Level(14, 14, 32, 11, 11, 8f, new Vector2(100, 100));

            _level = _levels[_currentLevel];

            _player = new Player();
            _player.Position = _level.StartPosition;

            _gameController = new GameController(_player, _level.Map, _level.Win, _tileSize);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _playerTexture = new Texture2D(GraphicsDevice, 32, 32);
            _wallTexture = new Texture2D(GraphicsDevice, 32, 32);
            _exitTexture = new Texture2D (GraphicsDevice, 32, 32);

            Color[] data = new Color[32 * 32]; 
            for (var i = 0; i < data.Length; i++)
            {
                data[i] = Color.White;
            }

            _playerTexture.SetData(data);
            _wallTexture.SetData(data);
            _exitTexture.SetData(data);

            _gameView = new GameView(_player, _level.Map, _spriteBatch, _tileSize, _playerTexture, _wallTexture, _exitTexture);
            LoadLevel(_currentLevel);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            var keyboardState = Keyboard.GetState();

            _gameController.Update(keyboardState);

            if (_level.Win.IsWin)
            {
                if (_currentLevel < _levels.Length - 1)
                {
                    _currentLevel++;
                    LoadLevel(_currentLevel);
                }
                else
                {
                    Window.Title = "Все уровни пройдены !";
                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                        Exit();
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _gameView.Draw();

            base.Draw(gameTime);
        }

        public void LoadLevel(int index)
        {
            _level = _levels[index];
            _player.Position = _level.StartPosition;

            _gameController.UpdateLevelController(_level);
            _gameView.UpdateLevelView(_level);

        }
    }
}


