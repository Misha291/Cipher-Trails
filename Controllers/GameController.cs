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
        private BulletManager _bulletManager;
        private EnemyManager _enemyManager;
        private int _tileSize;

        private Vector2 _lastDirection = new Vector2(1, 0);
        private bool _previousSpaceState = false;

        public GameController(
            Player player, 
            Map map, 
            Win win, 
            int tileSize, 
            CoinManager coinManager, 
            BulletManager bulletManager, 
            EnemyManager enemyManager
            )
        {
            _player = player;
            _map = map;
            _win = win;
            _coinManager = coinManager;
            _tileSize = tileSize;
            _bulletManager = bulletManager;
            _enemyManager = enemyManager;
        }

        public void Update(KeyboardState keyboardState, GameTime gametime)
        {
            if (_win.IsPlayerOnExit(_player.Position) && _coinManager.AllCollected())
            {
                return;
            }

            float deltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;

            Vector2 direction = new Vector2(0, 0);
            if (keyboardState.IsKeyDown(Keys.D)) direction.X += 1;
            if (keyboardState.IsKeyDown(Keys.A)) direction.X -= 1;
            if (keyboardState.IsKeyDown(Keys.W)) direction.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.S)) direction.Y += 1;

            if (direction != Vector2.Zero) direction.Normalize();

            Vector2 move = direction * (_player.Speed * deltaTime);

            CanMoveTo(move);

            Vector2 playerCenter = _player.Position + new Vector2(_tileSize / 2, _tileSize / 2);

            if (direction != Vector2.Zero) _lastDirection = direction;

            if (keyboardState.IsKeyDown(Keys.Space) && _previousSpaceState == false)
            {
                _bulletManager.BulletAdd(playerCenter, _lastDirection, 800f, 5, 5);
            }
            _previousSpaceState = keyboardState.IsKeyDown(Keys.Space);

            _bulletManager.Update(deltaTime, _map.width * _tileSize, _map.height * _tileSize);

            _enemyManager.Update(deltaTime, playerCenter, _map);
            
            _coinManager.CheckCollisionsCoins(playerCenter);
        }

        public void CanMoveTo(Vector2 direction)
        {
            var newPosition = _player.Position + direction;
            var checkX = newPosition.X;
            var checkY = newPosition.Y;

            if (!IsCellFree(checkX, checkY)) return;
            if (!IsCellFree(checkX + _tileSize, checkY)) return;
            if (!IsCellFree(checkX, checkY + _tileSize)) return;
            if (!IsCellFree(checkX + _tileSize, checkY + _tileSize)) return;
            else _player.Move(direction);
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

        public void UpdateLevelController(Level level)
        {
            Vector2 playerCenter = _player.Position + new Vector2(_tileSize / 2, _tileSize / 2);

            _map = level.Map;
            _win = level.Win;
            _coinManager = level.CoinManager;

            _bulletManager.ClearBullets();

            _enemyManager.ClearEnemies();
            _enemyManager.SpawnRandomEnemies(5, _map, _tileSize, playerCenter, 150f, 64, 64);
        }

        public bool IsLevelComplete()
        {
            return (_win.IsPlayerOnExit(_player.Position) && _coinManager.AllCollected());
        }
    }
}


