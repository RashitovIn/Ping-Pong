using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Ping_Pong
{
    public class Ball
    {
        public event Goal GoalEvent;
        public Rectangle Body;
        private int areaWidth;
        private int areaHeight;
        Random random;

        public Bitmap Sprite { get; protected set; }
        public int SpeedX { get; private protected set; }
        public int SpeedY { get; private protected set; }

        private void LoadBitmap()
        {
            var appDir = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            var fullPath = Path.Combine(appDir, @"src");
            Sprite = new Bitmap(Image.FromFile(Path.Combine(fullPath, "shar.bmp")));
            Sprite.MakeTransparent(Color.FromArgb(255, 255, 255));
        }

        public Ball(PictureBox mainArea)
        {
            SpeedX = -5;
            SpeedY = 5;
            LoadBitmap();
            areaWidth = mainArea.Width;
            areaHeight = mainArea.Height;
            random = new Random();
            Body = new Rectangle(areaWidth / 2, random.Next(0, areaHeight - Body.Height - 5), 30, 30);
        }

        public static int AbsMax(int f, int s)
        {
            if (Math.Abs(f) >= Math.Abs(s))
            {
                return f;
            }
            else
            {
                return s;
            }
        }

        public void MoveCollision(Platform platform)
        {
            if (Body.X >= areaWidth / 2 && Body.IntersectsWith(platform.Body))
            {
                if (SpeedX < 0)
                {
                    Body.X = platform.Body.Left - Body.Width - 3;
                    if (Math.Abs(platform.SpeedX) >= 45)
                        SpeedX = -45;
                    else
                        SpeedX = -Math.Max(Math.Abs(platform.SpeedX), 45);
                }
                else if (SpeedX > 0)
                {
                    if (Body.Left <= platform.Body.Left && Body.Right >= platform.Body.Left && Body.Right <= platform.Body.Right)
                    {
                        Body.X = platform.Body.Left - Body.Width - 3;
                        //SpeedX = -AbsMax(platform.SpeedX, SpeedX);
                        if (Math.Abs(platform.SpeedX) >= 45)
                            SpeedX = -45;
                        else
                            SpeedX = -Math.Max(Math.Abs(platform.SpeedX), 45);
                        //SpeedX = 1;
                    }
                    else if (Body.Left <= platform.Body.Right && platform.SpeedX > 0)
                    {
                        Body.X = platform.Body.Right + 10;
                        SpeedX = Math.Abs(AbsMax(platform.SpeedX, 25));
                        //SpeedX = 1;
                    }
                }
            }
        }

        public void Collision(Platform[] platforms)
        {
            foreach (Platform item in platforms)
            {
                if (Body.IntersectsWith(item.Body))
                {
                    /*if (item.Type == "player" && Body.Right >= item.Body.Left + 10 && SpeedX < 0)
                    {
                        Body.X = item.Body.Left - Body.Width;
                        if (SpeedX > 0)
                            SpeedX += 25;
                        else
                            SpeedX -= 25;
                        SpeedX *= -1;
                    }*/
                    if (item.Type == "computer" && Body.Left <= item.Body.Right - 10)
                    {
                        if (SpeedX >= 0)
                            SpeedX += 25;
                        else
                            SpeedX -= 25;
                        SpeedX *= -1;
                        Body.X = item.Body.Right;
                    }
                }
            }

        }

        public void Update(Platform[] platforms)
        {
            if (Body.Bottom >= areaHeight)
            {
                //Body.Bottom = areaHeight;
                SpeedY *= -1;
            }
            else if (Body.Top <= 0)
            {
                SpeedY *= -1;
            }

            if (Body.Left <= 0)
            {
                GoalEvent('c');
                Body.X = areaWidth / 2;
                Body.Y = random.Next(0, areaHeight - Body.Height - 5);
                SpeedX = 5;
            }
            else if (Body.Right >= areaWidth)
            {
                GoalEvent('p');
                Body.X = areaWidth / 2;
                Body.Y = random.Next(0, areaHeight - Body.Height - 5);
                SpeedX = -5;
            }

            Body.X += SpeedX;
            Body.Y += SpeedY;

            if (Math.Abs(SpeedX) > 10)
            {
                if (SpeedX <= 0)
                    SpeedX += 3;
                else
                    SpeedX -= 1;

            }

            Collision(platforms);
            MoveCollision(platforms[0]);
        }
    }
}
