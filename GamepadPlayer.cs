using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
namespace isaac
{
    class GamepadPlayer : Player
    {
        public GamepadPlayer(Texture2D texture, Rectangle position) : base(texture, position) { }
        public void Update(GamePadState pad)
        {
            if (pad.ThumbSticks.Left.X >= 0.75) pos.X += speed;
            if (pad.ThumbSticks.Left.X <= -0.75) pos.X -= speed;
            if (pad.ThumbSticks.Left.Y >= 0.75) pos.Y -= speed;
            if (pad.ThumbSticks.Left.Y <= -0.75) pos.Y += speed;
        }
    }
}
