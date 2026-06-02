using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cipher_Trails.Models
{
    public class Background
    {
        private Texture2D _textureBackground;
        private int _screenWidth;
        private int _screenHeight;
        
        public Texture2D BackgroundTexture { get { return _textureBackground; } }
        public Background(Texture2D textureBackground, int screenWidth, int screenHeight)
        {
            _textureBackground = textureBackground;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }

        public Rectangle GetSourceRectangle(Vector2 cameraPosition)
        {
            var rectangle = new Rectangle((int)(-cameraPosition.X), (int)(-cameraPosition.Y), _screenWidth, _screenHeight);
            return rectangle;
        }

    } 
}
