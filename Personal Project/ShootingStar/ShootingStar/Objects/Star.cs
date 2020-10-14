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

        public Star(int x, int y, Form1 form1) : base(x, y, form1)
        {
            //myPicturebox.SizeMode = PictureBoxSizeMode.Normal;
            myPicturebox.BackColor = Color.Yellow;
            Elasticity = 1f;
        }

        public override void Move()
        {
            base.Move();
        }

        public void Drop_Shift(int diff)
        {
            size = random.Next(3, 11) + diff;
            Mass = size;
            vector.Horizontal = (float)(random.Next(-40, 40) * random.NextDouble());
            vector.Vertical = (float)(random.Next(20) * random.NextDouble());
        }

        public void Throw_Shift(int diff)
        {
            size = random.Next(20, 30) - diff;
            Mass = size;
            int power = random.Next(40, 70);
            double rad = (double)random.Next(45, 81) / 180d * 3.141592d;

            vector.Horizontal = (float)Math.Cos(rad) * power;
            vector.Vertical = -(float)Math.Sin(rad) * power;
        }

        public override void Generate(int x, int y, int diff)
        {
            myPicturebox.Width = size;
            myPicturebox.Height = size;
            base.Generate(x, y, diff);
        }

    }
}
