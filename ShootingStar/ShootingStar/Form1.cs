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

    }


}
