using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cipher_Trails.Models
{
    public class Map
    {
        public int[,] map;
        public int height;
        public int width;

        public Map(int x, int y)
        {
            width = x;
            height = y;
            map = new int[y, x];

            //создание стен вокруг 
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
    }
}
