using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ping_Pong
{
    class Platform
    {
        public Rectangle Body;
        protected int areaWidth;
        protected int areaHeight;
        protected int width = 20;
        protected int height = 100;

        public int Dy { get; set; }
        public int Dx { get; set; }
        public Point OldPoint { get; set; }
        public int SpeedX { get; private protected set; }
        public int SpeedY { get; private protected set; }
        public string Type { get; private protected set; }

        public Platform(PictureBox mainArea)
        {
            Type = "player";
            Dy = 0;
            Dx = 0;
            SpeedX = 35;
            SpeedY = 35;
            Body = new Rectangle(mainArea.Width - (20 + width), 50, width, height);
            OldPoint = new Point(mainArea.Width - (20 + width), 50);
            areaWidth = mainArea.Width;
            areaHeight = mainArea.Height;
        }

        public virtual void CheckPos()
        {
            if (Body.Bottom >= areaHeight)
            {
                Body.Y = areaHeight - height;
            }
            else if (Body.Top <= 0)
            {
                Body.Y = 0;
            }

            if (Body.Right >= areaWidth)
            {
                Body.X = areaWidth - width;
            }
            else if (Body.Left <= areaWidth / 2)
            {
                Body.X = areaWidth / 2;
            }
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            Body.Location = e.Location;
            SpeedX = (OldPoint.X - Body.Location.X) / 30;
            SpeedY = (OldPoint.Y - Body.Location.Y) / 30;
            OldPoint = Body.Location;

        }

        public void Update()
        {
            //Body.Y += Dy * Speed;
            //Body.X += Dx * Speed;

            CheckPos();
        }

        /*public void Update(Ball ball)
        {
            int center = Body.Y + height / 2;
            if (ball.Dx > 0 && ball.Body.X >= areaWidth * 2 / 3)
            {
                if (Body.Bottom + 0 <= ball.Body.Bottom || Body.Top - 0 >= ball.Body.Top)
                {
                    if (ball.Body.Y < center)
                    {
                        Dy = -1;
                    }
                    else if (ball.Body.Y > center)
                    {
                        Dy = 1;
                    }
                }
            }
            else
            {
                if (center + 30 < areaHeight / 2)
                {
                    Dy = 1;
                }
                else if (center - 30 > areaHeight / 2)
                {
                    Dy = -1;
                }
            }

            Body.Y += Dy * SpeedY;
            Dy = 0;

            CheckPos();
        }*/
    }

    class ComputerPlatform : Platform
    {
        public ComputerPlatform(PictureBox mainArea) : base(mainArea)
        {
            Type = "computer";
            SpeedY = 20;
            Body = new Rectangle(20, 50, width, height);
        }

        /*public bool Predict()
        {
            int center = Body.Y + width / 2;
            if 
        }*/

        public override void CheckPos()
        {
            if (Body.Bottom >= areaHeight)
            {
                Body.Y = areaHeight - height;
            }
            else if (Body.Top <= 0)
            {
                Body.Y = 0;
            }
        }

        public void Update(Ball ball)
        {
            int center = Body.Y + height / 2;
            if (ball.Dx < 0 && ball.Body.X <= areaWidth * 2 / 3)
            {
                if (Body.Bottom + 0 <= ball.Body.Bottom || Body.Top - 0 >= ball.Body.Top)
                {
                    if (ball.Body.Y < center)
                    {
                        Dy = -1;
                    }
                    else if (ball.Body.Y > center)
                    {
                        Dy = 1;
                    }
                }
            }
            else
            {
                if (center + 30 < areaHeight / 2)
                {
                    Dy = 1;
                }
                else if (center - 30 > areaHeight / 2)
                {
                    Dy = -1;
                }
            }

            Body.Y += Dy * SpeedY;
            Dy = 0;

            CheckPos();
        }
    }
}
