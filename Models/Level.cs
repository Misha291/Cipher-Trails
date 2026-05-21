using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace Cipher_Trails.Models
{
    public class Level
    {
        private Map _map;
        private Win _win;
        private Vector2 _startPosition;
        private CoinManager _coinManager;

        private int _tileSize;

        public Map Map { get { return _map; } }
        public Win Win { get { return _win; } }
        public Vector2 StartPosition { get { return _startPosition; } }
        public CoinManager CoinManager { get { return _coinManager; } }

        public Level(int mapWidth, int mapHeight, int tileSize, int exitPointX, int exitPointY, float winRadius, Vector2 startPosition)
        {
            _map = new Map(mapWidth, mapHeight);
            _win = new Win(tileSize, exitPointX, exitPointY, winRadius);
            _startPosition = startPosition;
            _coinManager = new CoinManager(mapWidth, mapHeight, tileSize);

            _coinManager.GenerateCoins(3);

        }
    }
}
