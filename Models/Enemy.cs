using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cipher_Trails.Models
{
    public class Enemy
    {
        public const float DefaultSpeed = 300f;
        public int Width { get; }
        public int Height { get; }
        public Vector2 Position { get; set; }
        public float Speed { get; set; }

        public Enemy(Vector2 position, float speed, int width, int height)
        {
            Position = position;
            Speed = speed;
            Width = width;
            Height = height;
        }
    }
}
