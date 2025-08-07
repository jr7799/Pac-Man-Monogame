using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.FSM
{
    public abstract class BaseFSM<TState>
    {
        private TState mState;
        public TState M_STATE
        { 
            get { return mState; }
            set { mState = value; }
        }
        public abstract void SwitchState(TState newState);
    }
}
