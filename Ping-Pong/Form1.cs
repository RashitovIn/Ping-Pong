using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Ping_Pong
{
    public partial class Form1 : Form
    {
        Ball ball;
        Platform platform;

        public Form1()
        {
            InitializeComponent();
            Bitmap Sprite = new Bitmap(Image.FromFile(Path.Combine(@"C:\Users\wispm\source\repos\Ping-Pong\Ping-Pong\src", "shar.bmp")));
            Sprite.MakeTransparent(Color.FromArgb(255, 255, 255));
            ball = new Ball(Sprite, 1, 0);
            platform = new Platform(platformRightPB);

            timer.Tick += new EventHandler(Update);
            timer.Interval = 10;
            timer.Enabled = true;
        }

        private void Update(object sender, EventArgs e)
        {
            Invalidate();
            ball.Update();
            platform.Update();
            
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(ball.Sprite, ball.Body);
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
