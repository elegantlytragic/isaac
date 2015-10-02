using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace isaac
{
    /// <summary>
    /// A class to hold the Room data.
    /// </summary>
    class Room
    {
        public int[,] data, col;
        public int width, height;
        private Texture2D tileset;
        public string name;
        /// <summary>
        /// Create a new Room.
        /// </summary>
        /// <param name="filename">The filename of the room.</param>
        /// <param name="w">The width of the room.</param>
        /// <param name="h">The height of the room.</param>
        /// <param name="t">The texture of the tileset of the room.</param>
        public Room(string filename, int w, int h, Texture2D t)
        {
            tileset = t;
            name = filename;
            using (BinaryReader br = new BinaryReader(File.Open(filename + ".dat", FileMode.Open)))
            {
                width = br.ReadInt32();
                height = br.ReadInt32();
                data = new int[width, height];
                col = new int[width, height];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++) data[x, y] = br.ReadInt32();
                }
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++) col[x, y] = br.ReadInt32();
                }
            }
        }
        /// <summary>
        /// Performs collision detection.
        /// </summary>
        /// <param name="players">A list of all players to detect collision.</param>
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
        /// <summary>
        /// Draws the room.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch from the main Draw function.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++) if (data[y, x] != 999) spriteBatch.Draw(tileset, new Rectangle(x * 64, y * 64, 64, 64), new Rectangle((data[y, x] % 6) * 32, (data[y, x] / 6) * 32, 32, 32), Color.White);
            }
        }
        /// <summary>
        /// Draws the collision data of the level.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch from the main Draw function.</param>
        /// <param name="notsolid">The texture to draw for a non-solid tile.</param>
        /// <param name="solid">The texture to draw for a solid tile.</param>
        public void DrawCollision(SpriteBatch spriteBatch, Texture2D notsolid, Texture2D solid)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (col[x, y] == 1) spriteBatch.Draw(solid, new Rectangle(x * 64, y * 64, 64, 64), Color.White); //Draw solid tile
                    if (col[x, y] == 0) spriteBatch.Draw(notsolid, new Rectangle(x * 64, y * 64, 64, 64), Color.White); //Draw non-solid tile
                }
            }
        }
        /// <summary>
        /// Saves any changes to the Room made in the editor.
        /// </summary>
        public void Save()
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(name + ".dat", FileMode.Open)))
            {
                bw.Write(width);
                bw.Write(height);
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++) bw.Write(data[x, y]);
                }
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++) bw.Write(col[x, y]);
                }
            }
        }
    }
}
