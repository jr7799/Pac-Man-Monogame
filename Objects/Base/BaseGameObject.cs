using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rossi_PAC_MAN_Midterm.Anims;
using Rossi_PAC_MAN_Midterm.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.Objects.Base
{
    public abstract class BaseGameObject
    {
        public SpriteManager spriteManager;

        public Texture2D texture;
        public Point gridPosition;
        public Vector2 vectorPosition;
        public Rectangle rect;
        public int index;
        public bool isActive;
        public string tag;
        public int layer;
        public Point GridPosition
        {
            get { return gridPosition; }
            set { gridPosition = value; }
        }
        public Vector2 VectorPosition
        {
            get { return vectorPosition; }
            set { vectorPosition = value; }
        }
        public virtual void Render(SpriteBatch spriteBatch)
        {
            if(isActive) spriteBatch.Draw(texture, VectorPosition, Color.White);
        }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
        public virtual Rectangle BoxCollider
        {
            get
            {
                if (texture != null)
                {
                    return new Rectangle((int)VectorPosition.X, (int)VectorPosition.Y, texture.Width, texture.Height);
                }
                return Rectangle.Empty;
            }
        }
        public abstract void CheckCollisions(BaseGameObject other);
        public virtual Vector2 PointToVectorConvert(int tileSize, Point GridPosition)
        {
            return new Vector2(
                (GridPosition.X * (tileSize * Globals.spriteScale) + (tileSize * Globals.spriteScale)/2),
                 GridPosition.Y * (tileSize * Globals.spriteScale) + (tileSize * Globals.spriteScale)/2);
        }
    }
}
