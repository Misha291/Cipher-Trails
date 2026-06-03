using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Cipher_Trails.Models
{
    public class LevelManager
    {
        private Level[] _levels;
        private int _currentLevel;

        public LevelManager()
        {
            _levels = new Level[3];

            _levels[0] = new Level(110, 110, 64, 104, 104, 80f, new Vector2(128, 128), 0.03f);
            _levels[1] = new Level(120, 120, 64, 114, 114, 80f, new Vector2(128, 128), 0.03f);
            _levels[2] = new Level(130, 130, 64, 124, 124, 80f, new Vector2(128, 128), 0.03f);

            _currentLevel = 0;
        }

        public Level CurrentLevel()
        {
            return _levels[_currentLevel];
        }

        public bool IsLastLevel()
        {
            return (_currentLevel >= _levels.Length - 1);
        }

        public Level NextLevel()
        {
            if (!IsLastLevel())
            {
                _currentLevel++;
                return _levels[_currentLevel];
            }
            return null;
        }
    }
}
