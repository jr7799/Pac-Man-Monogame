using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Rossi_PAC_MAN_Midterm.Environment;
using Rossi_PAC_MAN_Midterm.FSM.Base;
using Rossi_PAC_MAN_Midterm.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rossi_PAC_MAN_Midterm.States.Base;
using Rossi_PAC_MAN_Midterm.Objects.Ghost_States.Base;
using Rossi_PAC_MAN_Midterm.Objects.Ghost_States;
using Rossi_PAC_MAN_Midterm.Objects;

namespace Rossi_PAC_MAN_Midterm.FSM
{
    public class GhostFSM : BaseFSM<BaseGhostState>
    {

        public override void Initialize(string name)
        {
            AddState("START_STATE", new StartState());
            AddState("ROAM_STATE", new RoamState());
            AddState("FLEE_STATE", new FleeState());

            switch (name) //each ghost has different chase
            {
                case "winky":
                    AddState("CHASE_STATE", new WinkyChaseState());
                    break;
                case "dobby":
                    AddState("CHASE_STATE", new DobbyChaseState());
                    break;
                case "kreacher":
                    AddState("CHASE_STATE", new KreacherChaseState());
                    break;
                case "hokey":
                    AddState("CHASE_STATE", new HokeyChaseState());
                    break;
            }
        }
        public override void AddState(string key, BaseGhostState newStateToAdd)
        {
            M_STATES.Add(key, newStateToAdd);
        }
        public override void SwitchState(string newKey)
        {
            M_STATE = this[newKey];
        }
        public override void Update(GameTime gameTime, Point point, Vector2 vectorPos,
                                    Point pointDir, TileMap tileMap, int tileSize, float moveSpeed, Player player) ///not used, part of base without refs
        {
            //M_STATE.PerformAction(player, gameTime, point, vectorPos, pointDir, tileMap, tileSize, moveSpeed);
        }
        public  void MyUpdate(GameTime gameTime, ref Point point, ref Vector2 vectorPos,
                                ref Point pointDir, TileMap tileMap, int tileSize, float moveSpeed, Player player) //using cause of refs, not base
        {
            M_STATE.PerformAction(player, gameTime, ref point, ref vectorPos, ref pointDir, tileMap, tileSize, moveSpeed);
        }
    }
}
