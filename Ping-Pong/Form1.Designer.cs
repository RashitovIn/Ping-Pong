
namespace Ping_Pong
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.platformRightPB = new System.Windows.Forms.PictureBox();
            this.panel = new System.Windows.Forms.Panel();
            this.ballPB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.platformRightPB)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ballPB)).BeginInit();
            this.SuspendLayout();
            // 
            // platformRightPB
            // 
            this.platformRightPB.BackColor = System.Drawing.SystemColors.Desktop;
            this.platformRightPB.Location = new System.Drawing.Point(585, 139);
            this.platformRightPB.Name = "platformRightPB";
            this.platformRightPB.Size = new System.Drawing.Size(26, 147);
            this.platformRightPB.TabIndex = 0;
            this.platformRightPB.TabStop = false;
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.Transparent;
            this.panel.Controls.Add(this.ballPB);
            this.panel.Controls.Add(this.platformRightPB);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(800, 531);
            this.panel.TabIndex = 1;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            // 
            // ballPB
            // 
            this.ballPB.BackColor = System.Drawing.Color.Transparent;
            this.ballPB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ballPB.BackgroundImage")));
            this.ballPB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ballPB.Location = new System.Drawing.Point(133, 139);
            this.ballPB.Name = "ballPB";
            this.ballPB.Size = new System.Drawing.Size(50, 50);
            this.ballPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ballPB.TabIndex = 1;
            this.ballPB.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 531);
            this.Controls.Add(this.panel);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.Color.White;
            this.Click += new System.EventHandler(this.Form1_Click);
            ((System.ComponentModel.ISupportInitialize)(this.platformRightPB)).EndInit();
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ballPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox platformRightPB;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.PictureBox ballPB;
    }
}

