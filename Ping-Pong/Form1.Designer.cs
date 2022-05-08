
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
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.mainArea = new System.Windows.Forms.PictureBox();
            this.compGoals = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.playerGoals = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mainArea)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainArea
            // 
            this.mainArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainArea.Location = new System.Drawing.Point(3, 30);
            this.mainArea.Name = "mainArea";
            this.mainArea.Size = new System.Drawing.Size(928, 527);
            this.mainArea.TabIndex = 0;
            this.mainArea.TabStop = false;
            // 
            // compGoals
            // 
            this.compGoals.AutoSize = true;
            this.compGoals.Location = new System.Drawing.Point(3, 3);
            this.compGoals.Name = "compGoals";
            this.compGoals.Size = new System.Drawing.Size(38, 15);
            this.compGoals.TabIndex = 0;
            this.compGoals.Text = "label1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mainArea, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.821429F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.17857F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(934, 560);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.playerGoals);
            this.panel1.Controls.Add(this.compGoals);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(928, 21);
            this.panel1.TabIndex = 0;
            // 
            // playerGoals
            // 
            this.playerGoals.AutoSize = true;
            this.playerGoals.Location = new System.Drawing.Point(887, 3);
            this.playerGoals.Name = "playerGoals";
            this.playerGoals.Size = new System.Drawing.Size(38, 15);
            this.playerGoals.TabIndex = 1;
            this.playerGoals.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 560);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Ping-Pong";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Transparent;
            ((System.ComponentModel.ISupportInitialize)(this.mainArea)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox mainArea;
        private System.Windows.Forms.Label compGoals;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label playerGoals;
    }
}

