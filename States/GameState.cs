using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rossi_PAC_MAN_Midterm.Anims;
using Rossi_PAC_MAN_Midterm.Environment;
using Rossi_PAC_MAN_Midterm.Objects;
using Rossi_PAC_MAN_Midterm.Objects.Base;
using Rossi_PAC_MAN_Midterm.States.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.States
{
    public class GameState : BaseGameState
    {
        private PacManGridMap pacmanMap = new PacManGridMap();

        private SpriteManager spriteManager;
        private Player player;
        public override void Initialize()
        {

        }

        public override void LoadContent(ContentManager content)
        {
            Globals.tileMapImage = Globals.g_Content.Load<Texture2D>("Tile Map");
            Globals.g_font = Globals.g_Content.Load<SpriteFont>("Pixel");

            //map gen
            pacmanMap.GenerateMap();
            foreach (var tile in pacmanMap.tiles)
            {             
                AddGameObject(tile);
            }
            foreach(var egg in pacmanMap.eggs)
            {
                AddGameObject(egg);
            }

            //player gen
            spriteManager = new SpriteManager();
            Texture2D playerIdle = content.Load<Texture2D>("playerRight");
            spriteManager.LoadAnimation("idle", playerIdle, 16, 16, 4, 0.1f);
            player = new Player(spriteManager, new Point(12, 28), 16, Globals.spriteScale, 100f, pacmanMap);
            AddGameObject(player);
        }
        public override void HandleInput(GameTime gameTime)
        {

        }
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                InvokeGameSignals("QUIT_GAME");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.N))
            {
                InvokeStateSwitched("LOSE_STATE");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.B))
            {
                InvokeStateSwitched("WIN_STATE");
            }

            player.Update(gameTime);
            CheckCollisionsPlayerToEggs(player, pacmanMap.eggs);
        }
        public override void RenderStrings(SpriteBatch spriteBatch)
        {
            string stateName = $"SCORE: {Globals.G_PlayerScore}"; //needs player score
            spriteBatch.DrawString(Globals.g_font, stateName, new Vector2(Globals.Graphics.PreferredBackBufferWidth / 2 - Globals.g_font.MeasureString(stateName).X/2, 0), Color.White);
        }
        private void CheckCollisionsPlayerToEggs(Player Player, List<BaseGameObject> eggs)
        {
            foreach (var egg in eggs)
            {
                if (!egg.isActive) continue;
                if (Player.BoxCollider.Intersects(egg.BoxCollider))
                {
                    egg.isActive = false;
                    Globals.G_PlayerScore += 10;
                }
            }
        }
        //private void CheckCollisionsPlayerToGhosts(Player Player, List<BaseGameObject> Ghosts)
        //{
        //    foreach (var ghost in Ghosts)
        //    {
        //        if (!ghost.isActive) continue;
        //        if (Player.BoxCollider.Intersects(ghost.BoxCollider))
        //        {
        //            if(ghost.state == GhostStates.Fleeing)
        //            {
        //                ghost.isActive = false;
        //                Globals.G_PlayerScore += 50;
        //            }
        //            else
        //            {
        //                player.isActive = false;
        //            }
        //        }
        //    }
        //}
    }
}
