using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.Objects.Ghost_States.Base
{
    public abstract class BaseGhostState
    {
        public abstract void PerformAction(GameTime gameTime);
    }
}
