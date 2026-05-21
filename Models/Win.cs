using MazeGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cipher_Trails
{
    public class Win
    {
        
        private int _tileSize;
        private Vector2 _exitPosition;
        private float _winRadius;
        
        public Vector2 ExitPosition { get { return _exitPosition; } }
        public Win(int tileSize, int exitPointX, int exitPointY, float winRadius)
        {
            _tileSize = tileSize;
            _winRadius = winRadius;
            _exitPosition = new Vector2(_tileSize * exitPointX + _tileSize / 2f,
                                        _tileSize * exitPointY + _tileSize / 2f);
        }

        public bool IsPlayerOnExit(Vector2 playerPosition)
        {
            float playerCenterX = playerPosition.X + _tileSize / 2;
            float playerCenterY = playerPosition.Y + _tileSize / 2;
            var playerCenter = new Vector2(playerCenterX, playerCenterY);

            if (Vector2.Distance(playerCenter, _exitPosition) < _winRadius)
            {
                return true;
            }
            return false;
        }
        
    }
}

