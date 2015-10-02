using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace isaac
{
    /// <summary>
    /// The base class that KeyboardPlayer, GamepadPlayer, and OnlinePlayer extend from.
    /// </summary>
    public abstract class Player
    {
        protected Texture2D tex;
        public Rectangle pos;
        public int speed;
        /// <summary>
        /// Creates a basic Player.
        /// </summary>
        /// <param name="texture">What texture to draw to represent the character.</param>
        /// <param name="position">The position of the character.</param>
        public Player(Texture2D texture, Rectangle position)
        {
            tex = texture; pos = position;
            speed = 10;
        }
        /// <summary>
        /// Draws the Player.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch from the main Draw function.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }
    }
}
