using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
namespace isaac
{
    /// <summary>
    /// A player that uses the keyboard.
    /// </summary>
    class KeyboardPlayer : Player
    {
        /// <summary>
        /// Creates a player that uses the keyboard.
        /// </summary>
        /// <param name="texture">What texture to draw to represent the character.</param>
        /// <param name="position">The position of the character.</param>
        public KeyboardPlayer(Texture2D texture, Rectangle position) : base(texture, position) {}
        /// <summary>
        /// Performs the logic to control the player.
        /// </summary>
        /// <param name="keys">The keyboard state of the player.</param>
        public void Update(KeyboardState keys)
        {
            if (keys.IsKeyDown(Keys.W)) pos.Y -= speed;
            if (keys.IsKeyDown(Keys.S)) pos.Y += speed;
            if (keys.IsKeyDown(Keys.A)) pos.X -= speed;
            if (keys.IsKeyDown(Keys.D)) pos.X += speed;
        }
    }
}
