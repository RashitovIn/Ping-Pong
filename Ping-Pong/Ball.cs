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
        public event Goal GoalEvent;
        public Rectangle Body;
        private int areaWidth;
        private int areaHeight;
        Random random;

        public Bitmap Sprite { get; protected set; }
        public int SpeedX { get; private protected set; }
        public int SpeedY { get; private protected set; }
        public int Dx { get; private protected set; } = -1;
        public int Dy { get; private protected set; }

        private void LoadBitmap()
        {
            var appDir = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            var fullPath = Path.Combine(appDir, @"src");
            Sprite = new Bitmap(Image.FromFile(Path.Combine(fullPath, "shar.bmp")));
            Sprite.MakeTransparent(Color.FromArgb(255, 255, 255));
        }

        public Ball(int dx, int dy, PictureBox mainArea)
        {
            Dx = dx;
            Dy = dy;
            SpeedX = 15;
            SpeedY = 15;
            LoadBitmap();
            areaWidth = mainArea.Width;
            areaHeight = mainArea.Height;
            random = new Random();
            Body = new Rectangle(areaWidth / 2, random.Next(0, areaHeight - Body.Height - 5), 30, 30);
        }


        public void Update(Platform[] platforms)
        {
            if (Body.Bottom >= areaHeight)
            {
                //Body.Bottom = areaHeight;
                Dy *= -1;
            }
            else if (Body.Top <= 0)
            {
                Dy *= -1;
            }

            if (Body.Left <= 0)
            {
                GoalEvent('c');
                Body.X = areaWidth / 2;
                Body.Y = random.Next(0, areaHeight - Body.Height - 5);
                Dx *= -1;
            }
            else if (Body.Right >= areaWidth)
            {
                GoalEvent('p');
                Body.X = areaWidth / 2;
                Body.Y = random.Next(0, areaHeight - Body.Height - 5);
                Dx *= -1;
            }

            Body.X += Dx * SpeedX;
            Body.Y += Dy * SpeedY;

            foreach (Platform item in platforms)
            {
                if (Body.IntersectsWith(item.Body))
                {
                    if (item.Type == "player") //&& Body.Right >= item.Body.Left + 10)
                    {
                        if (Dx > 0 && Body.Right >= item.Body.Left)
                        {
                            Body.X = item.Body.Left - Body.Width;
                            Dx *= -1;
                        }
                        else if (Dx < 0 && Body.Left <= item.Body.Right)
                        {
                            Body.X = item.Body.Right + 5;
                            Dx = 1;
                        }
                    }

                    if (item.Type == "computer" && Body.Left <= item.Body.Right - 10)
                    {
                        Dx *= -1;
                        Body.X = item.Body.Right;
                    }
                }
            } 
        }
    }
}
