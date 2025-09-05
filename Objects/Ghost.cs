using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rossi_PAC_MAN_Midterm.Anims;
using Rossi_PAC_MAN_Midterm.Environment;
using Rossi_PAC_MAN_Midterm.FSM;
using Rossi_PAC_MAN_Midterm.Objects.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.Objects
{
    public class Ghost : BaseGameObject
    {
        public GhostFSM fsm;
        Player player;

        public TileMap tileMap;

        private bool _facingLeft;
        private int tileSize;
        private float moveSpeed;

        private Point startPoint;
        private Point pointDir;
        private Point gridPos;
        private Vector2 vecPos;

        private float timer = 2.5f;
        private float timerStartVal = 2.5f;


        public float startTimer = 5f;

        public bool flee = false;
        public float fleeTimer = 10f;

        Color color = Color.White;
        public Ghost(SpriteManager spriteManager, Point startPoint, int tileSize, float moveSpeed, TileMap tileMap, Player player, string name)
        {
            this.startPoint = startPoint;
            this.spriteManager = spriteManager;
            this.tileSize = tileSize;
            this.moveSpeed = moveSpeed;
            this.tileMap = tileMap;
            this.player = player;

            layer = 1;
            isActive = true;
            GridPosition = startPoint;
            VectorPosition = PointToVectorConvert(tileSize, GridPosition);
            
            gridPos = GridPosition;
            vecPos = VectorPosition;

            fsm = new GhostFSM();
            fsm.Initialize(name);
        }
        public override void Update(GameTime gameTime)
        {
            if (isActive)
                fsm.MyUpdate(gameTime, ref gridPos, ref vecPos, ref pointDir, tileMap, tileSize, moveSpeed, player);

            startTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if(flee)
            {
                color = Color.MediumAquamarine;
            }
            else
            {
                color = Color.White;
            }

            if (isActive && player.isActive && flee)
            {
                fsm.SwitchState("FLEE_STATE");
                fleeTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds; //FLEEING TIMER
                if (fleeTimer <= 0)
                {
                    flee = false;
                }
            }
            if (isActive && player.isActive && startTimer <= 0 && !flee || GridPosition == new Point(12, 15)) //IF PLAYER ALIVE CHASE
            {
                fsm.SwitchState("CHASE_STATE");
            }
            if (isActive && !player.isActive && startTimer <= 0 && !flee) //IF PLAYER DEAD ROAM FOR A BIT
            {
                fsm.SwitchState("ROAM_STATE");
            }

            if (!isActive) //reset ghost
            {
                flee = false;
                gridPos = startPoint;
                vecPos = PointToVectorConvert(tileSize, GridPosition);
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer <= 0)
                {
                    isActive = true;
                    timer = timerStartVal;
                    startTimer = 5f;
                    //fsm.M_STATE.startTimer = 5f;
                    fsm.SwitchState("START_STATE");
                }
            }

            GridPosition = gridPos;
            VectorPosition = vecPos;
        }
        public override Rectangle BoxCollider
        {
            get
            {
                if (spriteManager != null)
                {
                    return new Rectangle((int)VectorPosition.X, (int)VectorPosition.Y, spriteManager._currentAnimation._frameWidth, spriteManager._currentAnimation._frameHeight);
                }
                return Rectangle.Empty;
            }
        }
        public override void Render(SpriteBatch spriteBatch)
        {
            SpriteEffects flipEffect = _facingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            if (isActive) spriteManager.Draw(spriteBatch, new Vector2(VectorPosition.X - spriteManager._currentAnimation._frameWidth, VectorPosition.Y - spriteManager._currentAnimation._frameHeight), color, flipEffect);
        }      
    }
}
