using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rossi_PAC_MAN_Midterm.Environment;
using System.Runtime.InteropServices;
using System;
using Rossi_PAC_MAN_Midterm.States;
using Rossi_PAC_MAN_Midterm.States.Base;

namespace Rossi_PAC_MAN_Midterm
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        BaseGameState currentState = new LoadingState();
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            SetWindowScale();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Window.Title = "ROOT ROT STOMP - Rossi Pacman Midterm";
            Globals.Graphics = _graphics;

            currentState.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            currentState.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            _spriteBatch.Begin();

            currentState.Render(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void SetWindowScale()
        {
            const int BASE_W = 1080;
            const int BASE_H = 1296;
            int screenW = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int screenH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            float scaleW = (float)screenW / BASE_W;
            float scaleH = (float)screenH / BASE_H;
            float scale = Math.Min(scaleW, scaleH) / 1.2f;
            Globals.windowScale = scale;
            int w = (int)(BASE_W * scale);
            int h = (int)(BASE_H * scale);
            _graphics.PreferredBackBufferWidth = w;
            _graphics.PreferredBackBufferHeight = h;
            _graphics.ApplyChanges();
        }
    }
}

