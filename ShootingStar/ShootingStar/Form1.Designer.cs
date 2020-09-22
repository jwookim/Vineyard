﻿namespace ShootingStar
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(784, 961);
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
    }
}

