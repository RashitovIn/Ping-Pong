using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Ping_Pong
{
    public delegate void Goal(char platformType);
    public delegate void MoveCollision(Platform platform);

    public partial class Form1 : Form
    {
        Ball ball;
        Platform player;
        ComputerPlatform computer;
        Graphics g;
        Platform[] platforms = new Platform[2];

        public Form1()
        {
            InitializeComponent();

            compGoals.Text = "0";
            playerGoals.Text = "0";
            mainArea.Image = new Bitmap(mainArea.Width, mainArea.Height);
            g = Graphics.FromImage(mainArea.Image);

            ball = new Ball(mainArea);
            ball.GoalEvent += Goal;

            player = new Platform(mainArea, label1);
            player.CheckCollision += ball.MoveCollision;

            computer = new ComputerPlatform(mainArea, label1);
            
            KeyUp += new KeyEventHandler(PlatformControlUp);
            KeyDown += new KeyEventHandler(PlatformControlDown);
            mainArea.MouseMove += new MouseEventHandler(player.MouseMove);
            timer.Tick += new EventHandler(Update);
            timer.Interval = 10;
            timer.Enabled = true;
        }

        private void Update(object sender, EventArgs e)
        {
            g.Clear(Color.FromArgb(0, Color.White));
            
            player.Update();
            computer.Update(ball);

            platforms[0] = player;
            platforms[1] = computer;
            ball.Update(platforms);
            label1.Text = Convert.ToString(ball.SpeedX) + ' ' + Convert.ToString(ball.SpeedY);
            g.DrawImage(ball.Sprite, ball.Body);
            g.FillRectangle(Brushes.Black, player.Body);
            g.FillRectangle(Brushes.Black, computer.Body);

            mainArea.Refresh();
        }

        private void PlatformControlUp(object sender, KeyEventArgs e)
        {
            //player.Dy = 0;
            //player.Dx = 0;
        }

        private void PlatformControlDown(object sender, KeyEventArgs e)
        {
            /*switch (e.KeyCode)
            {
                case Keys.Up:
                    player.Dy = -1;
                    break;
                case Keys.Down:
                    player.Dy = 1;
                    break;
                case Keys.Left:
                    player.Dx = -1;
                    break;
                case Keys.Right:
                    player.Dx = 1;
                    break;
            }*/
        }

        private void Goal(char platformType)
        {
            if (platformType == 'p')
            {
                compGoals.Text = Convert.ToString(Convert.ToInt32(compGoals.Text) + 1);
                //System.Threading.Thread.Sleep(200);
            }
            else
            {
                playerGoals.Text = Convert.ToString(Convert.ToInt32(playerGoals.Text) + 1);
                //System.Threading.Thread.Sleep(200);
            }
        }
    }
}
