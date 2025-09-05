using Microsoft.Xna.Framework;
using Rossi_PAC_MAN_Midterm.Environment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.Objects.Ghost_States.Base
{
    public abstract class BaseGhostState
    {
        public float startTimer = 2.5f;
        public static readonly Point[] Directions = new[]
        {
            new Point( 1, 0), // right
            new Point(-1, 0), // left
            new Point( 0, 1), // down
            new Point( 0,-1)  // up
        };
        public abstract void PerformAction(Player player, GameTime gameTime, ref Point GridPosition, ref Vector2 VectorPosition, 
                                            ref Point pointDir, TileMap tileMap, int tileSize, float moveSpeed);
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
        public virtual Point CheckSurroundings(ref Point GridPosition, ref Vector2 VectorPosition, TileMap tileMap)
        {
            Random rand = new Random();

            foreach(Point dir in Directions)
            {
                int checkX = GridPosition.X + dir.X;
                int checkY = GridPosition.Y + dir.Y;

                if (IsWalkableTile(checkX, checkY, tileMap))
                {
                    if (rand.Next(2) == 0)
                        return new Point(dir.X, dir.Y);
                    else
                        continue; 
                }
            }
            return Point.Zero;
        }      

        //below only works if path is 100% clear and walkable, used to have ghosts exit the "gate" area
        public virtual void MoveToDirectGhostTiles(GameTime gameTime, int x, int y, ref Point GridPosition, 
                                                    ref Vector2 VectorPosition, ref Point pointDir, TileMap tileMap, float moveSpeed, int tileSize)
        {
            int newXTarget = x - GridPosition.X;
            int newYTarget = y - GridPosition.Y;
            int pointIndex = 0;
            List<Point> targetDirections = BreakPoint(newXTarget, newYTarget, tileMap);

            if(pointIndex < targetDirections.Count)
            {
                Point point = targetDirections[pointIndex];
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (IsWalkableGhostTile(GridPosition.X + point.X, GridPosition.Y + point.Y, tileMap) || IsWalkableTile(GridPosition.X + point.X, GridPosition.Y + point.Y, tileMap))
                {
                    Vector2 targetPos = PointToVectorConvert(tileSize, GridPosition + point);
                    Vector2 toTarget = targetPos - VectorPosition;

                    float distThisFrame = moveSpeed * deltaTime;
                    if (toTarget.Length() <= distThisFrame)
                    {
                        pointIndex++;
                        VectorPosition = targetPos;
                        GridPosition += point;
                    }
                    else
                    {
                        VectorPosition += Vector2.Normalize(toTarget) * distThisFrame;
                    }
                }
            }
        }
        public List<Point> BreakPoint(int x, int y, TileMap tileMap)
        {
            var steps = new List<Point>();

            int stepX = Math.Sign(x); // -1, 0, or 1
            int stepY = Math.Sign(y);

            for (int i = 0; i < Math.Abs(x); i++)
            {
                steps.Add(new Point(stepX, 0));
            }

            for (int i = 0; i < Math.Abs(y); i++)
            {
                steps.Add(new Point(0, stepY));
            }

            return steps;
        }

        
    }
}
