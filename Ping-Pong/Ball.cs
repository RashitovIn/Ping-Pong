using System;
using System.Drawing;
using System.IO;

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

            StartPosition('c');
            LoadBitmap();
        }

        private void LoadBitmap()
        {
            var appDir = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            var fullPath = Path.Combine(appDir, @"src");
            Sprite = new Bitmap(Image.FromFile(Path.Combine(fullPath, "shar.bmp")));
            Sprite.MakeTransparent(Color.FromArgb(0, 0, 0));
        }

        private double GetAngle()
        {
            int i = random.Next(0, 2);
            if (i == 0)
                return random.Next(30, 80) * Math.PI / 180;
            else
                return random.Next(130, 160) * Math.PI / 180;
        }

        private void StartPosition(char side)
        {
            angle = GetAngle();
            Body.X = areaWidth / 2;
            Body.Y = random.Next(0, areaHeight - Body.Height - 5);

            SpeedX = Convert.ToInt32(Math.Round(InitSpeed * Math.Cos(angle)));
            SpeedY = Convert.ToInt32(Math.Round(InitSpeed * Math.Sin(angle)));

            if (side == 'c')
                SpeedX = -Math.Abs(SpeedX);
            else
                SpeedX = Math.Abs(SpeedX);
        }

        private void AngleCorrection(ComputerPlatform platform)
        {
            int newSpeedY = InitSpeed;
            Rectangle platformRect = platform.Body;

            if (Body.Bottom >= platformRect.Top - 15 && Body.Bottom < platformRect.Top + platformRect.Height / 3 - 5)
            {
                newSpeedY = Convert.ToInt32(InitSpeed * Math.Round(Math.Sin(random.Next(60, 130) * Math.PI / 180)));
            }
            else if (Body.Top <= platformRect.Bottom + 15 && platformRect.Top > platformRect.Top + platformRect.Height * 2 / 3 + 5)
            {
                newSpeedY = Convert.ToInt32(InitSpeed * Math.Round(Math.Sin(random.Next(60, 130) * Math.PI / 180)));
            }
            else if (Body.Top >= platformRect.Top + platformRect.Height / 3 - 10 && Body.Bottom <= platformRect.Top + platformRect.Height * 2 / 3 + 10)
            {
                newSpeedY = Convert.ToInt32(InitSpeed * Math.Round(Math.Sin(random.Next(5, 10) * Math.PI / 180)));
            }

            SpeedY = newSpeedY;
        }

        public void PlayerCollision(PlayerPlatform platform)
        {
            int Dx = Math.Sign(SpeedX);
            SpeedX = Math.Abs(SpeedX);

            // Основная коллизия мяча и каретки игрока
            if (Body.X >= areaWidth / 2 - 40 && Body.IntersectsWith(platform.Body))
            {
                if (Dx < 0) //Мяч уже отбит, второе столкновение просто увеличит скорость полета мяча
                {
                    Body.X = platform.Body.Left - Body.Width - 3;
                    if (Math.Abs(platform.SpeedX) >= 35)
                        SpeedX = 35;
                    else
                        SpeedX = Math.Max(Math.Abs(platform.SpeedX), 15);
                }
                else if (Dx > 0)
                {
                    if (platform.SpeedX > 0)
                    {
                        Body.X = platform.Body.Right + 10;
                        SpeedX += Math.Abs(platform.SpeedX);
                    }
                    else if (Body.Left <= platform.Body.Left && Body.Right >= platform.Body.Left && Body.Right <= platform.Body.Right)
                    {
                        Body.X = platform.Body.Left - Body.Width - 3;
                        if (Math.Abs(platform.SpeedX) >= 45)
                            SpeedX = 45;
                        else
                            SpeedX = Math.Max(Math.Abs(platform.SpeedX), 12);
                        Dx = -1;

                        AngleCorrection(platform);
                    }
                }
            }
            // Дополнительная коллизия для отлова мяча на большой скорости
            else if (Body.X >= areaWidth / 2 - 40 && Dx > 0 && !Body.IntersectsWith(platform.Body) && platform.SpeedX <= 0)
            {
                if (ShadowRect.IntersectsWith(platform.Body) || ShadowRect.IntersectsWith(platform.ShadowRect))
                {
                    Body.X = platform.Body.Left - Body.Width - 3;
                    if (Math.Abs(platform.SpeedX) >= 45)
                        SpeedX = 45;
                    else
                        SpeedX = Math.Max(Math.Abs(platform.SpeedX), 12);
                    Dx = -1;
                    
                    AngleCorrection(platform);
                }
            }

            SpeedX *= Dx;
        }

        private void ComputerCollision(ComputerPlatform computer)
        {
            Rectangle computerRect = computer.Body;
            if (Body.IntersectsWith(computerRect) && Body.Left <= computerRect.Right - 10)
            {
                SpeedX += Math.Sign(SpeedX) * 13;
                SpeedX *= -1;
                Body.X = computerRect.Right;
                AngleCorrection(computer);
            }
        }

        public void Update(ComputerPlatform computer, PlayerPlatform player)
        {
            if (Body.Bottom >= areaHeight)
                SpeedY *= -1;
            else if (Body.Top <= 0)
                SpeedY *= -1;

            if (Body.Left <= 0)
            {
                GoalEvent('c');
                StartPosition('c');
            }
            else if (Body.Right >= areaWidth)
            {
                GoalEvent('p');
                StartPosition('p');
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

            ComputerCollision(computer);
            PlayerCollision(player);
        }
    }
}
