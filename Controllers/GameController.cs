using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cipher_Trails.Models;

namespace Cipher_Trails.Controllers
{
    public class GameController
    {
        private Player _player;
        private Map _map;
        private Win _win;
        private CoinManager _coinManager;

        private int _tileSize;

        public GameController(Player player, Map map, Win win, int tileSize, CoinManager coinManager)
        {
            _player = player;
            _map = map;
            _win = win;
            _coinManager = coinManager;
            _tileSize = tileSize;
        }

        public void Update(KeyboardState keyboardState)
        {
            if (_win.IsPlayerOnExit(_player.Position) && _coinManager.AllCollected())
            {
                return;
            }

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

            _coinManager.CheckCollisionsCoins(_player.Position);
        }

        protected void CanMoveTo(Vector2 direction)
        {
            var nextPosition = _player.Position + direction;

            var checkX = nextPosition.X;
            var checkY = nextPosition.Y;

            if (direction.X > 0)
            {
                //вправо
                var xRtopR = checkX + _tileSize;
                var yRtopR = checkY;
                var xRdownR = checkX;
                var yRdownR = checkY + _tileSize;

                if (!TwoCelisFree(xRtopR, yRtopR, xRdownR, yRdownR))
                {
                    return;
                }
                _player.Move(direction);
            }

            if (direction.Y > 0)
            {
                //вниз
                var xRdownD = checkX;
                var yRdownD = checkY + _tileSize;
                var xLdownD = checkX;
                var yLdownD = checkY + _tileSize;

                if (!TwoCelisFree(xRdownD, yRdownD, xLdownD, yLdownD))
                {
                    return;
                }
                _player.Move(direction);
            }

            if (direction.X < 0)
            {
                //влево
                var xLtopL = checkX;
                var yLtopL = checkY;
                var xLdownL = checkX;
                var yLdownL = checkY + _tileSize;

                if (!TwoCelisFree(xLtopL, yLtopL, xLdownL, yLdownL))
                {
                    return;
                }
                _player.Move(direction);
            }

            if (direction.Y < 0)
            {
                //вверх
                var xLtopT = checkX;
                var yLtopT = checkY;
                var xRtopT = checkX + _tileSize;
                var yRtopT = checkY;

                if (!TwoCelisFree(xLtopT, yLtopT, xRtopT, yRtopT))
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

        protected bool TwoCelisFree(float x1, float y1, float x2, float y2) //метод проверки двух крайних точек игрока 
        {
            if (!IsCellFree(x1, y1))
            {
                return false;
            }
            if (!IsCellFree(x2, y2))
            {
                return false;
            }
            return true;
        }

        public void UpdateLevelController(Level level)
        {
            _map = level.Map;
            _win = level.Win;
            _coinManager = level.CoinManager;
        }

        public bool IsLevelComplete()
        {
            return (_win.IsPlayerOnExit(_player.Position) && _coinManager.AllCollected());
        }

    }
}
