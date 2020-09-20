using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootingStar
{
    /*enum State
    {
        Drop,
        Stop,
        Spread
    }*/

    class Star : GameObject
    {

        Random random = new Random();

        private int size;
        /*protected Element myElement { get; set; }

        protected int Temperature { get; set; }
        protected int Moisture { get; set; }*/

        public Star(int x, int y, int diff, Form1 form1) : base(x, y, form1)
        {
            //myPicturebox.SizeMode = PictureBoxSizeMode.Normal;
            myPicturebox.BackColor = Color.Yellow;
            Elasticity = 1f;
            Generate(x, y, diff);
        }

        public override void Move()
        {
            base.Move();
        }

        public override void Generate(int x, int y, int diff)
        {
            size = random.Next(3 + diff, 11 + diff);
            Mass = (float)size;
            myPicturebox.Width = size;
            myPicturebox.Height = size;
            vector.Horizontal = (float)(random.Next(-40, 40) * random.NextDouble());
            vector.Vertical = (float)(random.Next(20) * random.NextDouble());
            base.Generate(x, y, diff);
        }
    }
}
