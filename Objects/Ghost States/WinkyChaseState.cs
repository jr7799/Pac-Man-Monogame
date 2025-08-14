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
        public override void PerformAction(GameTime gameTime, Point GridPosition, Vector2 VectorPosition, TileMap tileMap)
        {
            ///Debug.WriteLine("PERFORMING WINKY CHASE ACTION");
        }
    }
}
