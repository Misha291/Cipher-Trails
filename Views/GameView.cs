using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cipher_Trails.Models;

namespace Cipher_Trails.Views
{
    public class GameView
    {
        private Player _player;
        private Map _map;
        private Vector2 _exitPosition;
        private CoinManager _coinManeger;

        private SpriteBatch _spriteBatch;

        private Texture2D _playerTexture;
        private Texture2D _wallTexture;
        private Texture2D _exitTexture;
        private Texture2D _backgroundTexture;
        private Texture2D _coinTexture;

        private int _tileSize;

        private Camera _camera;

        public GameView(Player player, Map map, SpriteBatch spriteBatch, int tileSize, Texture2D playerTexture, Texture2D wallTexture, Texture2D exitTexture, Texture2D coinTexture, Texture2D backgroundTexture, Camera camera)
        {
            _player = player;  
            _map = map;
            _spriteBatch = spriteBatch;
            _tileSize = tileSize;
            _playerTexture = playerTexture;
            _wallTexture = wallTexture;
            _exitTexture = exitTexture;
            _backgroundTexture = backgroundTexture;
            _coinTexture = coinTexture;
            _camera = camera;
            
        }

        public void UpdateLevelView(Level level)
        {
            _map = level.Map;
            _exitPosition = level.Win.ExitPosition - new Vector2(_tileSize / 2f, _tileSize / 2f);
            _coinManeger = level.CoinManager;
        }



        public void Draw()
        {
            _spriteBatch.Begin();

            Rectangle backgroundSize = new Rectangle((int)(-_camera.CameraPosition.X), (int)(-_camera.CameraPosition.YЁ), _map.width * _tileSize, _map.height * _tileSize);
            _spriteBatch.Draw(_backgroundTexture, backgroundSize, Color.White);

            for (int dy = 0; dy < _map.height; dy++)
            {
                for (int dx = 0; dx < _map.width; dx++)
                {
                    if (_map.map[dy, dx] == 1)
                    {
                        Vector2 wallPos = new Vector2(dx * _tileSize, dy * _tileSize);
                        _spriteBatch.Draw(_wallTexture, wallPos - _camera.CameraPosition, Color.Gray); ;
                    }
                }
            }

            foreach (Coin coin in _coinManeger.CoinsList)
            {
                if (!coin.IsCollected)
                {
                    _spriteBatch.Draw(_coinTexture, coin.Position - _camera.CameraPosition, Color.White);
                }
            }

            
            _spriteBatch.Draw(_playerTexture, _player.Position - _camera.CameraPosition, Color.White);
            _spriteBatch.Draw(_exitTexture, _exitPosition - _camera.CameraPosition, Color.White);
            _spriteBatch.End(); 
        }
    }
}
