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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.D))
            {
                _player.Position = new Vector2( _player.Position.X + 10, _player.Position.Y );
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                _player.Position = new Vector2( _player.Position.X - 10, _player.Position.Y );
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                _player.Position = new Vector2(_player.Position.X, _player.Position.Y - 10);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                _player.Position = new Vector2( _player.Position.X, _player.Position.Y + 10 );
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
