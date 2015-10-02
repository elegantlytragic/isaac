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
    public enum GameState
    {
        MainMenu,
        Game,
        Editor,
        EditorSelect
    }
    public enum EditorState
    {
        Tiles,
        Grid,
        Collision
    }
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        KeyboardPlayer p1;
        GamepadPlayer p2;
        List<Player> players;
        Room r, editor;
        GameState gameState = GameState.MainMenu;
        EditorState editorState = EditorState.Tiles;
        Textbox levelName;
        InputHelper help;
        int editSelected = 0;
        Vector2 tileSelected = new Vector2(0, 0);
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            this.IsMouseVisible = true;
            this.Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
            help = new InputHelper();
            players = new List<Player>();
            p1 = new KeyboardPlayer(Content.Load<Texture2D>("p1"), new Rectangle(300, 300, 50, 50));
            p2 = new GamepadPlayer(Content.Load<Texture2D>("p2"), new Rectangle(300, 350, 50, 50));
            players.Add(p1);
            players.Add(p2);
            r = new Room("deer", 10, 10, Content.Load<Texture2D>("tileset"));
            levelName = new Textbox(font, "deer", Color.White, Vector2.Zero);
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
            help.Update();
            if (pad.Buttons.Back == ButtonState.Pressed || keys.IsKeyDown(Keys.Escape)) this.Exit();
            switch (gameState)
            {
                #region MainMenu
                case GameState.MainMenu:
                    if (keys.IsKeyDown(Keys.P) || pad.IsButtonDown(Buttons.A)) gameState = GameState.Game;
                    if (keys.IsKeyDown(Keys.E) || pad.IsButtonDown(Buttons.X)) gameState = GameState.EditorSelect;
                    break;
                #endregion
                #region Game
                case GameState.Game:
                    p1.Update(keys);
                    p2.Update(pad);
                    r.Update(players);
                    break;
                #endregion
                #region EditorSelect
                case GameState.EditorSelect:
                    levelName.focused = true;
                    levelName.Update(keys.GetPressedKeys());
                    if (keys.IsKeyDown(Keys.Enter))
                    {
                        int w, h;
                        Functions.CreateLevel(levelName.value, 10, 10);
                        if (File.Exists(levelName.value + ".dat"))
                        {
                            Functions.LoadHeader(levelName.value, out w, out h);
                            editor = new Room(levelName.value, w, h, Content.Load<Texture2D>("tileset"));
                        }
                        else
                        {
                            Functions.LoadHeader(levelName.value, out w, out h);
                            Functions.CreateLevel(levelName.value, 10, 10);
                            editor = new Room(levelName.value, 10, 10, Content.Load<Texture2D>("tileset"));
                        }
                        gameState = GameState.Editor;
                    }
                    break;
                #endregion
                #region Editor
                case GameState.Editor:
                    switch(editorState)
                    {
                        case EditorState.Tiles:
                            if (help.IsNewPress(Keys.W)) { if (tileSelected.Y >= 1) tileSelected.Y--; }
                            if (help.IsNewPress(Keys.S)) { if (tileSelected.Y <= editor.height - 2) tileSelected.Y++; }
                            if (help.IsNewPress(Keys.D)) { if (tileSelected.X <= editor.width - 2) tileSelected.X++; }
                            if (help.IsNewPress(Keys.A)) { if (tileSelected.X >= 1) tileSelected.X--; }
                            if (help.IsNewPress(Keys.Space)) editor.data[(int)tileSelected.Y, (int)tileSelected.X] = editSelected;
                            if (help.IsNewPress(Keys.E)) editorState = EditorState.Grid;
                            if (help.IsNewPress(Keys.C)) editorState = EditorState.Collision;
                            break;
                        case EditorState.Grid:
                            if (help.IsNewPress(Keys.W)) { if (editSelected >= 6) editSelected -= 6; }
                            if (help.IsNewPress(Keys.S)) { if (editSelected <= 41) editSelected += 6; }
                            if (help.IsNewPress(Keys.A)) { if (editSelected > 1) editSelected--; }
                            if (help.IsNewPress(Keys.D)) { if (editSelected < 47) editSelected++; }
                            if (help.IsNewPress(Keys.E)) editorState = EditorState.Tiles;
                            if (help.IsNewPress(Keys.C)) editorState = EditorState.Collision;
                            break;
                        case EditorState.Collision:
                            if (help.IsNewPress(Keys.W)) { if (tileSelected.Y >= 1) tileSelected.Y--; }
                            if (help.IsNewPress(Keys.S)) { if (tileSelected.Y <= editor.height - 2) tileSelected.Y++; }
                            if (help.IsNewPress(Keys.D)) { if (tileSelected.X <= editor.width - 2) tileSelected.X++; }
                            if (help.IsNewPress(Keys.A)) { if (tileSelected.X >= 1) tileSelected.X--; }
                            if (help.IsNewPress(Keys.D0)) editor.col[(int)tileSelected.X, (int)tileSelected.Y] = 0;
                            if (help.IsNewPress(Keys.D1)) editor.col[(int)tileSelected.X, (int)tileSelected.Y] = 1;
                            if (help.IsNewPress(Keys.E)) editorState = EditorState.Tiles;
                            break;
                    }
                    if (help.IsNewPress(Keys.F1))
                    {
                        editor.Save();
                        gameState = GameState.MainMenu;
                    }
                    break;
                #endregion
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            switch (gameState)
            {
                #region MainMenu
                case GameState.MainMenu:
                    spriteBatch.DrawString(font, "Press P (keyboard) or A (gamepad) to play!", Vector2.Zero, Color.White);
                    spriteBatch.DrawString(font, "Press E (keyboard) or X (gamepad) to open the level editor.", new Vector2(0, 20), Color.White);
                    break;
                #endregion
                #region Game
                case GameState.Game:
                    p1.Draw(spriteBatch);
                    p2.Draw(spriteBatch);
                    r.Draw(spriteBatch);
                    break;
                #endregion
                #region EditorSelect
                case GameState.EditorSelect:
                    levelName.Draw(spriteBatch);
                    break;
                #endregion
                #region Editor
                case GameState.Editor:
                    Vector2 selectPos = new Vector2(tileSelected.X * 64 - 4, tileSelected.Y * 64 - 4),
                            gridPos = new Vector2((editSelected % 6) * 64 + 696, (editSelected / 6) * 64 - 4);
                    editor.Draw(spriteBatch);
                    spriteBatch.Draw(Content.Load<Texture2D>("tileset"), new Rectangle(700, 0, 384, 512), Color.White);
                    switch (editorState)
                    {
                        case EditorState.Grid:
                            spriteBatch.Draw(Content.Load<Texture2D>("redselect"), selectPos, Color.White); //Normal select as red
                            spriteBatch.Draw(Content.Load<Texture2D>("blueselect"), gridPos, Color.White); //Tile select as blue
                            spriteBatch.DrawString(font, "Selecting tiles, WASD - Move cursor, E - Switch to editing level, C - Switch to editing collision, F1 - Save and Quit", new Vector2(0, 680), Color.White);
                            break;
                        case EditorState.Tiles:
                            spriteBatch.Draw(Content.Load<Texture2D>("blueselect"), selectPos, Color.White); //Normal select as blue
                            spriteBatch.Draw(Content.Load<Texture2D>("redselect"), gridPos, Color.White); //Tile select as red
                            spriteBatch.DrawString(font, "Editing level, WASD - Move cursor, E - Switch to selecting tiles, Space - Place tile, C - Switch to editing collision, F1 - Save and Quit", new Vector2(0, 680), Color.White);
                            break;
                        case EditorState.Collision:
                            editor.DrawCollision(spriteBatch, Content.Load<Texture2D>("notsolid"), Content.Load<Texture2D>("solid"));
                            spriteBatch.Draw(Content.Load<Texture2D>("blueselect"), selectPos, Color.White); //Normal select as blue
                            spriteBatch.Draw(Content.Load<Texture2D>("redselect"), gridPos, Color.White); //Tile select as red
                            spriteBatch.DrawString(font, "Editing collision, WASD - Move cursor, E - Switch to editing level, 0 - Make non-solid, 1 - Make solid, F1 - Save and Quit", new Vector2(0, 680), Color.White);
                            break;
                    }
                    spriteBatch.DrawString(font, "Editing " + editor.name, new Vector2(1084, 0), Color.White);
                    break;
                #endregion
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
