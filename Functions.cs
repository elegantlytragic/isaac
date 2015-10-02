using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
namespace isaac
{
    /// <summary>
    /// A static class to hold random functions.
    /// </summary>
    static class Functions
    {
        /// <summary>
        /// A function to load the header of a level.
        /// </summary>
        /// <param name="name">The filename of the level.</param>
        /// <param name="width">The variable to write the width of the level to.</param>
        /// <param name="height">THe variable to write the height of the level to.</param>
        public static void LoadHeader(String name, out int width, out int height)
        {
            using (BinaryReader br = new BinaryReader(File.Open(name + ".dat", FileMode.OpenOrCreate)))
            {
                width = br.ReadInt32();
                height = br.ReadInt32();
            }
        }
        /// <summary>
        /// A function to create a level.
        /// </summary>
        /// <param name="levelname">The name of the level.</param>
        /// <param name="width">The width of the level.</param>
        /// <param name="height">The height of the level.</param>
        public static void CreateLevel(String levelname, int width, int height)
        {
            int width2 = width, height2 = height;
            int[,] data = new int[width, height], coldata = new int[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    data[x, y] = 999;
                    coldata[x, y] = 0;
                }
            }
            using (BinaryWriter bw = new BinaryWriter(File.Open(levelname + ".dat", FileMode.OpenOrCreate)))
            {
                bw.Write(width);
                bw.Write(height);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++) bw.Write(data[x, y]);
                }
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++) bw.Write(coldata[x, y]);
                }
            }
        }

    }
}
