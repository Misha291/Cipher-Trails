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

        public void Move(Vector2 direction)
        {
            Position = Position + direction;
        }
    }
}
