using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace isaac
{
    class Room
    {
        private int[,] data, col;
        private int width, height;
        private Texture2D tileset;
        public Room(string filename, int w, int h, Texture2D t)
        {
            width = w; height = h; tileset = t;
            data = new int[10, 10] {{ 24,  1,  2,  3,  4,  5,  0,  1,  2, 39},
                                    { 17,999,999,999,999,999,999,999,999, 22},
                                    { 16,999,999,999,999,999,999,999,999, 23},
                                    { 15,999,999,999,999,999,999,999,999, 18},
                                    { 14,999,999,999,999,999,999,999,999, 19},
                                    { 13,999,999,999,999,999,999,999,999, 20},
                                    { 12,999,999,999,999,999,999,999,999, 21},
                                    { 17,999,999,999,999,999,999,999,999, 22},
                                    { 16,999,999,999,999,999,999,999,999, 23},
                                    { 33,  8,  7,  6, 11, 10,  9,  8,  7, 42}};
            col = new int[10, 10] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                                   {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                                   {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                                   {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                                   {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                                   {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                                   {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                                   {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                                   {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                                   {1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};
        }
        public void Update(List<Player> players)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    foreach (Player player in players)
                    {
                        Rectangle cur = new Rectangle(x * 64, y * 64, 64, 64);
                        if (col[y, x] == 1)
                        {
                            if (cur.Contains(player.pos.Right, player.pos.Y + player.pos.Height / 2)) player.pos.X -= player.speed;
                            if (cur.Contains(player.pos.Left, player.pos.Y + player.pos.Height / 2)) player.pos.X += player.speed;
                            if (cur.Contains(player.pos.X + player.pos.Width / 2, player.pos.Top)) player.pos.Y += player.speed;
                            if (cur.Contains(player.pos.X + player.pos.Width / 2, player.pos.Bottom)) player.pos.Y -= player.speed;
                        }
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++) if (data[y, x] != 999) spriteBatch.Draw(tileset, new Rectangle(x * 64, y * 64, 64, 64), new Rectangle((data[y, x] % 6) * 32, (data[y, x] / 6) * 32, 32, 32), Color.White);
            }
        }
    }
}
