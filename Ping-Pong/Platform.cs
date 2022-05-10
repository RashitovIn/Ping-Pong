using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ping_Pong
{
    public class ComputerPlatform
    {
        public Rectangle Body;

        private protected int areaWidth;
        private protected int areaHeight;

        public int Dy { get; private protected set; }
        public int SpeedY { get; private protected set; }
        public string Type { get; private protected set; }

        public ComputerPlatform(int speed, int width, int height, int areaWidth, int areaHeight)
        {
            Type = "computer";
            SpeedY = speed;
            Body = new Rectangle(20, 50, width, height);
            this.areaWidth = areaWidth;
            this.areaHeight = areaHeight;
        }

        public virtual void CheckPos()
        {
            if (Body.Bottom >= areaHeight)
                Body.Y = areaHeight - Body.Height;
            else if (Body.Top <= 0)
                Body.Y = 0;
        }

        public void Update(Ball ball)
        {
            int center = Body.Y + Body.Height / 2;
            if (ball.SpeedX < 0 && ball.Body.X <= areaWidth * 2 / 3)
            {
                if (Body.Bottom + 0 <= ball.Body.Bottom || Body.Top - 0 >= ball.Body.Top)
                {
                    if (ball.Body.Y < center)
                        Dy = -1;
                    else if (ball.Body.Y > center)
                        Dy = 1;
                }
            }
            else
            {
                if (center + 30 < areaHeight / 2)
                    Dy = 1;
                else if (center - 30 > areaHeight / 2)
                    Dy = -1;
            }

            Body.Y += Dy * SpeedY;
            Dy = 0;
            CheckPos();
        }
    }
    public class PlayerPlatform : ComputerPlatform
    {
        public event PlayerCollision CheckCollision;
        public Rectangle ShadowRect;

        private Point Position { get; set; }
        public int SpeedX { get; private protected set; }

        public PlayerPlatform(int speed, int width, int height, int areaWidth, int areaHeight) : base(speed, width, height, areaWidth, areaHeight)
        {
            Type = "player";
            Dy = 0;
            SpeedX = speed;
            SpeedY = speed;
            Body = new Rectangle(areaWidth - (20 + width), 50, width, height);
            ShadowRect = new Rectangle();
            this.areaWidth = areaWidth;
            this.areaHeight = areaHeight;
        }

        public override void CheckPos()
        {
            if (Body.Bottom >= areaHeight)
            {
                Body.Y = areaHeight - Body.Height;
            }
            else if (Body.Top <= 0)
            {
                Body.Y = 0;
            }

            if (Body.Right >= areaWidth)
            {
                Body.X = areaWidth - Body.Width;
            }
            else if (Body.Left <= areaWidth / 2 + 15)
            {
                Body.X = areaWidth / 2 + 15;
            }

            ShadowRect.Location = Body.Location;
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Location.X < areaWidth && 0 < e.Location.Y && e.Location.Y < areaHeight)
            {
                int x = e.Location.X - Body.Width / 2;
                int y = e.Location.Y - Body.Height / 2;

                Position = new Point(x, y);

                if (areaWidth / 2 + 30 < e.Location.X)
                {
                    ShadowRect.Size = new Size(Math.Min(60, Math.Abs(ShadowRect.Right - Body.Left)), Body.Height);

                    SpeedX = Position.X - Body.Location.X;
                    SpeedY = Position.Y - Body.Location.Y;

                }
                Body.Location = Position;
                
                CheckCollision(this);
            }
        }

        public void Update()
        {
            SpeedX += -Math.Sign(SpeedX);
            CheckPos();
            //g.DrawString(Convert.ToString(SpeedX), new Font("Roboto", 21), Brushes.White, new Point(5, 5));
        }
    }
}
