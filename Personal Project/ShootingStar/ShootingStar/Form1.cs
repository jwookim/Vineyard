using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootingStar
{
    public partial class Form1 : Form
    {
        GameManager Gm;
        public Form1()
        {
            InitializeComponent();
            Gm = new GameManager(this);

            /*objectManager.Create(150, 0);
            objectManager.Create(100, 0);*/
            //for(int x= 50; x<Width; x+=50)
            //{
            //    objectManager.Create(x, 0);
            //}
            

        }

        public void HpUpdate(int hp)
        {
            HpBar.Value = hp;
        }

        public void HpBarToggle(bool toggle)
        {
            HpBar.Visible = toggle;
        }

        public void ScoreUpdate(int score)
        {
            ScoreLabel.Text = "Score : " + score.ToString();
        }

        private void GameTimer_Tick_1(object sender, EventArgs e)
        {
            if (Gm.Playing)
                Gm.Game();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void DownKey(object sender, KeyEventArgs e)
        {
            Gm.KeyDown(e);
            e.SuppressKeyPress = true;

        }

        private void UpKey(object sender, KeyEventArgs e)
        {
            Gm.KeyUp(e);
        }

        private void UpButton_Click(object sender, EventArgs e)
        {
            try
            {
                int diff = int.Parse(DiffBox.Text) + 1;

                if (diff > 5)
                    diff = 5;
                else if (diff < 0)
                    diff = 0;

                DiffBox.Text = diff.ToString();
            }
            catch(Exception)
            {
                DiffBox.Text = "0";
            }
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            try
            {
                int diff = int.Parse(DiffBox.Text) - 1;

                if (diff > 5)
                    diff = 5;
                else if (diff < 0)
                    diff = 0;

                DiffBox.Text = diff.ToString();
            }
            catch (Exception)
            {
                DiffBox.Text = "0";
            }
        }

        private void SelectDodge_Click(object sender, EventArgs e)
        {
            Gm.Dodge_Star_Setting();
        }

        private void SelectShoot_Click(object sender, EventArgs e)
        {
            Gm.Shooting_Star_Setting();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            try
            {
                int diff = int.Parse(DiffBox.Text);

                if (diff > 5)
                    diff = 5;
                else if (diff < 0)
                    diff = 0;

                Gm.Start(diff);

            }
            catch(Exception)
            {
                DiffBox.Text = "0";
            }
        }


        public void ToggleVisible(bool toggle)
        {
            UpButton.Enabled = toggle;
            UpButton.Visible = toggle;
            DownButton.Enabled = toggle;
            DownButton.Visible = toggle;
            DiffBox.Enabled = toggle;
            DiffBox.Visible = toggle;

            SelectDodge.Enabled = toggle;
            SelectDodge.Visible = toggle;
            SelectShoot.Enabled = toggle;
            SelectShoot.Visible = toggle;

            Start.Enabled = toggle;
            Start.Visible = toggle;
        }

        public void ToggleRank(bool toggle)
        {
            Rank_Label.Visible = toggle;
            Rank_Label1.Visible = toggle;
            Rank_Label2.Visible = toggle;
            Rank_Label3.Visible = toggle;
            Rank_Label4.Visible = toggle;
            Rank_Label5.Visible = toggle;
            Rank_Num1.Visible = toggle;
            Rank_Num2.Visible = toggle;
            Rank_Num3.Visible = toggle;
            Rank_Num4.Visible = toggle;
            Rank_Num5.Visible = toggle;
            Rank_Panel.Visible = toggle;
            Rank_Panel1.Visible = toggle;
            Rank_Panel2.Visible = toggle;
            Rank_Panel3.Visible = toggle;
            Rank_Panel4.Visible = toggle;
            Rank_Panel5.Visible = toggle;

            Rank_Label.Enabled = toggle;
            Rank_Label1.Enabled = toggle;
            Rank_Label2.Enabled = toggle;
            Rank_Label3.Enabled = toggle;
            Rank_Label4.Enabled = toggle;
            Rank_Label5.Enabled = toggle;
            Rank_Num1.Enabled = toggle;
            Rank_Num2.Enabled = toggle;
            Rank_Num3.Enabled = toggle;
            Rank_Num4.Enabled = toggle;
            Rank_Num5.Enabled = toggle;
            Rank_Panel.Enabled = toggle;
            Rank_Panel1.Enabled = toggle;
            Rank_Panel2.Enabled = toggle;
            Rank_Panel3.Enabled = toggle;
            Rank_Panel4.Enabled = toggle;
            Rank_Panel5.Enabled = toggle;
        }

        public void Rank_Swap(bool Atkable)
        {
            if (Atkable)
                Rank_Label.Text = "Shooting Rank";
            else
                Rank_Label.Text = "Dodge Rank";
        }

        public void Rank_Update(int rank, int score, bool now = false)
        {
            switch(rank + 1)
            {
                case 1:
                    Rank_Label1.Text = score.ToString();
                    if (now)
                        Rank_Label1.ForeColor = Color.Red;
                    else
                        Rank_Label1.ForeColor = Color.Black;
                    break;
                case 2:
                    Rank_Label2.Text = score.ToString();
                    if (now)
                        Rank_Label2.ForeColor = Color.Red;
                    else
                        Rank_Label2.ForeColor = Color.Black;
                    break;
                case 3:
                    Rank_Label3.Text = score.ToString();
                    if (now)
                        Rank_Label3.ForeColor = Color.Red;
                    else
                        Rank_Label3.ForeColor = Color.Black;
                    break;
                case 4:
                    Rank_Label4.Text = score.ToString();
                    if (now)
                        Rank_Label4.ForeColor = Color.Red;
                    else
                        Rank_Label4.ForeColor = Color.Black;
                    break;
                case 5:
                    Rank_Label5.Text = score.ToString();
                    if (now)
                        Rank_Label5.ForeColor = Color.Red;
                    else
                        Rank_Label5.ForeColor = Color.Black;
                    break;
            }
        }

        private void Rank_Panel_Clicked(object sender, MouseEventArgs e)
        {
            ToggleRank(false);
        }
    }


}
