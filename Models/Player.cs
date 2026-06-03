using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cipher_Trails.Models
{
    public class Player
    {
        public Vector2 Position;
        public const float DefaultSpeed = 800f;
        private float _speed;
        
        public float Speed { get { return _speed; } set { _speed = value; } }
        public Player(Vector2 startPosition, float defaultSpeed)
        {
            Position = startPosition;
            _speed = defaultSpeed;
        }
        public void Move(Vector2 direction)
        {
            Position = Position + direction;
        }
    }
}
