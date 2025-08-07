using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rossi_PAC_MAN_Midterm.Environment;
using Rossi_PAC_MAN_Midterm.Objects;
using Rossi_PAC_MAN_Midterm.States.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.States
{
    public class LoadingState : BaseGameState
    {
        public override void HandleInput(GameTime gameTime)
        {
            
        }

        public override void Initialize()
        {
           
        }

        public override void LoadContent(ContentManager Content)
        {
            Globals.tileMapImage = Content.Load<Texture2D>("Tile Map"); //goes into new state for Loading

            TileMap.GenerateMap();

            float gridMapScaleValue = 2.7f * Globals.windowScale;
            int newX, newY;
            for (int i = 0; i < TileMap.totalSize; i++) //1
            {
                newX = i / TileMap.MAP_ROWS;
                newY = i % TileMap.MAP_ROWS;
                var tile = TileMap.map[newY, newX];

                tile.index = tile.Type == TileType.Floor ? 0 : 1;
                tile.VectorPosition = new Vector2(newX * (TileMap.tileSize * gridMapScaleValue), newY * (TileMap.tileSize * gridMapScaleValue));
                AddGameObject(tile);
            }
        }

        public override void RenderStrings(SpriteBatch spriteBatch)
        {
            
        }

        public override void UnloadContent(ContentManager contentManager)
        {
           
        }
    }
}
