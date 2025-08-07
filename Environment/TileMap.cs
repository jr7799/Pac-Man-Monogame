using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rossi_PAC_MAN_Midterm.Environment
{
    public static class TileMap
    {
        public static int tileSize = 16;
        public static Rectangle floor1Rect = new Rectangle(9 * tileSize, 0 * tileSize, tileSize, tileSize);   //light spaced cobble
        public static Rectangle floor2Rect = new Rectangle(10 * tileSize, 0 * tileSize, tileSize, tileSize);  //light heavy cobble
        public static Rectangle floor3Rect = new Rectangle(9 * tileSize, 1 * tileSize, tileSize, tileSize);   //dark light cobble
        public static Rectangle floor4Rect = new Rectangle(10 * tileSize, 1 * tileSize, tileSize, tileSize);  //dark heavy cobble
        public static Rectangle wall1Rect = new Rectangle(1 * tileSize, 4 * tileSize, tileSize, tileSize);    //dirty dark brick wall
        public static Rectangle wall2Rect = new Rectangle(2 * tileSize, 4 * tileSize, tileSize, tileSize);    //dirty cracked dark brick wall
        public static Rectangle cornerRect = new Rectangle(5 * tileSize, 1 * tileSize, tileSize, tileSize);   //Large Heavy cobble
        public static Rectangle tube1Rect = new Rectangle(1 * tileSize, 11 * tileSize, tileSize, tileSize);   //red gem floor "teleport"
        public static Rectangle tube2Rect = new Rectangle(2 * tileSize, 11 * tileSize, tileSize, tileSize);   //blue gem floor "teleport"

        //floor tiles = walkable
        public static Tile newFloor1 = new Tile(TileType.Floor, Globals.tileMapImage, TileMap.floor1Rect);
        public static Tile newFloor2 = new Tile(TileType.Floor, Globals.tileMapImage, TileMap.floor2Rect);
        public static Tile newFloor3 = new Tile(TileType.Floor, Globals.tileMapImage, TileMap.floor3Rect);
        public static Tile newFloor4 = new Tile(TileType.Floor, Globals.tileMapImage, TileMap.floor4Rect);
        //wall tiles = !walkable
        public static Tile newWall1 = new Tile(TileType.Wall, Globals.tileMapImage, TileMap.wall1Rect);
        public static Tile newWall2 = new Tile(TileType.Wall, Globals.tileMapImage, TileMap.wall2Rect);
        //powerup floor = Walkable and has item to eat "ghosts" on them
        public static Tile newPowerup = new Tile(TileType.Floor, Globals.tileMapImage, TileMap.cornerRect);
        //tube/teleport tiles
        public static Tile newTube1 = new Tile(TileType.Floor, Globals.tileMapImage, TileMap.tube1Rect);
        public static Tile newTube2 = new Tile(TileType.Floor, Globals.tileMapImage, TileMap.tube2Rect);
    }

}
