using Rossi_PAC_MAN_Midterm.States.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.States
{
    public abstract class BaseFSM
    {
        private BaseGameState mState;
        protected abstract void SwitchState(BaseGameState newState);
        //action
    }
}
