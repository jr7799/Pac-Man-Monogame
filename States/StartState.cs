using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rossi_PAC_MAN_Midterm.States.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.States
{
    public class StartState : BaseGameState
    {
        public override void HandleInput(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            throw new NotImplementedException();
        }

        public override void RenderStrings(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void UnloadContent(ContentManager contentManager)
        {
            contentManager.Unload();
        }
    }
}
