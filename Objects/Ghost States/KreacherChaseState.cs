using Microsoft.Xna.Framework;
using Rossi_PAC_MAN_Midterm.Environment;
using Rossi_PAC_MAN_Midterm.Objects.Ghost_States.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.Objects.Ghost_States
{
    public class KreacherChaseState : BaseGhostState
    {
        bool hasTarget;
        public override void PerformAction(Player player, GameTime gameTime, ref Point GridPosition, ref Vector2 VectorPosition, ref Point pointDir, TileMap tileMap, int tileSize, float moveSpeed)
        {
            PerformActionChaseToXY(player.GridPosition.X, player.GridPosition.Y, gameTime, ref GridPosition, ref VectorPosition, ref pointDir, tileMap, tileSize, moveSpeed);
        }
        public void PerformActionChaseToXY(int targetX, int targetY, GameTime gameTime, ref Point GridPosition, ref Vector2 VectorPosition, ref Point pointDir, TileMap tileMap, int tileSize, float moveSpeed)
        {
            if (!hasTarget) //needs target
            {
                Point reverseDir = new Point(-pointDir.X, -pointDir.Y); //set "reverse" / 180 direction to prevent turning around 

                Point bestDir = Point.Zero; // the point to check with
                int bestDistScore = int.MaxValue; //start value as large as possible for first step

                for (int i = 0; i < Directions.Length; i++)
                {
                    Point newPointDir = Directions[i];
                    if (newPointDir == reverseDir) continue; //dont let new point be behind

                    int newX = GridPosition.X + newPointDir.X;
                    int newY = GridPosition.Y + newPointDir.Y;
                    if (!IsWalkableTile(newX, newY, tileMap)) continue;

                    //get distance for new point to check later if this distance is larger than the best distance score so far
                    int dist = Math.Abs(newX - targetX) + Math.Abs(newY - targetY);

                    int biasStraight = (newPointDir == pointDir) ? -1 : 0; //if two newPointDir have the same distance above,
                                                                           //this will make whichever is moving in the same as the current direction the preferred

                    int deadEndPenalty = 0;
                    if (!IsWalkableTile(newX + newPointDir.X, newY + newPointDir.Y, tileMap)) //if dead end two ahead, newX plus newPointDir is two in front of current location
                        deadEndPenalty = 2;                                                   //add two to distance score to prevent that point if possible

                    int distScore = dist * 10 + deadEndPenalty + biasStraight; // create score to see if its the lowest of all directions, reall is the distance with added modifiers

                    if (distScore < bestDistScore) //if the lowest by the end of the loop, that dir is the bestDir
                    {
                        bestDistScore = distScore;
                        bestDir = newPointDir;
                    }
                }

                // If nothing but reverse is available, allow reverse as last resort
                if (bestDir == Point.Zero)
                {
                    Point point = reverseDir;
                    int nx = GridPosition.X + point.X, ny = GridPosition.Y + point.Y;
                    if (IsWalkableTile(nx, ny, tileMap)) bestDir = point;
                }

                //if bestDir not zero, either new dir or reverse, assign as pointDir and continue below
                if (bestDir != Point.Zero)
                {
                    pointDir = bestDir;
                    hasTarget = true;
                }
            }

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (IsWalkableTile(GridPosition.X + pointDir.X, GridPosition.Y + pointDir.Y, tileMap))
            {
                Vector2 targetPos = PointToVectorConvert(tileSize, GridPosition + pointDir);
                Vector2 toTarget = targetPos - VectorPosition;

                float distThisFrame = moveSpeed * 0.5f * deltaTime; // keep your scaling
                if (toTarget.Length() <= distThisFrame)
                {
                    VectorPosition = targetPos;
                    GridPosition += pointDir;
                    hasTarget = false;
                }
                else
                {
                    VectorPosition += Vector2.Normalize(toTarget) * distThisFrame;
                }
            }
            else //in case blocked
            {
                hasTarget = false;
            }
        }
    }
}
