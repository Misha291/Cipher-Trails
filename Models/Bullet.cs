using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cipher_Trails.Models
{
    public class Bullet
    {
        public Vector2 PositionBullet { get; private set; }
        public Vector2 DirectionBullet { get; private set; }
        public float SpeedBullet { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Bullet(Vector2 positionBullet, Vector2 directionBullet, float speedBullet, int width, int height)
        {
            PositionBullet = positionBullet;
            DirectionBullet = directionBullet;
            SpeedBullet = speedBullet;
            Width = width;
            Height = height;
        }

        public void UpdatePositionBullet(float deltaTime)
        {
            PositionBullet += deltaTime * SpeedBullet * DirectionBullet;
        }
    }
}
