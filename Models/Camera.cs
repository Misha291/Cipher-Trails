using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Cipher_Trails.Models
{
    public class Camera
    {
        private Vector2 _cameraPosition;

        public Vector2 CameraPosition { get { return _cameraPosition; } }
        public void Follow(Vector2 playerPosition, int screenWidth, int screenHeight)
        {
            float cameraX = playerPosition.X - (screenWidth / 2f);
            float cameraY = playerPosition.Y - (screenHeight / 2f);

            _cameraPosition = new Vector2(cameraX, cameraY);
        }
    }
}
