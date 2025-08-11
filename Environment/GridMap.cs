using Microsoft.Xna.Framework;
using Rossi_PAC_MAN_Midterm.Objects;
using Microsoft.Xna.Framework.Graphics;
using Rossi_PAC_MAN_Midterm.Objects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.Environment
{
    /*
     *   -------NOT BEING USED PRESET TILES, KEPT FOR COPY AND PASTE-----|---Map---|
     *   -------------------floor tiles = walkable-----------------------|---Key---|
     *   new Tile(TileType.Floor, Globals.tileMapImage, floorRect);      |    0    |
     *   -------------------wall tiles = !walkable-----------------------|---------|
     *   new Tile(TileType.Wall, Globals.tileMapImage, wallRect);        |    1    |
     *   -------------powerup floor = Walkable and contains pickups------|---------|
     *   new Tile(TileType.Floor, Globals.tileMapImage, cornerRect);     |    5    |
     *   ------------------tube/teleport tiles---------------------------|---------|
     *   new Tile(TileType.Floor, Globals.tileMapImage, tubeRect);       |    9    |
    */

    public class TileMap
    {
        public const int tileSize = 16;
        public Rectangle floorRect = new Rectangle(6 * tileSize, 7 * tileSize, tileSize, tileSize);   
        public Rectangle wallRect = new Rectangle(1 * tileSize, 4 * tileSize, tileSize, tileSize);    
        public Rectangle cornerRect = new Rectangle(6 * tileSize, 7 * tileSize, tileSize, tileSize);   
        public Rectangle tubeRect = new Rectangle(1 * tileSize, 11 * tileSize, tileSize, tileSize);   //red gem floor "teleport"
        public Rectangle gateRect = new Rectangle(9 * tileSize, 10 * tileSize, tileSize, tileSize);   //red gem floor "teleport"

        public const int MAP_ROWS = 30;
        public const int MAP_COLS = 25;
        public const int totalSize = MAP_ROWS * MAP_COLS;

        public int[,] map;
        public List<Tile> tiles = new List<Tile>();
        public virtual void GenerateMap()
        {
            map = new int[MAP_ROWS, MAP_COLS]
            {
               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
               { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
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

                tiles.Add(tile);              
            }
        }
        public virtual void DeleteMap()
        {
            tiles.Clear();
            map = null;
        }
    }
}
