using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rossi_PAC_MAN_Midterm.Anims;
using Rossi_PAC_MAN_Midterm.Environment;
using Rossi_PAC_MAN_Midterm.Objects;
using Rossi_PAC_MAN_Midterm.Objects.Base;
using Rossi_PAC_MAN_Midterm.Objects.Ghost_States;
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
        private Player player;
        private List<Ghost> ghosts = new();
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
            SpriteManager spriteManager = new SpriteManager();
            Texture2D playerIdle = content.Load<Texture2D>("playerRight");
            spriteManager.LoadAnimation("idle", playerIdle, 16, 16, 4, 0.1f);
            player = new Player(spriteManager, new Point(12, 28), 16, Globals.spriteScale, 125f, pacmanMap);
            AddGameObject(player);

            SpriteManager spriteManager1 = new SpriteManager();
            Texture2D ghost = content.Load<Texture2D>("blue");
            spriteManager1.LoadAnimation("idle", ghost, 13, 12, 1, 0.1f);
            Ghost winky = new Ghost(spriteManager1, new Point(9, 19), 16, 175f, pacmanMap, player, "winky");
            AddGameObject(winky); ghosts.Add(winky);

            SpriteManager spriteManager2 = new SpriteManager();
            Texture2D ghost1 = content.Load<Texture2D>("purp");
            spriteManager2.LoadAnimation("idle", ghost1, 13, 12, 1, 0.1f);
            Ghost dobby = new Ghost(spriteManager2, new Point(9, 17), 16, 175f, pacmanMap, player, "dobby");
            AddGameObject(dobby); ghosts.Add(dobby);

            SpriteManager spriteManager3 = new SpriteManager();
            Texture2D ghost2 = content.Load<Texture2D>("yel");
            spriteManager3.LoadAnimation("idle", ghost2, 13, 12, 1, 0.1f);
            Ghost hokey = new Ghost(spriteManager3, new Point(15, 17), 16, 175f, pacmanMap, player, "hokey");
            AddGameObject(hokey); ghosts.Add(hokey);

            SpriteManager spriteManager4 = new SpriteManager();
            Texture2D ghost3 = content.Load<Texture2D>("red");
            spriteManager4.LoadAnimation("idle", ghost3, 13, 12, 1, 0.1f);
            Ghost kreacher = new Ghost(spriteManager4, new Point(15, 19), 16, 175f, pacmanMap, player, "kreacher");           
            AddGameObject(kreacher); ghosts.Add(kreacher);
            
            foreach(var g in ghosts)
            {
                g.fsm.SwitchState("START_STATE");
            }
        }
        public override void HandleInput(GameTime gameTime)
        {

        }
        public override void Update(GameTime gameTime)
        {
            //updates and checks
            player.Update(gameTime);
            foreach (var g in ghosts)
            {
                g.Update(gameTime);
            }
            CheckCollisionsPlayerToEggs(player, pacmanMap.eggs, ghosts);
            CheckCollisionsPlayerToGhosts(player, ghosts);

            //input
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                InvokeGameSignals("QUIT_GAME");
            }
            //game state switches
            if (player.playerLives <= 0)
            {
               InvokeStateSwitched("LOSE_STATE");
               player.playerLives = 3;
            }
            if (pacmanMap.eggs.Count <= 0)
            {
                InvokeStateSwitched("WIN_STATE");
            }
        }
        public override void RenderStrings(SpriteBatch spriteBatch)
        {
            string stateName = $"SCORE: {Globals.G_PlayerScore}"; //needs player score
            spriteBatch.DrawString(Globals.g_font, stateName, new Vector2(Globals.Graphics.PreferredBackBufferWidth / 2 - Globals.g_font.MeasureString(stateName).X/2, 0), Color.White);
        }
        private void CheckCollisionsPlayerToEggs(Player Player, List<Egg> eggs, List<Ghost> Ghosts)
        {
            for (int i = eggs.Count - 1; i >= 0; i--)
            {
                var egg = eggs[i];
                if (!egg.isActive) continue;

                if (Player.BoxCollider.Intersects(egg.BoxCollider))
                {
                    if (egg.isPower)
                    {
                        foreach (var g in Ghosts)
                        {
                            g.flee = true;
                            g.fleeTimer = 10f;
                        }
                        egg.isActive = false;
                        Globals.G_PlayerScore += 50;
                    }
                    else
                    {
                        egg.isActive = false;
                        Globals.G_PlayerScore += 10;
                    }

                    eggs.RemoveAt(i);
                }
            }
        }
        private void CheckCollisionsPlayerToGhosts(Player Player, List<Ghost> Ghosts)
        {
            foreach (var ghost in Ghosts)
            {
                if (!ghost.isActive) continue;
                if (Player.BoxCollider.Intersects(ghost.BoxCollider))
                {
                    if (ghost.flee == true)
                    {
                        ghost.isActive = false;
                        Globals.G_PlayerScore += 100;
                    }
                    else
                    {
                        player.playerLives--;
                        player.isActive = false;
                        break;
                    }
                }
            }
        }
    }
}
