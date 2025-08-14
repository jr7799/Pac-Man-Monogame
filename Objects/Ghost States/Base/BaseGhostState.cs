using Microsoft.Xna.Framework;
using Rossi_PAC_MAN_Midterm.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.Objects.Ghost_States.Base
{
    public abstract class BaseGhostState
    {
        public abstract void PerformAction(GameTime gameTime, Point GridPosition, Vector2 VectorPosition, TileMap tileMap);
        public virtual bool IsWalkableGhostTile(int tx, int ty, TileMap tileMap)
        {
            if (tx < 0 || ty < 0 || tx >= TileMap.MAP_COLS || ty >= TileMap.MAP_ROWS)
                return false;

            Tile tempTile = null;
            foreach (var tile in tileMap.tiles)
            {
                if (tile.GridPosition == new Point(tx, ty))
                {
                    tempTile = tile;
                }
                else
                    continue;
            }
            return tempTile.IsGhostWalkable;
        }
        public virtual bool IsWalkableTile(int tx, int ty, TileMap tileMap)
        {
            if (tx < 0 || ty < 0 || tx >= TileMap.MAP_COLS || ty >= TileMap.MAP_ROWS)
                return false;

            Tile tempTile = null;
            foreach (var tile in tileMap.tiles)
            {
                if (tile.GridPosition == new Point(tx, ty))
                {
                    tempTile = tile;
                }
                else
                    continue;
            }
            return tempTile.IsWalkable;
        }
        public virtual Vector2 PointToVectorConvert(int tileSize, Point GridPosition)
        {
            return new Vector2(
                (GridPosition.X * (tileSize * Globals.spriteScale) + (tileSize * Globals.spriteScale) / 2),
                 GridPosition.Y * (tileSize * Globals.spriteScale) + (tileSize * Globals.spriteScale) / 2);
        }
    }
}
