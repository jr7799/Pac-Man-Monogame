using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rossi_PAC_MAN_Midterm.Environment;
using Rossi_PAC_MAN_Midterm.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.FSM.Base
{
    public abstract class BaseFSM<TState>
    {
        private Dictionary<string , TState> mStates = new();
        public Dictionary<string, TState> M_STATES //FOR ADDING TO DICTIONARY AND GETTING AND SETTING DICTIONARY ITSELF
        {
            get { return mStates; }
            set { mStates = value; }
        }
        public TState this[string key] //get and set specific keys within the dictionary
        {
            get { return mStates[key]; }
            set { mStates[key] = value; }
        }

        private TState mState;
        public TState M_STATE //get and set the current state being used by the FSM
        { 
            get { return mState; }
            set { mState = value; }
        }
        public abstract void Initialize(string name);
        public abstract void AddState(string key, TState newStateToAdd);
        public abstract void SwitchState(string newKey); //switch state
        public abstract void Update(GameTime gameTime, Point point, Vector2 vectorPos, Point pointDir, TileMap tileMap = null, 
                                    int tileSize = 0, float moveSpeed = 0f, Player player = null);

    }
}
