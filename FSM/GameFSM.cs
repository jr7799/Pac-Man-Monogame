using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rossi_PAC_MAN_Midterm.Environment;
using Rossi_PAC_MAN_Midterm.FSM.Base;
using Rossi_PAC_MAN_Midterm.States;
using Rossi_PAC_MAN_Midterm.States.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.FSM
{
    public class GameFSM : BaseFSM<BaseGameState>
    {
        public override void Initialize(string name)
        {
            AddState("LOAD_STATE", new LoadingState());
            AddState("GAME_STATE", new GameState());
            AddState("WIN_STATE", new WinState());
            AddState("LOSE_STATE", new LoseState());
        }
        public override void AddState(string key, BaseGameState newStateToAdd)
        {
            M_STATES.Add(key, newStateToAdd);
        }
        public override void SwitchState(string newKey)
        {
            if (M_STATE != null)
            {
                M_STATE.UnloadContent(Globals.g_Content);
            }
            M_STATE = this[newKey];
            M_STATE.fsm = this;
            M_STATE.Initialize();
            M_STATE.LoadContent(Globals.g_Content);
        }
        public override void Update(GameTime gameTime, Point point, Vector2 vectorPos, TileMap tileMap = null)
        {
            M_STATE.HandleInput(gameTime);
            M_STATE.Update(gameTime);
        }
        public void DrawRenders(SpriteBatch spriteBatch)
        {
            M_STATE.Render(spriteBatch);
            M_STATE.RenderStrings(spriteBatch);
        }
    }
}
