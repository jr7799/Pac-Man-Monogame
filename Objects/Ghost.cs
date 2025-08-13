using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rossi_PAC_MAN_Midterm.Anims;
using Rossi_PAC_MAN_Midterm.Environment;
using Rossi_PAC_MAN_Midterm.FSM;
using Rossi_PAC_MAN_Midterm.Objects.Base;
using System;
using System.Collections.Generic;
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
        private bool isMoving = false;

        private bool _facingLeft;
        private readonly int tileSize;
        private readonly float moveTileScale;
        private readonly float moveSpeed;

        private Point startPoint;

        private float timer = 2.5f;
        private float timerStartVal = 2.5f;


        private float startTimer = 5f;

        public bool flee = false;
        
        public Ghost(SpriteManager spriteManager, Point startPoint, int tileSize, float moveTileScale, float moveSpeed, TileMap tileMap, Player player, string name)
        {
            this.startPoint = startPoint;
            this.spriteManager = spriteManager;
            this.tileSize = tileSize;
            this.moveTileScale = moveTileScale;
            this.moveSpeed = moveSpeed;
            this.tileMap = tileMap;
            this.player = player;

            layer = 1;
            isActive = true;
            GridPosition = startPoint;
            VectorPosition = PointToVectorConvert(tileSize, GridPosition);

            fsm = new GhostFSM();
            fsm.Initialize(name);
            fsm.SwitchState("START_STATE");
        }
        public override void Update(GameTime gameTime)
        {
            fsm.Update(gameTime);

            startTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (isActive && player.isActive && startTimer <= 0)
            {
                fsm.SwitchState("CHASE_STATE");
            }
            if (isActive && !player.isActive && startTimer <= 0)
            {
                fsm.SwitchState("ROAM_STATE");
            }

            if (!isActive) //reset ghost
            {
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(timer <= 0)
                {
                    GridPosition = startPoint;
                    VectorPosition = PointToVectorConvert(tileSize, GridPosition);
                    timer = timerStartVal;
                    isActive = true;
                    fsm.SwitchState("START_STATE");
                }
            }
        }
        public override Rectangle BoxCollider
        {
            get
            {
                if (spriteManager != null)
                {
                    return new Rectangle((int)VectorPosition.X, (int)VectorPosition.Y, texture.Width, texture.Height);
                }
                return Rectangle.Empty;
            }
        }
        public override void Render(SpriteBatch spriteBatch)
        {
            SpriteEffects flipEffect = _facingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            if (isActive) spriteManager.Draw(spriteBatch, new Vector2(VectorPosition.X - spriteManager._currentAnimation._frameWidth, VectorPosition.Y - spriteManager._currentAnimation._frameHeight), flipEffect);
        }
        bool IsWalkableGhostTile(int tx, int ty)
        {
            if (tx < 0 || ty < 0 || tx >= TileMap.MAP_COLS || ty >= TileMap.MAP_ROWS)
                return false;

            Tile tempTile = null;
            foreach (var tile in tileMap.tiles)
            {
                if (tile.GridPosition == new Point(tx, ty))
                {
                    tempTile = tile;
                }
                else
                    continue;
            }
            return tempTile.IsGhostWalkable;
        }
        bool IsWalkableTile(int tx, int ty)
        {
            if (tx < 0 || ty < 0 || tx >= TileMap.MAP_COLS || ty >= TileMap.MAP_ROWS)
                return false;

            Tile tempTile = null;
            foreach (var tile in tileMap.tiles)
            {
                if (tile.GridPosition == new Point(tx, ty))
                {
                    tempTile = tile;
                }
                else
                    continue;
            }
            return tempTile.IsWalkable;
        }
    }
}
