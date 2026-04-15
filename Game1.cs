using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics; // объект который настраивает графику игры
        private SpriteBatch _spriteBatch; // рисует текстуры (картинки) - мы рисуем каждый кадр 
        private Player _player;
        private Texture2D _playerTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _player = new Player();
            _player.Position = new Vector2(100, 100);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _playerTexture = new Texture2D(GraphicsDevice, 32, 32);

            Color[] data = new Color[32 * 32]; 
            for (var i = 0; i < data.Length; i++)
            {
                data[i] = Color.White;
            }
            _playerTexture.SetData(data);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.D))
            {
                _player.Move(new Vector2(10, 0));
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                _player.Move(new Vector2(-10, 0));
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                _player.Move(new Vector2(0, -10));
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                _player.Move(new Vector2(0, 10));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(_playerTexture, _player.Position, Color.Red);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}


