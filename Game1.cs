/*------------CODE NOTES "FlowerBox-------------*/
/*
     //1 = Draw the grid map environment
    
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rossi_PAC_MAN_Midterm.Environment;
using System.Runtime.InteropServices;
using System;

namespace Rossi_PAC_MAN_Midterm
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //"Tile Map"
        //CREATE THE DIFFERENT RECTANGLE FOR TEXTURES FROM THE TILE MAP PNG
    

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1080;
            _graphics.PreferredBackBufferHeight = 1296;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Window.Title = "ROOT ROT STOMP - Rossi Pacman Midterm";

            Globals.Graphics = _graphics;


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Globals.tileMapImage = Content.Load<Texture2D>("Tile Map");

            GridMapCreator.GenerateMap();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            float gridMapScaleValue = 2.7f;
            GraphicsDevice.Clear(Color.WhiteSmoke);
            _spriteBatch.Begin();
           
            int newX, newY;
            for(int i = 0; i < GridMapCreator.totalSize; i++) //1
            {
                newX = i / GridMapCreator.mapRows;
                newY = i % GridMapCreator.mapRows;
                _spriteBatch.Draw(GridMapCreator.map[newY, newX].Texture, new Vector2(newX * (TileMap.tileSize * gridMapScaleValue), newY * (TileMap.tileSize * gridMapScaleValue)), GridMapCreator.map[newY, newX].textureSourceRectangle, Color.White, 0, Vector2.Zero, 2.7f, SpriteEffects.None, 0);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
