using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rossi_PAC_MAN_Midterm.Objects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.States.Base
{
    public abstract class BaseGameState
    {
        private readonly List<BaseGameObject> gameObjects = new List<BaseGameObject>();
        public abstract void LoadContent(ContentManager contentManager);
        public abstract void UnloadContent(ContentManager contentManager);
        public abstract void HandleInput(GameTime gameTime);

        public virtual void Update(GameTime gameTime) { }
        protected void AddGameObject(BaseGameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }
        public abstract void RenderStrings(SpriteBatch spriteBatch);
        public virtual void Render(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in gameObjects.OrderBy(a => a.index))
            {
                gameObject.Render(spriteBatch);
                RenderStrings(spriteBatch);
            }
        }
    }
}
