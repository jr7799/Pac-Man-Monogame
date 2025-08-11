using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rossi_PAC_MAN_Midterm.Environment
{

    public static class Globals
    {
        public static float spriteScale {  get; set; }

        public static float windowScale { get; set; }
        public static GraphicsDeviceManager Graphics { get; set; }
        public static Point windowSize { get; set; }

        public static ContentManager g_Content { get; set; }
        public static SpriteFont g_font { get; set; }
        public static Texture2D tileMapImage { get; set; }

        public static int G_PlayerScore { get; set; }
        public static int TotalEggs { get; set; }
    }
}
