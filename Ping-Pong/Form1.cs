using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Ping_Pong
{
    public delegate void Goal(char platformType);
    public delegate void PlayerCollision(PlayerPlatform player);

    public partial class Form1 : Form
    {
        Ball ball;
        ComputerPlatform computer;
        PlayerPlatform player;
        Graphics g;

        public int pGoals;
        public int cGoals;

        public Form1()
        {
            InitializeComponent();

            pGoals = 0;
            cGoals = 0;
        }

        private void GameController()
        {
            if (pGoals >= 5 || cGoals >= 5)
            {
                string text = "";
                Color color = Color.Black;
                int smech = 0;
                if (pGoals >= 5)
                {
                    text = "Вы победили!";
                    color = Color.Green;
                    smech = 20;
                }
                else if (cGoals >= 5)
                {
                    text = "Вы проиграли!";
                    color = Color.Red;
                    smech = 8;
                }

                timer.Tick -= new EventHandler(Update);
                timer.Enabled = false;
                timer.Stop();
                mainArea.Enabled = false;
                mainArea.Visible = false;
                panel1.Enabled = true;
                panel1.Visible = true;
                label1.Text = text;
                label1.ForeColor = color;
                label1.Location = new Point(mainArea.Width / 2 - label1.Width * 3 / 2 - smech, label1.Location.Y);
            }
        }

        private void Start(int compSpeed, int playerWidth, int playerHeight, int compWidth, int comprHeight, int ballSpeed)
        {
            mainArea.Enabled = true;
            mainArea.Visible = true;
            mainArea.Dock = DockStyle.Fill;
            mainArea.Image = new Bitmap(mainArea.Width, mainArea.Height);
            timer.Start();
            g = Graphics.FromImage(mainArea.Image);

            pGoals = 0;
            cGoals = 0;

            ball = new Ball(mainArea.Width, mainArea.Height, ballSpeed);
            ball.GoalEvent += Goal;

            computer = new ComputerPlatform(compSpeed, compWidth, comprHeight, mainArea.Width, mainArea.Height);

            player = new PlayerPlatform(15, playerWidth, playerHeight, mainArea.Width, mainArea.Height);
            player.CheckCollision += ball.PlayerCollision;

            mainArea.MouseMove += new MouseEventHandler(player.MouseMove);
            timer.Tick += new EventHandler(Update);
            timer.Interval = 10;
            timer.Enabled = true;
            Thread.Sleep(300);
        }

        private void Drawing()
        {
            g.Clear(Color.FromArgb(0, Color.White));
            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 244, 67, 54)), new Rectangle(0, 0, mainArea.Width / 2, mainArea.Height));
            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 63, 81, 181)), new Rectangle(mainArea.Width / 2, 0, mainArea.Width / 2, mainArea.Height));

            g.DrawString(Convert.ToString(cGoals) + ' ' + Convert.ToString(pGoals), new Font("Roboto", 21), Brushes.White, new Point(mainArea.Width / 2 - 26, 5));
            g.DrawLine(new Pen(Color.White, 2), new Point(mainArea.Width / 2 - 1, 0), new Point(mainArea.Width / 2 - 1, mainArea.Height));
            
            g.DrawImage(ball.Sprite, ball.Body);
            //g.FillRectangle(Brushes.Red, ball.ShadowRect);
            
            //g.FillRectangle(Brushes.Red, player.ShadowRect);
            g.FillRectangle(Brushes.White, player.Body);
            g.FillRectangle(Brushes.White, computer.Body);

            mainArea.Refresh();
        }

        private void Update(object sender, EventArgs e)
        {
            player.Update();
            computer.Update(ball);
            
            ball.Update(computer.Body, player);

            Drawing();
            GameController();
        }

        private void Goal(char platformType)
        {
            if (platformType == 'p')
            {
                cGoals++;
                Thread.Sleep(100);
            }
            else
            {
                pGoals++;
                Thread.Sleep(100);
            }
        }

        private void easyBtn_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            panel1.Visible = false;
            Start(8, 20, 100, 20, 70, 7);
        }

        private void mediumBtn_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            panel1.Visible = false;
            Start(15, 20, 100, 20, 100, 10);
        }

        private void hardBtn_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            panel1.Visible = false;
            Start(20, 20, 100, 20, 100, 13);
        }
    }
}
