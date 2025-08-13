using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rossi_PAC_MAN_Midterm.Environment;
using Rossi_PAC_MAN_Midterm.FSM;
using Rossi_PAC_MAN_Midterm.FSM.Base;
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
        public GameFSM fsm;

        private readonly List<BaseGameObject> gameObjects = new List<BaseGameObject>();

        public event Action<string> OnStateSwitched;
        public virtual void InvokeStateSwitched(string key) => OnStateSwitched?.Invoke(key);


        public event Action<string> OnGameSignals; //ex: exit
        protected virtual void InvokeGameSignals(string key) => OnGameSignals?.Invoke(key);

        public abstract void Initialize();
        public abstract void LoadContent(ContentManager contentManager);
        public virtual void UnloadContent(ContentManager contentManager)
        {
            contentManager.Unload();
        }
        public abstract void HandleInput(GameTime gameTime);

        public virtual void Update(GameTime gameTime) { }
        protected void AddGameObject(BaseGameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }
        public void ClearGameObjects()
        {
            gameObjects.Clear();
        }
        public virtual void RenderStrings(SpriteBatch spriteBatch) { }
        public virtual void Render(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.Render(spriteBatch);
            }
            RenderStrings(spriteBatch);
        }

    }
}
