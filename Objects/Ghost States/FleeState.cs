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
    public class FleeState : BaseGhostState
    {
        Random randomPoint = new Random();
        bool hasTarget;
        Point prevDir = new Point(0, 0);

        public override void PerformAction(Player player, GameTime gameTime, ref Point GridPosition, 
            ref Vector2 VectorPosition, ref Point pointDir, TileMap tileMap, int tileSize, float moveSpeed)
        {
            if (!hasTarget)
            {
                //ghost from player
                int distBefore = Math.Abs(GridPosition.X - player.GridPosition.X) + Math.Abs(GridPosition.Y - player.GridPosition.Y);

                // Randomize start
                int start = randomPoint.Next(Directions.Length);

                Point bestNotCloserToPlayer = Point.Zero; // first walkable dir that is not closer
                Point bestAnyDir = Point.Zero;       // fallback: walkable dir with max distAfter
                int bestAnyDist = int.MinValue;

                for (int i = 0; i < Directions.Length; i++)
                {
                    Point point = Directions[(start + i) % Directions.Length];
                    int x = GridPosition.X + point.X;
                    int y = GridPosition.Y + point.Y;

                    if (!IsWalkableTile(x, y, tileMap)) continue;

                    //check distance after a point is chosen
                    int distAfter = Math.Abs(x - player.GridPosition.X) + Math.Abs(y - player.GridPosition.Y);

                    //is distance after is bigger than distance before new point, move to that point meaning away
                    if (bestNotCloserToPlayer == Point.Zero && distAfter >= distBefore)
                    {
                        bestNotCloserToPlayer = point;
                    }

                    //cache other point that may be closer but does not put into wall or corner
                    if (distAfter > bestAnyDist)
                    {
                        bestAnyDist = distAfter;
                        bestAnyDir = point;
                    }
                }

                //choose which is a a better option
                Point chosen = (bestNotCloserToPlayer != Point.Zero) ? bestNotCloserToPlayer : bestAnyDir;

                if (chosen != Point.Zero)
                {
                    pointDir = chosen;
                    hasTarget = true;
                }
            }

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (IsWalkableTile(GridPosition.X + pointDir.X, GridPosition.Y + pointDir.Y, tileMap))
            {
                Vector2 targetPos = PointToVectorConvert(tileSize, GridPosition + pointDir);
                Vector2 toTarget = targetPos - VectorPosition;

                float distThisFrame = moveSpeed * .1f * deltaTime;
                if (toTarget.Length() <= distThisFrame)
                {
                    if (CheckSurroundings(ref GridPosition, ref VectorPosition, tileMap) != prevDir)
                    {
                        hasTarget = false;
                    }
                    VectorPosition = targetPos;
                    GridPosition += pointDir;
                    prevDir = new Point(-pointDir.X, -pointDir.Y);
                }
                else
                {
                    VectorPosition += Vector2.Normalize(toTarget) * distThisFrame;
                }
            }
            else
            {
                if (CheckSurroundings(ref GridPosition, ref VectorPosition, tileMap) != prevDir)
                {
                    hasTarget = false;
                }
            }
        }
       
    }
}
