using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Ping_Pong
{
    class Ball
    {
        public Rectangle Body;
        public Bitmap Sprite { get; protected set; }

        public int Speed { get; private protected set; }
        public int Dx { get; private protected set; } = -1;
        public int Dy { get; private protected set; }
        public int PosX { get; private protected set; }
        public int PosY { get; private protected set; }

        public Ball(Bitmap sprite, int dx, int dy)
        {
            Dx = dx;
            Dy = dy;
            PosX = 50;
            PosY = 150;
            Speed = 1;
            var appDir = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            var fullPath = Path.Combine(appDir, @"src");
            Sprite = sprite;
            //MessageBox.Show(Directory.GetCurrentDirectory());
            Body = new Rectangle(PosX, PosY, 30, 30);

        }

        public void Update()
        {
            Body.X += Dx * Speed;
            Body.Y += Dy * Speed;
        }
    }
}
