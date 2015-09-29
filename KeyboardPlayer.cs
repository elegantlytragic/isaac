using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
namespace isaac
{
    class KeyboardPlayer : Player
    {
        public KeyboardPlayer(Texture2D texture, Rectangle position) : base(texture, position) {}
        public void Update(KeyboardState keys)
        {
            if (keys.IsKeyDown(Keys.W)) pos.Y -= speed;
            if (keys.IsKeyDown(Keys.S)) pos.Y += speed;
            if (keys.IsKeyDown(Keys.A)) pos.X -= speed;
            if (keys.IsKeyDown(Keys.D)) pos.X += speed;
        }
    }
}
