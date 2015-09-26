using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace isaac
{
    public abstract class Player
    {
        protected Texture2D tex;
        protected Vector2 pos;
        protected int speed;
        public Player(Texture2D texture, Vector2 position)
        {
            tex = texture; pos = position;
            speed = 10;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }
    }
}
