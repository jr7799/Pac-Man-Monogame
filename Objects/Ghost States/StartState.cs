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
        public override void PerformAction(GameTime gameTime, Point GridPosition, Vector2 VectorPosition, TileMap tileMap)
        {
            Debug.WriteLine("PERFORMING START ACTION");
            pointDir = new Point(0, -1);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (IsWalkableTile(GridPosition.X, GridPosition.Y - 1, tileMap)) //continuous check while moving
            {
                Vector2 targetPos = PointToVectorConvert(tileSize, GridPosition + pointDir); //target position
                Vector2 toTarget = targetPos - VectorPosition;                               //target minus current = distance to move

                float distThisFrame = moveSpeed * deltaTime;                                 //pixels to move                               
                if (toTarget.Length() <= distThisFrame)                                      //dist to move <= pixels to move                            
                {
                    VectorPosition = targetPos; //at target
                    GridPosition += pointDir;   //increase grid position to check next tile after this
                }
                else
                {
                    VectorPosition += Vector2.Normalize(toTarget) * distThisFrame;  //not at target, normalized vector 0 or 1 times dist to move
                }
        }
    }
}
