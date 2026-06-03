using Cipher_Trails.Controllers;
using Cipher_Trails.Models;
using Cipher_Trails.Views;
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
        private CoinManager _coinManager;

        private GameController _gameController;
        private GameView _gameView;

        private Texture2D _playerTexture;
        private Texture2D _wallTexture;
        private Texture2D _exitTexture;
        private Texture2D _backgroundTexture;
        private Texture2D _coinTexture;

        private LevelManager _levelManager;

        private Camera _camera;

        private Background _background;
        private int _screenWidth;
        private int _screenHeight;

        private int _tileSize;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 2600;
            _graphics.PreferredBackBufferHeight = 1500;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _tileSize = 64;
            _screenWidth = _graphics.GraphicsDevice.Viewport.Width;
            _screenHeight = _graphics.GraphicsDevice.Viewport.Height;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _camera = new Camera();
            
            _levelManager = new LevelManager();
            _level = _levelManager.CurrentLevel();

            _player = new Player(_level.StartPosition, Player.DefaultSpeed);
            _coinManager = _level.CoinManager;

            _gameController = new GameController(_player, _level.Map, _level.Win, _tileSize , _coinManager);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _playerTexture = Content.Load<Texture2D>("player");
            _wallTexture = Content.Load<Texture2D>("wall");
            _exitTexture = Content.Load<Texture2D>("exit");
            _backgroundTexture = Content.Load<Texture2D>("background");
            _coinTexture = Content.Load<Texture2D>("coin");

            _background = new Background(_backgroundTexture, _screenWidth, _screenHeight);

            _gameView = new GameView(_player, _level.Map, _spriteBatch, _tileSize, _playerTexture, _wallTexture, _exitTexture, _coinTexture, _camera, _background);
            LoadLevel(_levelManager.CurrentLevel());
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            var keyboardState = Keyboard.GetState();

            _gameController.Update(keyboardState, gameTime);

            int screenWidthCamera = _graphics.GraphicsDevice.Viewport.Width;
            int screenHeightCamera = _graphics.GraphicsDevice.Viewport.Height;
            _camera.Follow(_player.Position, screenWidthCamera, screenHeightCamera,
               _level.Map.width * _tileSize, _level.Map.height * _tileSize);

            if (_gameController.IsLevelComplete())
            {
                if (!_levelManager.IsLastLevel())
                {
                    _levelManager.NextLevel();
                    LoadLevel(_levelManager.CurrentLevel());
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

        public void LoadLevel(Level level)
        {
            _level = level;
            _player.Position = _level.StartPosition;
            _coinManager = _level.CoinManager;

            _gameController.UpdateLevelController(_level);
            _gameView.UpdateLevelView(_level);

        }
    }
}