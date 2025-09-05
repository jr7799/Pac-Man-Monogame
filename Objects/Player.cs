using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rossi_PAC_MAN_Midterm.Anims;
using Rossi_PAC_MAN_Midterm.Environment;
using Rossi_PAC_MAN_Midterm.Objects.Base;
using Rossi_PAC_MAN_Midterm.States.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.Objects
{
    public enum PlayerDirections { None, Up, Down, Left, Right }
    public class Player : BaseGameObject
    {
        //health data
        public int playerLives = 3;

        //moving data
        private bool _facingLeft;
        private readonly int tileSize;
        private readonly float moveTileScale;
        private readonly float moveSpeed;

        public TileMap tileMap;
        private Point pointDir;
        private Point startPoint;
        private PlayerDirections currentDir = PlayerDirections.None;
        private PlayerDirections previousDir = PlayerDirections.None;

        private float timer = 2.5f;
        private float timerStartVal = 2.5f;
        public Player(SpriteManager spriteManager, Point startPoint, int tileSize, float moveTileScale, float moveSpeed, TileMap tileMap, bool isActive = true)
        {
            this.startPoint = startPoint;
            this.spriteManager = spriteManager;
            this.tileSize = tileSize;
            this.moveTileScale = moveTileScale;
            this.moveSpeed = moveSpeed;
            this.tileMap = tileMap;
            layer = 1;
            this.isActive = isActive;
            playerLives = 3;

            //start pos'
            GridPosition = startPoint;
            VectorPosition = PointToVectorConvert(tileSize, GridPosition);
        }
        public void MoveUp(GameTime gameTime)
        {
            pointDir = new Point(0, -1);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (IsWalkableTile(GridPosition.X, GridPosition.Y - 1)) //continuous check while moving
            {
                Vector2 targetPos = PointToVectorConvert(tileSize, GridPosition + pointDir); //target position
                Vector2 toTarget = targetPos - VectorPosition;                               //target minus current = distance to move

                float distThisFrame = moveSpeed * deltaTime;                                 //pixels to move                               
                if (toTarget.Length() <= distThisFrame)                                      //dist to move <= pixels to move                            
                {
                    VectorPosition = targetPos; //at target
                    GridPosition += pointDir;   //increase grid position to check next tile after this
                }
                else
                {
                    VectorPosition += Vector2.Normalize(toTarget) * distThisFrame;  //not at target, normalized vector 0 or 1 times dist to move
                }
            }
            else //continue in direction previously moving in if wall blocks new direction tried
            {
                currentDir = previousDir;
            }
        }
        public void MoveDown(GameTime gameTime)
        {
            pointDir = new Point(0, 1);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (IsWalkableTile(GridPosition.X, GridPosition.Y + 1))
            {
                Vector2 targetPos = PointToVectorConvert(tileSize, GridPosition + pointDir); 
                Vector2 toTarget = targetPos - VectorPosition;                               

                float distThisFrame = moveSpeed * deltaTime;                                                          
                if (toTarget.Length() <= distThisFrame)                                                                
                {
                    VectorPosition = targetPos; 
                    GridPosition += pointDir;   
                }
                else
                {
                    VectorPosition += Vector2.Normalize(toTarget) * distThisFrame;  
                }
            }
            else 
            {
                currentDir = previousDir;
            }
        }
        public void MoveLeft(GameTime gameTime)
        {
            pointDir = new Point(-1, 0);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (IsWalkableTile(GridPosition.X - 1, GridPosition.Y))
            {
                Vector2 targetPos = PointToVectorConvert(tileSize, GridPosition + pointDir); 
                Vector2 toTarget = targetPos - VectorPosition;                              

                float distThisFrame = moveSpeed * deltaTime;                                                             
                if (toTarget.Length() <= distThisFrame)                                                                
                {
                    VectorPosition = targetPos; 
                    GridPosition += pointDir;  
                }
                else
                {
                    VectorPosition += Vector2.Normalize(toTarget) * distThisFrame;
                }
                _facingLeft = true;
            }
            else
            {
                currentDir = previousDir;
            }
        }
        public void MoveRight(GameTime gameTime)
        {
            pointDir = new Point(1, 0);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (IsWalkableTile(GridPosition.X + 1, GridPosition.Y))
            {
                Vector2 targetPos = PointToVectorConvert(tileSize, GridPosition + pointDir); 
                Vector2 toTarget = targetPos - VectorPosition;                               

                float distThisFrame = moveSpeed * deltaTime;                                                   
                if (toTarget.Length() <= distThisFrame)                                                         
                {                                                                                      
                    VectorPosition = targetPos;
                    GridPosition += pointDir;   
                }
                else
                {
                    VectorPosition += Vector2.Normalize(toTarget) * distThisFrame;  
                }
                _facingLeft = false;
            }
            else 
            {
                currentDir = previousDir;
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (isActive)
            {
                var kb = Keyboard.GetState();
                if (kb.IsKeyDown(Keys.W) && IsWalkableTile(GridPosition.X, GridPosition.Y - 1) || kb.IsKeyDown(Keys.Up) && IsWalkableTile(GridPosition.X, GridPosition.Y - 1)) //check walkable on key press
                {
                    previousDir = currentDir;
                    currentDir = PlayerDirections.Up;
                }
                else if (kb.IsKeyDown(Keys.S) && IsWalkableTile(GridPosition.X, GridPosition.Y + 1) || kb.IsKeyDown(Keys.Down) && IsWalkableTile(GridPosition.X, GridPosition.Y + 1))
                {
                    previousDir = currentDir;
                    currentDir = PlayerDirections.Down;
                }
                else if (kb.IsKeyDown(Keys.A) && IsWalkableTile(GridPosition.X - 1, GridPosition.Y) || kb.IsKeyDown(Keys.Left) && IsWalkableTile(GridPosition.X - 1, GridPosition.Y))
                {
                    previousDir = currentDir;
                    currentDir = PlayerDirections.Left;
                }
                else if (kb.IsKeyDown(Keys.D) && IsWalkableTile(GridPosition.X + 1, GridPosition.Y) || kb.IsKeyDown(Keys.Right) && IsWalkableTile(GridPosition.X + 1, GridPosition.Y))
                {
                    previousDir = currentDir;
                    currentDir = PlayerDirections.Right;
                }
            }
            if(isActive)
            {
                switch (currentDir)
                {
                    case PlayerDirections.Up:
                        MoveUp(gameTime);
                        break;
                    case PlayerDirections.Down:
                        MoveDown(gameTime);
                        break;
                    case PlayerDirections.Left:
                        MoveLeft(gameTime);
                        break;
                    case PlayerDirections.Right:
                        MoveRight(gameTime);
                        break;
                }
            }
            if (!isActive) //reset ghost
            {
                GridPosition = startPoint;
                VectorPosition = PointToVectorConvert(tileSize, GridPosition);
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer <= 0)
                {
                    timer = timerStartVal;
                    isActive = true;
                }
            }
        }
        public override void Render(SpriteBatch spriteBatch)
        {
            SpriteEffects flipEffect = _facingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            if(isActive) spriteManager.Draw(spriteBatch, new Vector2(VectorPosition.X - spriteManager._currentAnimation._frameWidth, VectorPosition.Y - spriteManager._currentAnimation._frameHeight), Color.White, flipEffect);
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
        bool IsWalkableTile(int tx, int ty)
        {
            if (tx < 0 || ty < 0 || tx >= TileMap.MAP_COLS || ty >= TileMap.MAP_ROWS)
                return false;

            Tile tempTile = null;
            foreach(var tile in tileMap.tiles)
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
