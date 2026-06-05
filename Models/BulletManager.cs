using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cipher_Trails.Models
{
    public class BulletManager
    {
        private List<Bullet> _bullets = new List<Bullet>();
        public List<Bullet> Bullets { get { return _bullets; } }

        public BulletManager()
        {
        }

        public void BulletAdd(Vector2 position, Vector2 direction, float speed, int width, int height)
        {

            Bullet bullet = new Bullet(position, direction, speed, width, height);
            _bullets.Add(bullet);
        }

        public void Update(float deltaTime, int mapWidthPixels, int mapHeightPixels)
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                var currentBullet = _bullets[i];

                currentBullet.UpdatePositionBullet(deltaTime);

                if (currentBullet.PositionBullet.X > mapWidthPixels
                    || currentBullet.PositionBullet.Y > mapHeightPixels
                    || currentBullet.PositionBullet.X < 0
                    || currentBullet.PositionBullet.Y < 0
                    )
                {
                    _bullets.RemoveAt(i);
                }
            }
        }

        public void ClearBullets()
        {
            _bullets.Clear();
        }
    }
}
