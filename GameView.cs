using MazeGame;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cipher_Trails
{
    public class GameView
    {
        private Player _player;
        private Map _map;
        private SpriteBatch _spriteBatch;

        private Texture2D _playerTexture;
        private Texture2D _wallTexture;
        private Texture2D _exitTexture;

        private int _tileSize;

        public GameView(Player player, Map map, SpriteBatch spriteBatch, int tileSize, Texture2D playerTexture, Texture2D wallTexture, Texture2D exitTexture)
        {
            _player = player;  
            _map = map;
            _spriteBatch = spriteBatch;
            _tileSize = tileSize;
            _playerTexture = playerTexture;
            _wallTexture = wallTexture;
            _exitTexture = exitTexture;
        }

        public void Draw()
        {
            _spriteBatch.Begin();

            for (int dy = 0; dy < _map.height; dy++)
            {
                for (int dx = 0; dx < _map.width; dx++)
                {
                    if (_map.map[dy, dx] == 1)
                    {
                        _spriteBatch.Draw(_wallTexture, new Vector2(dx * _tileSize, dy * _tileSize), Color.Gray);
                    }
                }
            }

            _spriteBatch.Draw(_playerTexture, _player.Position, Color.Red);
            _spriteBatch.Draw(_exitTexture, new Vector2(_tileSize * 8, _tileSize * 8), Color.Green);
            _spriteBatch.End(); //пакетная отрисовка
        }
    }
}
