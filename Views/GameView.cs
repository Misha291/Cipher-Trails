using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cipher_Trails.Models;
using System.Runtime.Serialization.Formatters;

namespace Cipher_Trails.Views
{
    public class GameView
    {
        private Player _player;
        private Map _map;
        private Vector2 _exitPosition;
        private CoinManager _coinManeger;
        private BulletManager _bulletManeger;

        private SpriteBatch _spriteBatch;

        private Texture2D _playerTexture;
        private Texture2D _wallTexture;
        private Texture2D _exitTexture;
        private Texture2D _coinTexture;
        private Texture2D _bulletTexture;

        private int _tileSize;

        private int _desiredPlayerWidth = 64;
        private int _desiredPlayerHeight = 64;
        private int _desiredWallWidth = 64;
        private int _desiredWallHeight = 64;
        private int _desiredCoinWidth = 64; 
        private int _desiredCoinHeight = 64;
        private int _desiredExitWidth = 64;
        private int _desiredExitHeight = 64;
        private int _desiredBulletWidth = 24;
        private int _desiredBulletHeight = 16;

        private Camera _camera;
        private Background _background;

        public GameView(Player player, Map map, SpriteBatch spriteBatch, int tileSize, Texture2D playerTexture, Texture2D wallTexture, Texture2D exitTexture, Texture2D coinTexture, Texture2D bulletTexture, Camera camera, Background background, BulletManager bulletManager)
        {
            _player = player;  
            _map = map;
            _spriteBatch = spriteBatch;
            _tileSize = tileSize;
            _playerTexture = playerTexture;
            _wallTexture = wallTexture;
            _exitTexture = exitTexture;
            _coinTexture = coinTexture;
            _bulletTexture = bulletTexture;
            _camera = camera;
            _background = background;
            _bulletManeger = bulletManager;
        }

        public void UpdateLevelView(Level level)
        {
            _map = level.Map;
            _exitPosition = level.Win.ExitPosition - new Vector2(_tileSize / 2f, _tileSize / 2f);
            _coinManeger = level.CoinManager;
        }
        
        public void Draw()
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            int mapWidthPixels = _map.width * _tileSize;
            int mapHeightPixels = _map.height * _tileSize;
            Rectangle mapFullPixels = new Rectangle(
                (int)(0 - _camera.CameraPosition.X), 
                (int)(0 - _camera.CameraPosition.Y), 
                mapWidthPixels, 
                mapHeightPixels
                );
            _spriteBatch.Draw(_background.BackgroundTexture, mapFullPixels, Color.White);

            for (int dy = 0; dy < _map.height; dy++)
            {
                for (int dx = 0; dx < _map.width; dx++)
                {
                    if (_map.map[dy, dx] == 1)
                    {
                        Vector2 wallPos = new Vector2(dx * _tileSize, dy * _tileSize);
                        Rectangle wallRectangle = new Rectangle(
                            (int)(wallPos.X - _camera.CameraPosition.X), 
                            (int)(wallPos.Y - _camera.CameraPosition.Y), 
                            _desiredWallWidth, 
                            _desiredWallHeight
                            );
                        _spriteBatch.Draw(_wallTexture, wallRectangle, Color.White); 
                    }
                }
            }

            foreach (Coin coin in _coinManeger.CoinsList)
            {
                if (!coin.IsCollected)
                {
                    Vector2 coinTopLeft = coin.Position - new Vector2(_desiredCoinWidth / 2, _desiredCoinHeight / 2);
                    Rectangle coinRectangle = new Rectangle(
                        (int)(coinTopLeft.X - _camera.CameraPosition.X), 
                        (int)(coinTopLeft.Y - _camera.CameraPosition.Y), 
                        _desiredCoinWidth, 
                        _desiredCoinHeight
                        );
                    _spriteBatch.Draw(_coinTexture, coinRectangle, Color.White);
                }
            }

            Vector2 playerTopLeft = _player.Position - new Vector2(_desiredPlayerWidth / 2, _desiredPlayerHeight / 2);
            Rectangle playerRectangle = new Rectangle(
                (int)(playerTopLeft.X - _camera.CameraPosition.X), 
                (int)(playerTopLeft.Y - _camera.CameraPosition.Y), 
                _desiredPlayerWidth, 
                _desiredPlayerHeight
                );
            _spriteBatch.Draw(_playerTexture, playerRectangle, Color.White);

            Vector2 exitTopLeft = _exitPosition - new Vector2(_desiredExitWidth / 2, _desiredExitHeight / 2);
            Rectangle exitRectangle = new Rectangle(
                (int)(exitTopLeft.X - _camera.CameraPosition.X), 
                (int)(exitTopLeft.Y - _camera.CameraPosition.Y), 
                _desiredExitWidth, 
                _desiredExitHeight
                );
            _spriteBatch.Draw(_exitTexture, exitRectangle, Color.White);


            foreach (var bullet in _bulletManeger.Bullets)
            {
                Vector2 bulletTopLeft = bullet.PositionBullet - new Vector2(_desiredBulletWidth / 2, _desiredBulletHeight / 2);
                Rectangle bulletRectangle = new Rectangle(
                    (int)(bulletTopLeft.X - _camera.CameraPosition.X), 
                    (int)(bulletTopLeft.Y - _camera.CameraPosition.Y), 
                    _desiredBulletWidth, 
                    _desiredBulletHeight
                    );
                _spriteBatch.Draw(_bulletTexture, bulletRectangle, Color.White);
            }
            _spriteBatch.End(); 
        }
    }
}
