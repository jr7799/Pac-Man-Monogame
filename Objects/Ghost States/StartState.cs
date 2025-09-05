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
    public class StartState : BaseGhostState
    {
        Random randomPoint = new Random();
        Point randDir = new Point();
        int x;
        int y;
        bool hasTarget;
        public override void PerformAction(Player player, GameTime gameTime,
                                            ref Point GridPosition, ref Vector2 VectorPosition, 
                                            ref Point pointDir, TileMap tileMap, int tileSize, 
                                            float moveSpeed)
        {
            if(!hasTarget ) //get a target
            {
                randDir = Directions[randomPoint.Next(Directions.Length)]; //get a random direction from the Directions list
                x = randDir.X;
                y = randDir.Y;
                hasTarget = true;
            }

            startTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (startTimer > 0)
            {
                pointDir = new Point(x, y);
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (IsWalkableGhostTile(GridPosition.X + x, GridPosition.Y + y, tileMap))
                {
                    Vector2 targetPos = PointToVectorConvert(tileSize, GridPosition + pointDir);
                    Vector2 toTarget = targetPos - VectorPosition;

                    float distThisFrame = moveSpeed * deltaTime;// amount to move
                    if (toTarget.Length() <= distThisFrame)
                    {
                        hasTarget = false; //when at center of tile/location, new direction
                        VectorPosition = targetPos;
                        GridPosition += pointDir;
                    }
                    else //keep moving
                    {
                        VectorPosition += Vector2.Normalize(toTarget) * distThisFrame;
                    }
                }
                else//not walkable get new direction
                {
                    hasTarget = false;
                }
            }
            else //move out of "gate" area
            {
                MoveToDirectGhostTiles(gameTime, 12, 15, ref GridPosition, 
                    ref VectorPosition, ref pointDir, tileMap, moveSpeed, tileSize);
            }


        }
    }
}
