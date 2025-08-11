using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rossi_PAC_MAN_Midterm.Environment;
using Rossi_PAC_MAN_Midterm.Objects.Base;
using Rossi_PAC_MAN_Midterm.Objects.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.Objects
{
    public class Egg : BaseGameObject, Collectable
    {
        public Egg(Texture2D texture, Point spawnPoint, bool isActive = true)
        {
            tag = "nom";
            this.isActive = isActive;
            this.texture = texture;
            GridPosition = spawnPoint;
            VectorPosition = new Vector2((GridPosition.X * (TileMap.tileSize * Globals.spriteScale)) + 12, GridPosition.Y * (TileMap.tileSize * Globals.spriteScale) + 12);
            layer = 1;
        }
        public override void CheckCollisions(BaseGameObject other)
        {
            
        }
    }
}
