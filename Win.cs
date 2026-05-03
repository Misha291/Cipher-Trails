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
        private bool _flagWin = false;
        private int _tileSize;
        private Vector2 _exitPosition;
        private float _winRadius;
        public bool IsWin { get { return _flagWin; } }
        public Win(int tileSize, int exitPointX, int exitPointY, float winRadius)
        {
            _tileSize = tileSize;
            _winRadius = winRadius;
            _exitPosition = new Vector2(_tileSize * exitPointX + _tileSize / 2f,
                                        _tileSize * exitPointY + _tileSize / 2f);
        }

        public void Check(Vector2 playerPosition) //проверка дошли ли мы до точки победы
        {
            if (_flagWin) //проверка не стоим ли мы изначально в точке победы
            {
                return;
            }

            float playerCenterX = playerPosition.X + _tileSize / 2f;
            float playerCenterY = playerPosition.Y + _tileSize / 2f;
            Vector2 playerCenter = new Vector2(playerCenterX, playerCenterY);

            if ((Math.Abs(playerCenter.X - _exitPosition.X) <= _winRadius)
                && (Math.Abs(playerCenter.Y - _exitPosition.Y) <= _winRadius))
            {
                _flagWin = true;
            }
        }
    }
}
