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
    public class WinkyChaseState : BaseGhostState
    {
        bool hasTarget;
        public override void PerformAction(Player player, GameTime gameTime, ref Point GridPosition, ref Vector2 VectorPosition, ref Point pointDir, TileMap tileMap, int tileSize, float moveSpeed)
        {
            PerformActionChaseToXY(player.GridPosition.X - 1, player.GridPosition.Y, gameTime, ref GridPosition, ref VectorPosition, ref pointDir, tileMap, tileSize, moveSpeed);
        }
        public void PerformActionChaseToXY(int targetX, int targetY, GameTime gameTime, ref Point GridPosition, ref Vector2 VectorPosition, ref Point pointDir, TileMap tileMap, int tileSize, float moveSpeed)
        {
            if (!hasTarget)
            {
                Point reverse = new Point(-pointDir.X, -pointDir.Y);

                Point best = Point.Zero;
                int bestScore = int.MaxValue;

                for (int i = 0; i < Directions.Length; i++)
                {
                    Point point = Directions[i];
                    if (point == reverse) continue; //dont let new point be behind

                    int newX = GridPosition.X + point.X;
                    int newY = GridPosition.Y + point.Y;
                    if (!IsWalkableTile(newX, newY, tileMap)) continue;

                    int dist = Math.Abs(newX - targetX) + Math.Abs(newY - targetY);

                    int biasStraight = (point == pointDir) ? -1 : 0;

                    int deadEndPenalty = 0;
                    if (!IsWalkableTile(newX + point.X, newY + point.Y, tileMap))
                        deadEndPenalty = 2;

                    int score = dist * 10 + deadEndPenalty + biasStraight;

                    if (score < bestScore)
                    {
                        bestScore = score;
                        best = point;
                    }
                }

                // If nothing but reverse is available, allow reverse as last resort
                if (best == Point.Zero)
                {
                    Point point = reverse;
                    int nx = GridPosition.X + point.X, ny = GridPosition.Y + point.Y;
                    if (IsWalkableTile(nx, ny, tileMap)) best = point;
                }

                if (best != Point.Zero)
                {
                    pointDir = best;
                    hasTarget = true;          // lock route until next tile center
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
