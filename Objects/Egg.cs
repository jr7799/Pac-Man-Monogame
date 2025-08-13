using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rossi_PAC_MAN_Midterm.Environment;
using Rossi_PAC_MAN_Midterm.Objects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.Objects
{
    public class Egg : BaseGameObject
    {
        public bool isPower;
        public Egg(Texture2D texture, Point spawnPoint, int tileSize, bool isPower = false, bool isActive = true)
        {
            this.isActive = isActive;
            this.texture = texture;
            this.isPower = isPower;
            GridPosition = spawnPoint;
            VectorPosition = PointToVectorConvert(tileSize, GridPosition);
            layer = 1;
        }
        public override void Update(GameTime gameTime)
        {
            
        }

    }
}
