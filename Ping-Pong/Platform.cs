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
    class Platform
    {
        public Rectangle Body;
        public PictureBox pb;

        public int Dy { get; private protected set; }
        public int PosX { get; private protected set; }
        public int PosY { get; private protected set; }
        public int Speed { get; private protected set; }

        public Platform(PictureBox pb)
        {
            this.pb = pb;
            Dy = 0;
            Speed = 5;
            Body = new Rectangle(pb.Location, pb.Size);
        }

        public void Update()
        {
            Body.Y += Dy * Speed;
            pb.Location = Body.Location;
        }
    }
}
