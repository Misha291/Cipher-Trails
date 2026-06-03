using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cipher_Trails.Models
{
    public class Map
    {
        public int[,] map;
        public int height;
        public int width;

        private Random _random = new Random();

        public Map(int x, int y)
        {

            width = x;
            height = y;
            map = new int[y, x];

            for (int dx = 0; dx < x; dx++)
            {
                for (int dy = 0; dy < y; dy++)
                {
                    if (dx == 0 || dy == y - 1 || dy == 0 || dx == x - 1)
                    {
                        map[dy, dx] = 1;
                    }
                }
            }
        }

        public void GenerateMapLevel(float wallProbability, int exitX, int exitY, int startX, int startY)
        {
            for (int dx = 1; dx <= width - 2; dx++)
            {
                for (int dy = 1; dy <= height - 2; dy++)
                {
                    var randNumber = _random.NextDouble();
                    if (randNumber <= wallProbability)
                    {
                        map[dy, dx] = 1;
                    }
                    else
                    {
                        map[dy, dx] = 0;
                    }
                }
            }
            map[1, 1] = 0;
            map[height - 2, width - 2] = 0;
            map[exitY, exitX] = 0;
            map[startY, startX] = 0;
        }
    }
}
