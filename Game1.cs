using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rossi_PAC_MAN_Midterm.Environment;
using Rossi_PAC_MAN_Midterm.FSM;
using Rossi_PAC_MAN_Midterm.FSM.Base;
using Rossi_PAC_MAN_Midterm.States;
using Rossi_PAC_MAN_Midterm.States.Base;
using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace Rossi_PAC_MAN_Midterm
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        GameFSM gameFSM = new GameFSM();
        private string stateSwitchingKey;
        private string quitGameKey;

        private void SwitchGameState(string key)
        {
            if (gameFSM.M_STATE != null) //un-sub to old
            {
                gameFSM.M_STATE.OnStateSwitched -= OnStateSwitchRequestedFromState;
                gameFSM.M_STATE.OnGameSignals -= OnGameSignalRequestedFromState;
            }
            gameFSM.SwitchState(key); //switch

            if (gameFSM.M_STATE != null) //sub to new
            {
                gameFSM.M_STATE.OnStateSwitched += OnStateSwitchRequestedFromState;
                gameFSM.M_STATE.OnGameSignals += OnGameSignalRequestedFromState;
            }
        }
        private void OnStateSwitchRequestedFromState(string targetKey)
        {
            stateSwitchingKey = targetKey;
        }
        private void OnGameSignalRequestedFromState(string targetKey)
        {
            quitGameKey = targetKey;
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            SetWindowScale();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            Window.Title = "ROOT ROMP & STOMP - Rossi Pacman Midterm";
            Globals.Graphics = _graphics;
            Globals.windowSize = new Point(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Globals.spriteScale = 2.7f * Globals.windowScale;

            gameFSM.Initialize("");

            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.g_Content = Content;
            SwitchGameState("LOAD_STATE");
        }
        protected override void UnloadContent()
        {
            gameFSM.M_STATE.UnloadContent(Globals.g_Content);
        }
        protected override void Update(GameTime gameTime)
        {
            if (!string.IsNullOrEmpty(stateSwitchingKey))
            {
                SwitchGameState(stateSwitchingKey);
                stateSwitchingKey = null;
            }
            if(!string.IsNullOrEmpty(quitGameKey))
            {
                Exit();
            }
            gameFSM.Update(gameTime, Point.Zero, Vector2.Zero);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            _spriteBatch.Begin();

            gameFSM.DrawRenders(_spriteBatch);

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

