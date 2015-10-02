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
    /// A player using a gamepad.
    /// </summary>
    class GamepadPlayer : Player
    {
        /// <summary>
        /// Creates a player that uses a Gamepad.
        /// </summary>
        /// <param name="texture">What texture to draw to represent the character.</param>
        /// <param name="position">The position of the character.</param>
        public GamepadPlayer(Texture2D texture, Rectangle position) : base(texture, position) { }
        /// <summary>
        /// Performs the logic to control the Player.
        /// </summary>
        /// <param name="pad">The state of the gamepad used to control this player.</param>
        public void Update(GamePadState pad)
        {
            if (pad.ThumbSticks.Left.X >= 0.5) pos.X += speed;
            if (pad.ThumbSticks.Left.X <= -0.5) pos.X -= speed;
            if (pad.ThumbSticks.Left.Y >= 0.5) pos.Y -= speed;
            if (pad.ThumbSticks.Left.Y <= -0.5) pos.Y += speed;
        }
    }
}
