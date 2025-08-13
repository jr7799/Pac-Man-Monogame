using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rossi_PAC_MAN_Midterm.Objects;
using Rossi_PAC_MAN_Midterm.Objects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.Environment
{
    public class PacManGridMap:TileMap
    {
        public List<Egg> eggs = new List<Egg>();
        public List<BaseGameObject> powerUps = new List<BaseGameObject>();

        public override void GenerateMap()
        {
            map = new int[MAP_ROWS, MAP_COLS]
           {
               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
               { 1,5,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,5,1 },
               { 1,0,1,1,0,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,0,1,1,0,1 },
               { 1,0,1,1,0,0,0,1,1,1,0,1,1,1,0,1,1,1,0,0,0,1,1,0,1 },
               { 1,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,1 },
               { 1,0,1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1,0,1 },
               { 1,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,1 },
               { 1,0,1,1,0,1,0,1,1,1,1,0,1,0,1,1,1,1,0,1,0,1,1,0,1 },
               { 1,0,1,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,0,1 },
               { 1,0,0,0,0,0,1,0,1,1,1,1,1,1,1,1,1,0,1,0,0,0,0,0,1 },
               { 1,0,1,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,1,0,1 },
               { 1,0,1,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,1,0,1 },
               { 1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,1,1,1,1,1,0,1,1,1,1,7,1,1,1,1,0,1,1,1,1,1,0,1 },
               { 1,0,0,0,0,0,0,0,1,2,2,2,2,2,2,2,1,0,0,0,0,0,0,0,1 },
               { 1,1,1,1,1,1,1,0,1,2,2,2,2,2,2,2,1,0,1,1,1,1,1,1,1 },
               { 1,0,0,0,0,0,0,0,1,2,2,2,2,2,2,2,1,0,0,0,0,0,0,0,1 },
               { 1,0,1,1,0,1,1,0,1,1,1,1,1,1,1,1,1,0,1,1,0,1,1,0,1 },
               { 1,0,1,1,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,1,1,0,1 },
               { 1,0,1,1,0,1,0,1,1,1,1,0,1,0,1,1,1,1,0,1,0,1,1,0,1 },
               { 1,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,1 },
               { 1,0,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1,1,1,0,1 },
               { 1,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,1 },
               { 1,0,1,1,0,0,0,1,1,1,0,1,1,1,0,1,1,1,0,0,0,1,1,0,1 },
               { 1,0,1,1,0,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,0,1,1,0,1 },
               { 1,5,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,5,1 },
               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }
           };
            float gridMapScaleValue = Globals.spriteScale;
            int newX, newY;
            for (int i = 0; i < totalSize; i++) //1
            {
                newX = i / MAP_ROWS;
                newY = i % MAP_ROWS;
                var tileValue = map[newY, newX];
                Tile tile = null;
                switch (tileValue)
                {
                    case 0:
                        tile = new Tile(TileType.Floor, Globals.tileMapImage, floorRect);
                        break;
                    case 1:
                        tile = new Tile(TileType.Wall, Globals.tileMapImage, wallRect);
                        break;
                    case 2:
                        tile = new Tile(TileType.FloorGate, Globals.tileMapImage, floorRect);
                        break;
                    case 5:
                        tile = new Tile(TileType.PowerupFloor, Globals.tileMapImage, cornerRect);
                        break;
                    case 7:
                        tile = new Tile(TileType.Gate, Globals.tileMapImage, gateRect);
                        break;
                    case 9:
                        tile = new Tile(TileType.Tube, Globals.tileMapImage, tubeRect);
                        break;

                }
                tile.index = tile.Type == TileType.Floor ? 0 : 1;
                tile.GridPosition = new Point(newX, newY);
                tile.VectorPosition = new Vector2(tile.GridPosition.X * (tileSize * gridMapScaleValue), tile.GridPosition.Y * (tileSize * gridMapScaleValue));
                if (tile.Type == TileType.Floor)
                    eggs.Add(new Egg(Globals.g_Content.Load<Texture2D>("newEgg"), new Point(tile.GridPosition.X, tile.GridPosition.Y), 16));

                if (tile.Type == TileType.PowerupFloor)
                    eggs.Add(new Egg(Globals.g_Content.Load<Texture2D>("powerEgg"), new Point(tile.GridPosition.X, tile.GridPosition.Y), 16, true));

                tiles.Add(tile);
            }
        }
        public override void DeleteMap()
        {
            tiles.Clear();
            eggs.Clear();
            map = null;
        }

    }
}
