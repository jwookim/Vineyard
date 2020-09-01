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

            /*objectManager.Create(150, 0);
            objectManager.Create(100, 0);*/

            for (int x = 50; x < Width; x += 25)
            {
                for (int y = 0; y < 50; y += 25)
                {
                    objectManager.Create(x, 0);
                    Thread.Sleep(10);
                }
            }

        }


        private void GameTimer_Tick_1(object sender, EventArgs e)
        {
            objectManager.Gravity();

            objectManager.Move();

            objectManager.CollisionCheck();

            objectManager.Resist();
        }

    }
}
