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
        private Texture2D _playerTexture;

        private Texture2D _wallTexture;

        private Texture2D _exitTexture;

        private bool _flagWin = false;

        private Map _map;

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

            if (_flagWin)
            {
                if (keyboardState.IsKeyDown(Keys.Escape))
                {
                    Exit();
                }
                return;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                CanMoveTo(new Vector2(10, 0));
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                CanMoveTo(new Vector2(-10, 0));
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                CanMoveTo(new Vector2(0, -10));
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                CanMoveTo(new Vector2(0, 10));
            }

            CheckWin();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            for (int dy = 0; dy < _map.height; dy++)
            {
                for (int dx = 0; dx < _map.width; dx++)
                {
                    if (_map.map[dy, dx] == 1)
                    {
                        _spriteBatch.Draw(_wallTexture, new Vector2( dx * _tileSize, dy * _tileSize ), Color.Gray);
                    }
                }
            }

            _spriteBatch.Draw(_playerTexture, _player.Position, Color.Red);
            _spriteBatch.Draw(_exitTexture, new Vector2(_tileSize * 8, _tileSize * 8), Color.Green);
            _spriteBatch.End(); //пакетная отрисовка

            base.Draw(gameTime);
        }

        protected void CanMoveTo(Vector2 direction)
        {
            var nextPosition = _player.Position + direction;

            var checkX = nextPosition.X;
            var checkY = nextPosition.Y;

            if (direction.X > 0)
            {
                //вправо
                var xRtopR = checkX + _tileSize;
                var yRtopR = checkY;
                var xRdownR = checkX;
                var yRdownR = checkY + _tileSize;

                if (!TwoCelisFree(xRtopR, yRtopR, xRdownR, yRdownR))
                {
                    return;
                }
                _player.Move(direction);
            }

            if (direction.Y > 0)
            {
                //вниз
                var xRdownD = checkX;
                var yRdownD = checkY + _tileSize;
                var xLdownD = checkX;
                var yLdownD = checkY + _tileSize;

                if (!TwoCelisFree(xRdownD, yRdownD, xLdownD, yLdownD))
                {
                    return;
                }
                _player.Move(direction);
            }

            if (direction.X < 0)
            {
                //влево
                var xLtopL = checkX;
                var yLtopL = checkY;
                var xLdownL = checkX;
                var yLdownL = checkY + _tileSize;

                if (!TwoCelisFree(xLtopL, yLtopL, xLdownL, yLdownL))
                {
                    return;
                }
                _player.Move(direction);
            }

            if (direction.Y < 0)
            {
                //вверх
                var xLtopT = checkX;
                var yLtopT = checkY;
                var xRtopT = checkX + _tileSize;
                var yRtopT = checkY;

                if (!TwoCelisFree(xLtopT, yLtopT, xRtopT, yRtopT))
                {
                    return;
                }
                _player.Move(direction);
            }
        }

        protected bool IsCellFree(float checkX, float checkY) //метод проверки стена или нет 
        {
            int nextX = (int)(checkX / _tileSize);
            int nextY = (int)(checkY / _tileSize);
            if (nextX >= 0 && nextY >= 0
                && nextX <= _map.width - 1 && nextY <= _map.height - 1
                && _map.map[nextY, nextX] == 0)
            {
                return true;
            }
            return false;
        }

        protected bool TwoCelisFree(float x1, float y1, float x2, float y2)
        {
            if (!IsCellFree(x1, y1))
            {
                return false;
            }
            if (!IsCellFree(x2, y2))
            {
                return false;
            }
            return true;
        }

        public void CheckWin()
        {
            if (_flagWin)
            {
                return;
            }
            float _exitX = (_tileSize * 8) + _tileSize / 2f;
            float _exitY = (_tileSize * 8) + _tileSize / 2f;

            float _playerCenterX = _player.Position.X + _tileSize / 2f;
            float _playerCenterY = _player.Position.Y + _tileSize / 2f;

            Vector2 _exitPosition = new Vector2(_exitX, _exitY);
            Vector2 _playerCenter = new Vector2(_playerCenterX, _playerCenterY);

            if ((Math.Abs(_playerCenter.X - _exitPosition.X) <= 7) 
                && (Math.Abs(_playerCenter.Y - _exitPosition.Y) <= 7))
            {
                _flagWin = true;
                Window.Title = "ПОБЕДА, ВЫ ВЫШЛИ ИЗ ЛАБИРИНТА !";
            }
        }
    }
}


