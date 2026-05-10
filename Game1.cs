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
        private Map _map;
        private Win _win;

        private GameController _gameController;
        private GameView _gameView;

        private Texture2D _playerTexture;
        private Texture2D _wallTexture;
        private Texture2D _exitTexture;

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

            _player = new Player();
            _player.Position = new Vector2(100, 100);

            _map = new Map(10, 10);
            _win = new Win(32, 8, 8, 7f);

            _gameController = new GameController(_player, _map, _win, _tileSize);
            _gameView = new GameView(_player, _map, _spriteBatch, _tileSize, _playerTexture, _wallTexture, _exitTexture);
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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            var keyboardState = Keyboard.GetState();

            _gameController.Update(keyboardState);

            if (_win.IsWin)
            {
                Window.Title = "ПОБЕДА !";
                if (keyboardState.IsKeyDown(Keys.Escape))
                {
                    Exit();
                }
                return;
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
    }
}


