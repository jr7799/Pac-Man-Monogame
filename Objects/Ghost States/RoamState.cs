using Microsoft.Xna.Framework;
using Rossi_PAC_MAN_Midterm.Environment;
using Rossi_PAC_MAN_Midterm.Objects.Ghost_States.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.Objects.Ghost_States
{
    public class RoamState : BaseGhostState
    {
        Random randomPoint = new Random();
        Point randDir = new Point();
        int x;
        int y;
        bool hasTarget;
        Point prevDir = new Point(0,0);

        int count;
        Point prevDirTwoAgo = new Point(0,0);

        public override void PerformAction(Player player, GameTime gameTime, ref Point GridPosition,
                                            ref Vector2 VectorPosition, ref Point pointDir, 
                                            TileMap tileMap, int tileSize, float moveSpeed)
        {
            if (!hasTarget)
            {
                randDir = Directions[randomPoint.Next(Directions.Length)];
                x = randDir.X;
                y = randDir.Y;
                if (IsWalkableTile(GridPosition.X + x, GridPosition.Y + y, tileMap))
                {
                    if(count == 3)
                    {
                        if (prevDirTwoAgo != new Point(x, y))
                        {
                            pointDir = new Point(x, y);
                            hasTarget = true;
                        }                       
                    }
                    else
                    {
                        pointDir = new Point(x, y);
                        hasTarget = true;
                    }
                }
            }

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (IsWalkableTile(GridPosition.X + pointDir.X, GridPosition.Y + pointDir.Y, tileMap))
            {
                Vector2 targetPos = PointToVectorConvert(tileSize, GridPosition + pointDir);
                Vector2 toTarget = targetPos - VectorPosition;
            
                float distThisFrame = moveSpeed * 0.5f * deltaTime;
                if (toTarget.Length() <= distThisFrame)
                {
                    if (CheckSurroundings(ref GridPosition, ref VectorPosition, tileMap) != prevDir)
                    {
                        hasTarget = false;
                    }
                    VectorPosition = targetPos;
                    GridPosition += pointDir;
                    prevDir = new Point(-pointDir.X, -pointDir.Y);
                    if(count == 0)
                    {
                        prevDirTwoAgo = pointDir;
                    }
                    count++;
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
