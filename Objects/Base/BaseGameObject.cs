using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.Objects.Base
{
    public class BaseGameObject
    {
        public Texture2D texture;
        public Point gridPosition;
        public Vector2 vectorPosition;
        public Rectangle rect;
        public int index;

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
            spriteBatch.Draw(texture, VectorPosition, Color.White);
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
    }
}
