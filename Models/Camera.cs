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
        public void Follow(Vector2 playerPosition, int screenWidth, int screenHeight, int mapWidth, int mapHeight)
        {
            float cameraX = playerPosition.X - (screenWidth / 2f);
            float cameraY = playerPosition.Y - (screenHeight / 2f);

            cameraX = MathHelper.Clamp(cameraX, 0, mapWidth - screenWidth);
            cameraY = MathHelper.Clamp(cameraY, 0, mapHeight - screenHeight);

            _cameraPosition = new Vector2(cameraX, cameraY);
        }
    }
}
