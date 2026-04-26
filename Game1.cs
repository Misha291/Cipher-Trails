using Cipher_Trails;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

            Color[] data = new Color[32 * 32]; 
            for (var i = 0; i < data.Length; i++)
            {
                data[i] = Color.White;
            }


            _playerTexture.SetData(data);
            _wallTexture.SetData(data);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            var keyboardState = Keyboard.GetState();
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
                var okTopRight = IsCellFree(checkX + _tileSize, checkY);
                if (okTopRight == false)
                {
                    return;
                }
                
                var okDownRight = IsCellFree(checkX + _tileSize, checkY + _tileSize);
                if (okDownRight == false)
                {
                    return;
                }

                _player.Move(direction);
            }

            if (direction.Y > 0)
            {
                var okDownLeft = IsCellFree(checkX, checkY + _tileSize);
                if (okDownLeft == false)
                {
                    return;
                }

                var okDownRight = IsCellFree(checkX + _tileSize, checkY + _tileSize);
                if (okDownRight == false)
                {
                    return;
                }

                _player.Move(direction);
            }

            if (direction.X < 0)
            {
                var okTopLeft = IsCellFree(checkX, checkY);
                if (okTopLeft == false)
                {
                    return;
                }

                var okDownLeft = IsCellFree(checkX, checkY + _tileSize);
                if (okDownLeft == false)
                {
                    return;
                }

                _player.Move(direction);
            }

            if (direction.Y < 0)
            {
                var okTopLeft = IsCellFree(checkX, checkY);
                if (okTopLeft == false)
                {
                    return;
                }

                var okTopRight = IsCellFree(checkX + _tileSize, checkY);
                if (okTopRight == false)
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

    }
}


