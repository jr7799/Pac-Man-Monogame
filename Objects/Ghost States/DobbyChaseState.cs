using Microsoft.Xna.Framework;
using Rossi_PAC_MAN_Midterm.Objects.Ghost_States.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.Objects.Ghost_States
{
    public class DobbyChaseState : BaseGhostState
    {
        public override void PerformAction(GameTime gameTime)
        {
            Debug.WriteLine("PERFORMING DOBBY CHASE ACTION");
        }
    }

}
