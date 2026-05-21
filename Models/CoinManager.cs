using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Cipher_Trails.Models
{
    public class CoinManager
    {
        private List<Coin> _coinsList;

        private int _tileSize;
        private int _mapWidth;
        private int _mapHeight;

        private Random _random;

        private float _collectionRadius;

        public List<Coin> CoinsList { get { return _coinsList; } }
        public CoinManager(int mapWidth, int mapHeight, int tileSize)
        {
            _coinsList = new List<Coin>();

            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
            _tileSize = tileSize;

            _collectionRadius = _tileSize * 0.7f;

            _random = new Random();
        }

        public void GenerateCoins(int count)
        {
            int minCellX = 1;
            int minCellY = 1;

            int maxCellX = _mapWidth - 2;
            int maxCellY = _mapHeight - 2;

            
            for (int i = 0; i < count; i++)
            {
                int cellX = _random.Next(minCellX, maxCellX);
                int cellY = _random.Next(minCellY, maxCellY);

                var posX = cellX * _tileSize + _tileSize / 2;
                var posY = cellY * _tileSize + _tileSize / 2;

                Coin coin = new Coin(new Vector2(posX, posY));
                _coinsList.Add(coin);
            }
        }

        public bool AllCollected()
        {
            foreach (Coin coin in _coinsList)
            {
                if (coin.IsCollected == false)
                {
                    return false;
                }
            }
            return true;
        }

        public void CheckCollisionsCoins(Vector2 positionPlayer)
        {
            foreach (Coin coin in _coinsList)
            {
                float distance = Vector2.Distance(positionPlayer, coin.Position);
                if (distance < _collectionRadius)
                {
                    coin.Collected();
                }

            }
        }
    }
}
