namespace ShootingStar
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.HpBar = new System.Windows.Forms.ProgressBar();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.UpButton = new System.Windows.Forms.Button();
            this.DownButton = new System.Windows.Forms.Button();
            this.SelectDodge = new System.Windows.Forms.Button();
            this.SelectShoot = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.DiffBox = new System.Windows.Forms.TextBox();
            this.Rank_Panel = new System.Windows.Forms.Panel();
            this.Rank_Panel5 = new System.Windows.Forms.Panel();
            this.Rank_Label5 = new System.Windows.Forms.Label();
            this.Rank_Num5 = new System.Windows.Forms.Label();
            this.Rank_Panel4 = new System.Windows.Forms.Panel();
            this.Rank_Label4 = new System.Windows.Forms.Label();
            this.Rank_Num4 = new System.Windows.Forms.Label();
            this.Rank_Panel3 = new System.Windows.Forms.Panel();
            this.Rank_Label3 = new System.Windows.Forms.Label();
            this.Rank_Num3 = new System.Windows.Forms.Label();
            this.Rank_Panel2 = new System.Windows.Forms.Panel();
            this.Rank_Label2 = new System.Windows.Forms.Label();
            this.Rank_Num2 = new System.Windows.Forms.Label();
            this.Rank_Panel1 = new System.Windows.Forms.Panel();
            this.Rank_Label1 = new System.Windows.Forms.Label();
            this.Rank_Num1 = new System.Windows.Forms.Label();
            this.Rank_Label = new System.Windows.Forms.Label();
            this.Rank_Panel.SuspendLayout();
            this.Rank_Panel5.SuspendLayout();
            this.Rank_Panel4.SuspendLayout();
            this.Rank_Panel3.SuspendLayout();
            this.Rank_Panel2.SuspendLayout();
            this.Rank_Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Interval = 20;
            this.GameTimer.Tick += new System.EventHandler(this.GameTimer_Tick_1);
            // 
            // HpBar
            // 
            this.HpBar.Location = new System.Drawing.Point(12, 12);
            this.HpBar.Name = "HpBar";
            this.HpBar.Size = new System.Drawing.Size(141, 23);
            this.HpBar.TabIndex = 0;
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ScoreLabel.ForeColor = System.Drawing.Color.White;
            this.ScoreLabel.Location = new System.Drawing.Point(617, 12);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(62, 19);
            this.ScoreLabel.TabIndex = 1;
            this.ScoreLabel.Text = "Score";
            // 
            // UpButton
            // 
            this.UpButton.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.UpButton.Location = new System.Drawing.Point(206, 89);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(100, 50);
            this.UpButton.TabIndex = 2;
            this.UpButton.Text = "▲";
            this.UpButton.UseVisualStyleBackColor = true;
            this.UpButton.Click += new System.EventHandler(this.UpButton_Click);
            // 
            // DownButton
            // 
            this.DownButton.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DownButton.Location = new System.Drawing.Point(206, 197);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(100, 50);
            this.DownButton.TabIndex = 2;
            this.DownButton.Text = "▼";
            this.DownButton.UseVisualStyleBackColor = true;
            this.DownButton.Click += new System.EventHandler(this.DownButton_Click);
            // 
            // SelectDodge
            // 
            this.SelectDodge.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SelectDodge.Location = new System.Drawing.Point(372, 137);
            this.SelectDodge.Name = "SelectDodge";
            this.SelectDodge.Size = new System.Drawing.Size(100, 30);
            this.SelectDodge.TabIndex = 4;
            this.SelectDodge.Text = "Dodge";
            this.SelectDodge.UseVisualStyleBackColor = true;
            this.SelectDodge.Click += new System.EventHandler(this.SelectDodge_Click);
            // 
            // SelectShoot
            // 
            this.SelectShoot.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SelectShoot.Location = new System.Drawing.Point(372, 181);
            this.SelectShoot.Name = "SelectShoot";
            this.SelectShoot.Size = new System.Drawing.Size(100, 30);
            this.SelectShoot.TabIndex = 4;
            this.SelectShoot.Text = "Shooting";
            this.SelectShoot.UseVisualStyleBackColor = true;
            this.SelectShoot.Click += new System.EventHandler(this.SelectShoot_Click);
            // 
            // Start
            // 
            this.Start.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Start.Location = new System.Drawing.Point(521, 156);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(100, 30);
            this.Start.TabIndex = 4;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // DiffBox
            // 
            this.DiffBox.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DiffBox.Location = new System.Drawing.Point(206, 156);
            this.DiffBox.Name = "DiffBox";
            this.DiffBox.Size = new System.Drawing.Size(100, 29);
            this.DiffBox.TabIndex = 6;
            this.DiffBox.Text = "0";
            this.DiffBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Rank_Panel
            // 
            this.Rank_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Rank_Panel.Controls.Add(this.Rank_Panel5);
            this.Rank_Panel.Controls.Add(this.Rank_Panel4);
            this.Rank_Panel.Controls.Add(this.Rank_Panel3);
            this.Rank_Panel.Controls.Add(this.Rank_Panel2);
            this.Rank_Panel.Controls.Add(this.Rank_Panel1);
            this.Rank_Panel.Controls.Add(this.Rank_Label);
            this.Rank_Panel.Enabled = false;
            this.Rank_Panel.Location = new System.Drawing.Point(12, 41);
            this.Rank_Panel.Name = "Rank_Panel";
            this.Rank_Panel.Size = new System.Drawing.Size(760, 389);
            this.Rank_Panel.TabIndex = 7;
            this.Rank_Panel.Visible = false;
            this.Rank_Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Rank_Panel_Clicked);
            // 
            // Rank_Panel5
            // 
            this.Rank_Panel5.BackColor = System.Drawing.Color.White;
            this.Rank_Panel5.Controls.Add(this.Rank_Label5);
            this.Rank_Panel5.Controls.Add(this.Rank_Num5);
            this.Rank_Panel5.Enabled = false;
            this.Rank_Panel5.Location = new System.Drawing.Point(117, 326);
            this.Rank_Panel5.Name = "Rank_Panel5";
            this.Rank_Panel5.Size = new System.Drawing.Size(550, 50);
            this.Rank_Panel5.TabIndex = 3;
            this.Rank_Panel5.Visible = false;
            // 
            // Rank_Label5
            // 
            this.Rank_Label5.AutoSize = true;
            this.Rank_Label5.Enabled = false;
            this.Rank_Label5.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Rank_Label5.Location = new System.Drawing.Point(170, 17);
            this.Rank_Label5.Name = "Rank_Label5";
            this.Rank_Label5.Size = new System.Drawing.Size(23, 24);
            this.Rank_Label5.TabIndex = 2;
            this.Rank_Label5.Text = "0";
            this.Rank_Label5.Visible = false;
            // 
            // Rank_Num5
            // 
            this.Rank_Num5.AutoSize = true;
            this.Rank_Num5.Enabled = false;
            this.Rank_Num5.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Rank_Num5.Location = new System.Drawing.Point(24, 17);
            this.Rank_Num5.Name = "Rank_Num5";
            this.Rank_Num5.Size = new System.Drawing.Size(24, 24);
            this.Rank_Num5.TabIndex = 1;
            this.Rank_Num5.Text = "5";
            this.Rank_Num5.Visible = false;
            // 
            // Rank_Panel4
            // 
            this.Rank_Panel4.BackColor = System.Drawing.Color.White;
            this.Rank_Panel4.Controls.Add(this.Rank_Label4);
            this.Rank_Panel4.Controls.Add(this.Rank_Num4);
            this.Rank_Panel4.Enabled = false;
            this.Rank_Panel4.Location = new System.Drawing.Point(117, 257);
            this.Rank_Panel4.Name = "Rank_Panel4";
            this.Rank_Panel4.Size = new System.Drawing.Size(550, 50);
            this.Rank_Panel4.TabIndex = 3;
            this.Rank_Panel4.Visible = false;
            // 
            // Rank_Label4
            // 
            this.Rank_Label4.AutoSize = true;
            this.Rank_Label4.Enabled = false;
            this.Rank_Label4.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Rank_Label4.Location = new System.Drawing.Point(170, 17);
            this.Rank_Label4.Name = "Rank_Label4";
            this.Rank_Label4.Size = new System.Drawing.Size(23, 24);
            this.Rank_Label4.TabIndex = 2;
            this.Rank_Label4.Text = "0";
            this.Rank_Label4.Visible = false;
            // 
            // Rank_Num4
            // 
            this.Rank_Num4.AutoSize = true;
            this.Rank_Num4.Enabled = false;
            this.Rank_Num4.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Rank_Num4.Location = new System.Drawing.Point(24, 17);
            this.Rank_Num4.Name = "Rank_Num4";
            this.Rank_Num4.Size = new System.Drawing.Size(24, 24);
            this.Rank_Num4.TabIndex = 1;
            this.Rank_Num4.Text = "4";
            this.Rank_Num4.Visible = false;
            // 
            // Rank_Panel3
            // 
            this.Rank_Panel3.BackColor = System.Drawing.Color.White;
            this.Rank_Panel3.Controls.Add(this.Rank_Label3);
            this.Rank_Panel3.Controls.Add(this.Rank_Num3);
            this.Rank_Panel3.Enabled = false;
            this.Rank_Panel3.Location = new System.Drawing.Point(117, 189);
            this.Rank_Panel3.Name = "Rank_Panel3";
            this.Rank_Panel3.Size = new System.Drawing.Size(550, 50);
            this.Rank_Panel3.TabIndex = 3;
            this.Rank_Panel3.Visible = false;
            // 
            // Rank_Label3
            // 
            this.Rank_Label3.AutoSize = true;
            this.Rank_Label3.Enabled = false;
            this.Rank_Label3.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Rank_Label3.Location = new System.Drawing.Point(170, 17);
            this.Rank_Label3.Name = "Rank_Label3";
            this.Rank_Label3.Size = new System.Drawing.Size(23, 24);
            this.Rank_Label3.TabIndex = 2;
            this.Rank_Label3.Text = "0";
            this.Rank_Label3.Visible = false;
            // 
            // Rank_Num3
            // 
            this.Rank_Num3.AutoSize = true;
            this.Rank_Num3.Enabled = false;
            this.Rank_Num3.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Rank_Num3.Location = new System.Drawing.Point(24, 17);
            this.Rank_Num3.Name = "Rank_Num3";
            this.Rank_Num3.Size = new System.Drawing.Size(24, 24);
            this.Rank_Num3.TabIndex = 1;
            this.Rank_Num3.Text = "3";
            this.Rank_Num3.Visible = false;
            // 
            // Rank_Panel2
            // 
            this.Rank_Panel2.BackColor = System.Drawing.Color.White;
            this.Rank_Panel2.Controls.Add(this.Rank_Label2);
            this.Rank_Panel2.Controls.Add(this.Rank_Num2);
            this.Rank_Panel2.Enabled = false;
            this.Rank_Panel2.Location = new System.Drawing.Point(117, 120);
            this.Rank_Panel2.Name = "Rank_Panel2";
            this.Rank_Panel2.Size = new System.Drawing.Size(550, 50);
            this.Rank_Panel2.TabIndex = 3;
            this.Rank_Panel2.Visible = false;
            // 
            // Rank_Label2
            // 
            this.Rank_Label2.AutoSize = true;
            this.Rank_Label2.Enabled = false;
            this.Rank_Label2.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Rank_Label2.Location = new System.Drawing.Point(170, 17);
            this.Rank_Label2.Name = "Rank_Label2";
            this.Rank_Label2.Size = new System.Drawing.Size(23, 24);
            this.Rank_Label2.TabIndex = 2;
            this.Rank_Label2.Text = "0";
            this.Rank_Label2.Visible = false;
            // 
            // Rank_Num2
            // 
            this.Rank_Num2.AutoSize = true;
            this.Rank_Num2.Enabled = false;
            this.Rank_Num2.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Rank_Num2.Location = new System.Drawing.Point(24, 17);
            this.Rank_Num2.Name = "Rank_Num2";
            this.Rank_Num2.Size = new System.Drawing.Size(24, 24);
            this.Rank_Num2.TabIndex = 1;
            this.Rank_Num2.Text = "2";
            this.Rank_Num2.Visible = false;
            // 
            // Rank_Panel1
            // 
            this.Rank_Panel1.BackColor = System.Drawing.Color.White;
            this.Rank_Panel1.Controls.Add(this.Rank_Label1);
            this.Rank_Panel1.Controls.Add(this.Rank_Num1);
            this.Rank_Panel1.Enabled = false;
            this.Rank_Panel1.Location = new System.Drawing.Point(117, 48);
            this.Rank_Panel1.Name = "Rank_Panel1";
            this.Rank_Panel1.Size = new System.Drawing.Size(550, 50);
            this.Rank_Panel1.TabIndex = 2;
            this.Rank_Panel1.Visible = false;
            // 
            // Rank_Label1
            // 
            this.Rank_Label1.AutoSize = true;
            this.Rank_Label1.Enabled = false;
            this.Rank_Label1.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Rank_Label1.Location = new System.Drawing.Point(170, 17);
            this.Rank_Label1.Name = "Rank_Label1";
            this.Rank_Label1.Size = new System.Drawing.Size(23, 24);
            this.Rank_Label1.TabIndex = 2;
            this.Rank_Label1.Text = "0";
            this.Rank_Label1.Visible = false;
            // 
            // Rank_Num1
            // 
            this.Rank_Num1.AutoSize = true;
            this.Rank_Num1.Enabled = false;
            this.Rank_Num1.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Rank_Num1.Location = new System.Drawing.Point(24, 17);
            this.Rank_Num1.Name = "Rank_Num1";
            this.Rank_Num1.Size = new System.Drawing.Size(24, 24);
            this.Rank_Num1.TabIndex = 1;
            this.Rank_Num1.Text = "1";
            this.Rank_Num1.Visible = false;
            // 
            // Rank_Label
            // 
            this.Rank_Label.AutoSize = true;
            this.Rank_Label.Enabled = false;
            this.Rank_Label.Font = new System.Drawing.Font("돋움", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Rank_Label.Location = new System.Drawing.Point(283, 9);
            this.Rank_Label.Name = "Rank_Label";
            this.Rank_Label.Size = new System.Drawing.Size(177, 27);
            this.Rank_Label.TabIndex = 0;
            this.Rank_Label.Text = "Dodge Rank";
            this.Rank_Label.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(784, 961);
            this.Controls.Add(this.Rank_Panel);
            this.Controls.Add(this.DiffBox);
            this.Controls.Add(this.SelectShoot);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.SelectDodge);
            this.Controls.Add(this.DownButton);
            this.Controls.Add(this.UpButton);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.HpBar);
            this.Name = "Form1";
            this.Text = "Shooting Star";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DownKey);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UpKey);
            this.Rank_Panel.ResumeLayout(false);
            this.Rank_Panel.PerformLayout();
            this.Rank_Panel5.ResumeLayout(false);
            this.Rank_Panel5.PerformLayout();
            this.Rank_Panel4.ResumeLayout(false);
            this.Rank_Panel4.PerformLayout();
            this.Rank_Panel3.ResumeLayout(false);
            this.Rank_Panel3.PerformLayout();
            this.Rank_Panel2.ResumeLayout(false);
            this.Rank_Panel2.PerformLayout();
            this.Rank_Panel1.ResumeLayout(false);
            this.Rank_Panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.ProgressBar HpBar;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Button UpButton;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.Button SelectDodge;
        private System.Windows.Forms.Button SelectShoot;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.TextBox DiffBox;
        private System.Windows.Forms.Panel Rank_Panel;
        private System.Windows.Forms.Panel Rank_Panel1;
        private System.Windows.Forms.Label Rank_Num1;
        private System.Windows.Forms.Label Rank_Label;
        private System.Windows.Forms.Panel Rank_Panel5;
        private System.Windows.Forms.Label Rank_Num5;
        private System.Windows.Forms.Panel Rank_Panel4;
        private System.Windows.Forms.Label Rank_Num4;
        private System.Windows.Forms.Panel Rank_Panel3;
        private System.Windows.Forms.Label Rank_Num3;
        private System.Windows.Forms.Panel Rank_Panel2;
        private System.Windows.Forms.Label Rank_Num2;
        private System.Windows.Forms.Label Rank_Label5;
        private System.Windows.Forms.Label Rank_Label4;
        private System.Windows.Forms.Label Rank_Label3;
        private System.Windows.Forms.Label Rank_Label2;
        private System.Windows.Forms.Label Rank_Label1;
    }
}

