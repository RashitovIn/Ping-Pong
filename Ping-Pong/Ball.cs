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
        public Rectangle ShadowRect;
        private int areaWidth;
        private int areaHeight;
        private Random random;
        private double angle;

        public Bitmap Sprite { get; private set; }
        public int InitSpeed { get; private set; }
        public int SpeedX { get; private set; }
        public int SpeedY { get; private set; }

        public Ball(int areaWidth, int areaHeight, int speed)
        {
            random = new Random();
            InitSpeed = speed;
            this.areaWidth = areaWidth;
            this.areaHeight = areaHeight;
            Body = new Rectangle(areaWidth / 2 - 13, random.Next(areaHeight - 29), 25, 25);

            StartPosition();
            LoadBitmap();
        }

        private void LoadBitmap()
        {
            var appDir = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            var fullPath = Path.Combine(appDir, @"src");
            Sprite = new Bitmap(Image.FromFile(Path.Combine(fullPath, "shar.bmp")));
            Sprite.MakeTransparent(Color.FromArgb(255, 255, 255));
        }

        private double GetAngle()
        {
            int i = random.Next(0, 2);
            if (i == 0)
                return random.Next(30, 80) * Math.PI / 180;
            else
                return random.Next(130, 160) * Math.PI / 180;
        }

        private void StartPosition()
        {
            angle = GetAngle();
            Body.X = areaWidth / 2;
            Body.Y = random.Next(0, areaHeight - Body.Height - 5);

            SpeedX = Convert.ToInt32(Math.Round(InitSpeed * Math.Cos(angle)));
            SpeedY = Convert.ToInt32(Math.Round(InitSpeed * Math.Sin(angle)));
        }

        public void PlayerCollision(PlayerPlatform platform)
        {
            int Dx = Math.Sign(SpeedX);
            int Dy = Math.Sign(SpeedY);
            SpeedX = Math.Abs(SpeedX);
            SpeedY = Math.Abs(SpeedY);

            /*if (Dx > 0 && platform.SpeedX > 0 && (Body.IntersectsWith(platform.Body) || ShadowRect.IntersectsWith(platform.ShadowRect)))
            {
                Body.X = platform.Body.Right + 20;
                SpeedX = Math.Max(Math.Abs(platform.SpeedX), 25);

            }*/
           
                if (Body.X >= areaWidth / 2 - 40 && Body.IntersectsWith(platform.Body))
                {
                    if (Dx < 0)
                    {
                        Body.X = platform.Body.Left - Body.Width - 3;
                        if (Math.Abs(platform.SpeedX) >= 35)
                            SpeedX = 35;
                        else
                            SpeedX = Math.Max(Math.Abs(platform.SpeedX), 15);
                    }
                    else if (Dx > 0)
                    {
                        if (Body.Left <= platform.Body.Left && Body.Right >= platform.Body.Left && Body.Right <= platform.Body.Right)
                        {
                            Body.X = platform.Body.Left - Body.Width - 3;
                            if (Math.Abs(platform.SpeedX) >= 45)
                                SpeedX = 45;
                            else
                                SpeedX = Math.Max(Math.Abs(platform.SpeedX), 12);
                            Dx = -1;
                            SpeedY = Convert.ToInt32(Math.Round(InitSpeed * Math.Sin(angle + random.Next(1) / 100)));
                        }
                        /*else if (Body.Left <= platform.Body.Right && platform.SpeedX > 0 && Body.Left >= platform.Body.Left)
                        {
                            Body.X = platform.Body.Right + 10;
                            SpeedX = Math.Max(platform.SpeedX, 25);
                        }*/
                    }
                }
                else if (Body.X >= areaWidth / 2 - 40 && Dx > 0 && !Body.IntersectsWith(platform.Body))
                {
                    if (ShadowRect.IntersectsWith(platform.Body) || ShadowRect.IntersectsWith(platform.ShadowRect))
                    {
                        Body.X = platform.Body.Left - Body.Width - 3;
                        if (Math.Abs(platform.SpeedX) >= 45)
                            SpeedX = 45;
                        else
                            SpeedX = Math.Max(Math.Abs(platform.SpeedX), 12);
                        Dx = -1;
                        SpeedY = Convert.ToInt32(Math.Round(InitSpeed * Math.Sin(angle + random.Next(1) / 100)));
                    }
                }
            

            SpeedX *= Dx;
            SpeedY *= Dy;
        }

        private void ComputerCollision(Rectangle computerRect)
        {
            if (Body.IntersectsWith(computerRect) && Body.Left <= computerRect.Right - 10)
            {
                SpeedX += Math.Sign(SpeedX) * 15;
                SpeedX *= -1;
                Body.X = computerRect.Right;
            }
        }

        public void Update(Rectangle computerRect, PlayerPlatform player)
        {
            if (Body.Bottom >= areaHeight)
                SpeedY *= -1;
            else if (Body.Top <= 0)
                SpeedY *= -1;

            if (Body.Left <= 0)
            {
                GoalEvent('c');
                StartPosition();
            }
            else if (Body.Right >= areaWidth)
            {
                GoalEvent('p');
                StartPosition();
            }

            ShadowRect = Body;
            ShadowRect.X -= 2 * SpeedX;
            ShadowRect.Y -= SpeedY;
            ShadowRect.Y = ShadowRect.Y - Body.Height / 2 + Math.Abs(ShadowRect.Y - Body.Y);

            ShadowRect.Size = new Size(Math.Max(20, Math.Abs(ShadowRect.X - Body.X)), Math.Max(20, Math.Abs(ShadowRect.Y - Body.Y)));

            Body.X += SpeedX;
            Body.Y += SpeedY;

            if (Math.Abs(SpeedX) > InitSpeed)
            {
                SpeedX += -Math.Sign(SpeedX);
            }
            else if (Math.Abs(SpeedX) < InitSpeed)
            {
                SpeedX += Math.Sign(SpeedX) * InitSpeed;
            }

            //lb.Text = Convert.ToString(Math.Acos(SpeedY / Math.Sqrt(Math.Pow(SpeedX, 2) + Math.Pow(SpeedY, 2))) / Math.PI * 180);
            //lb.Text = Convert.ToString(SpeedX);

            ComputerCollision(computerRect);
            PlayerCollision(player);
        }
    }
}
