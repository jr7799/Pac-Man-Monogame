using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Rossi_PAC_MAN_Midterm.Environment;
using Rossi_PAC_MAN_Midterm.Objects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.Objects
{
    public enum TileType
    {
        Floor,
        Wall,
        PowerupFloor,
        Tube,
        FloorGate,
        Gate
    }

    public class Tile : BaseGameObject
    {
        public TileType Type { get; set; }
        public bool IsWalkable => Type == TileType.Floor || Type == TileType.Tube || Type == TileType.PowerupFloor;
        public bool IsGhostWalkable => Type == TileType.Floor || Type == TileType.Tube || Type == TileType.PowerupFloor || Type == TileType.Gate || Type == TileType.FloorGate;

        public Rectangle textureSourceRectangle;

        //tell tile to draw collectible or attack trigger collectible
        public bool ShouldDrawCollectable => Type == TileType.Floor;
        public bool ShouldDrawAttackCollectable => Type == TileType.PowerupFloor;


        public Tile(TileType type, Texture2D texture, Rectangle rectangle)
        {
            Type = type;
            this.texture = texture;
            textureSourceRectangle = rectangle;
            isActive = true;
        }
        public override void Render(SpriteBatch spriteBatch)
        {
            if (isActive) spriteBatch.Draw(texture, vectorPosition, textureSourceRectangle, (Type == TileType.Wall) ? Color.Violet : Color.White, 0, Vector2.Zero, 2.7f * Globals.windowScale, SpriteEffects.None, (Type == TileType.Wall) ? 1 : 0);
        }
        public override Rectangle BoxCollider
        {
            get
            {
                return Rectangle.Empty;
            }
        }
    }
}
