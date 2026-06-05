using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Cipher_Trails.Models
{
    public class EnemyManager
    {
        private List<Enemy> _enemies = new List<Enemy>();
        private Random _random = new Random();
        public List<Enemy> Enemies { get { return _enemies; } }

        public EnemyManager()
        {
        }
        public void AddEnemy(Vector2 position, float speed, int width, int height)
        {
            Enemy enemy = new Enemy(position, speed, width, height);
            _enemies.Add(enemy);
        }
        public void Update(float deltaTime, Vector2 playerPosition, Map map)
        {
            foreach (var enemy in _enemies)
            {
                var direction = playerPosition - enemy.Position;
                if (direction != Vector2.Zero) 
                    direction.Normalize();

                var move = direction * deltaTime * enemy.Speed;
                enemy.Position += move;
            }
        }

        public void SpawnRandomEnemies(int count, Map map, int tileSize, Vector2 playerPosition, float speed, int width, int height)
        {
            int minCellX = 1;
            int minCellY = 1;

            int maxCellX = map.width - 2;
            int maxCellY = map.height - 2;

            for (var i = 0; i < count; i++)
            {
                int attempts = 0;
                int maxAttempts = 100;
                bool flag = false;
                while(attempts < maxAttempts && flag == false)
                {
                    var CellX = _random.Next(minCellX, maxCellX);
                    var CellY = _random.Next(minCellY, maxCellY);

                    var posX = (CellX * tileSize) + tileSize / 2;
                    var posY = (CellY * tileSize) + tileSize / 2;

                    Vector2 posEnemy = new Vector2(posX, posY);
                    float dist = Vector2.Distance(playerPosition, posEnemy);
                    if (map.map[CellY, CellX] == 0 && dist > tileSize)
                    {
                        AddEnemy(posEnemy, speed, width, height);
                        flag = true;
                    }
                    attempts++;
                }
            }
        }

        public void ClearEnemies()
        {
            _enemies.Clear();
        }
    }
}
