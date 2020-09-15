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

namespace ElementSimulate
{
    public partial class Form1 : Form
    {
        ObjectManager objectManager;
        public Form1()
        {
            InitializeComponent();
            objectManager = new ObjectManager(this);

            objectManager.TailCreate(100, 100, 20);
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
            objectManager.Dodge_Star();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void DownKey(object sender, KeyEventArgs e)
        {
            objectManager.KeyDown(e);
        }

        private void UpKey(object sender, KeyEventArgs e)
        {
            objectManager.KeyUp(e);
        }

        private void PressKey(object sender, KeyPressEventArgs e)
        {
            objectManager.KeyPress(e);
        }
    }
}
