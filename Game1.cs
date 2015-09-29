using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace isaac
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        KeyboardPlayer p1;
        GamepadPlayer p2;
        List<Player> players;
        Room r;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 640;
            this.IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
            players = new List<Player>();
            p1 = new KeyboardPlayer(Content.Load<Texture2D>("p1"), new Rectangle(300, 300, 50, 50));
            p2 = new GamepadPlayer(Content.Load<Texture2D>("p2"), new Rectangle(300, 350, 50, 50));
            players.Add(p1);
            players.Add(p2);
            r = new Room("roomtest", 10, 10, Content.Load<Texture2D>("tileset"));
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();
            GamePadState pad = GamePad.GetState(PlayerIndex.One);
            if (pad.Buttons.Back == ButtonState.Pressed || keys.IsKeyDown(Keys.Escape)) this.Exit();
            p1.Update(keys);
            p2.Update(pad);
            r.Update(players);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            p1.Draw(spriteBatch);
            p2.Draw(spriteBatch);
            r.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
