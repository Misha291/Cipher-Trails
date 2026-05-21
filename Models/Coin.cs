using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Cipher_Trails
{
    public class Coin
    {

        private Vector2 _position;
        public Vector2 Position //обычное свойство 
        { 
            get { return _position; } 

            private set { _position = value; } 
        }
        public bool IsCollected { get; private set; } //автосвойство

        public Coin(Vector2 position)
        {
            _position = position;
        }

        public void Collected()
        {
            IsCollected = true;
        }

    }
}
