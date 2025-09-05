using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rossi_PAC_MAN_Midterm.Environment;
using Rossi_PAC_MAN_Midterm.Objects;
using Rossi_PAC_MAN_Midterm.States.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Net.Mime.MediaTypeNames;

namespace Rossi_PAC_MAN_Midterm.States
{
    public class LoadingState : BaseGameState
    {
        int _selectionIndex;
        string[] _menuItems = { "Play Game", "Exit" };
        KeyboardState _previousKeyboardState;
        TimeSpan _keyPressDelay = TimeSpan.FromMilliseconds(150);
        TimeSpan _elapsedTime;

        private StartTileMap startmenuMap = new StartTileMap();
        public override void Initialize()
        {
        }
        public override void LoadContent(ContentManager Content)
        {
            Globals.tileMapImage = Globals.g_Content.Load<Texture2D>("Tile Map");
            Globals.g_font = Globals.g_Content.Load<SpriteFont>("Pixel");

            //Generate Level Tiles for next state to use
            startmenuMap.GenerateMap();
            foreach (var tile in startmenuMap.tiles)
            {
                AddGameObject(tile);
            }
        }

        public override void UnloadContent(ContentManager contentManager)
        {
            contentManager.Unload();
        }
        public override void HandleInput(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            _elapsedTime += gameTime.ElapsedGameTime;

            if (_elapsedTime >= _keyPressDelay)
            {
                if (currentKeyboardState.IsKeyDown(Keys.W) && !_previousKeyboardState.IsKeyDown(Keys.W))
                {
                    _selectionIndex--;
                    if (_selectionIndex < 0)
                    {
                        _selectionIndex = _menuItems.Length - 1;
                    }
                    _elapsedTime = TimeSpan.Zero;
                }

                if (currentKeyboardState.IsKeyDown(Keys.S) && !_previousKeyboardState.IsKeyDown(Keys.S))
                {
                    _selectionIndex++;
                    if (_selectionIndex >= _menuItems.Length)
                    {
                        _selectionIndex = 0;
                    }
                    _elapsedTime = TimeSpan.Zero;
                }

                if (currentKeyboardState.IsKeyDown(Keys.Enter) && !_previousKeyboardState.IsKeyDown(Keys.Enter))
                {
                    switch (_selectionIndex)
                    {
                        case 0:
                            InvokeStateSwitched("GAME_STATE");
                            break;
                        case 1:
                            InvokeGameSignals("QUIT_GAME");
                            break;
                    }
                    _elapsedTime = TimeSpan.Zero;
                }
            }
            _previousKeyboardState = currentKeyboardState;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void RenderStrings(SpriteBatch _spriteBatch)
        {
            string title = "ROOT ROMP & STOMP";

            Vector2 titlePos = new Vector2(Globals.Graphics.PreferredBackBufferWidth / 2 - Globals.g_font.MeasureString(title).X / 2, Globals.Graphics.PreferredBackBufferHeight / 2 - Globals.g_font.MeasureString(title).Y * 2.5f);
            _spriteBatch.DrawString(Globals.g_font, title, titlePos, Color.White);

            Vector2 instPos = new Vector2(0 + Globals.g_font.MeasureString(title).X / 4, 32);
            _spriteBatch.DrawString(Globals.g_font, "SMASH ALL THE SLIMES EGGS AND AVOID BEING CAUGHT!!!!", instPos, Color.White);

            Vector2 ExitPos = new Vector2(0 + Globals.g_font.MeasureString(title).X / 4, 32 * 3);
            _spriteBatch.DrawString(Globals.g_font, "EXIT = ESC", ExitPos, Color.White);

            Vector2 NavPos1 = new Vector2(0 + Globals.g_font.MeasureString(title).X / 4, 32 * 4);
            _spriteBatch.DrawString(Globals.g_font, "UP = W", NavPos1, Color.White);
            Vector2 NavPos2 = new Vector2(0 + Globals.g_font.MeasureString(title).X / 4, 32 * 5);
            _spriteBatch.DrawString(Globals.g_font, "DOWN = S", NavPos2, Color.White);
            Vector2 NavPos3 = new Vector2(0 + Globals.g_font.MeasureString(title).X / 4, 32 * 6);
            _spriteBatch.DrawString(Globals.g_font, "LEFT = A", NavPos3, Color.White);
            Vector2 NavPos4 = new Vector2(0 + Globals.g_font.MeasureString(title).X / 4, 32 * 7);
            _spriteBatch.DrawString(Globals.g_font, "RIGHT = D", NavPos4, Color.White);
            Vector2 NavPos5 = new Vector2(0 + Globals.g_font.MeasureString(title).X / 4, 32 * 8);
            _spriteBatch.DrawString(Globals.g_font, "SELECT = ENTER", NavPos5, Color.White);

            for (int i = 0; i < _menuItems.Length; i++)
            {
                Color color = i == _selectionIndex ? Color.Yellow : Color.White;
                Vector2 position = new Vector2(Globals.Graphics.PreferredBackBufferWidth / 2 - Globals.g_font.MeasureString(_menuItems[i]).X / 2, Globals.Graphics.PreferredBackBufferHeight / 2 + Globals.g_font.MeasureString(title).Y * i * 1.5f);
                _spriteBatch.DrawString(Globals.g_font, _menuItems[i], position, color);
            }
        }


    }
}
