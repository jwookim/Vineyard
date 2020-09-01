using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElementSimulate
{
    enum State
    {
        Drop,
        Stop,
        Spread
    }

    class Pixel : GameObject
    {

        protected Element myElement { get; set; }

        protected int Temperature { get; set; }
        protected int Moisture { get; set; }

        public Pixel(int x, int y, Form1 form1) : base(x, y, form1)
        {
            myPicturebox.SizeMode = PictureBoxSizeMode.Normal;
            myPicturebox.Height = 3;
            myPicturebox.Width = 3;
        }

        public override void Move()
        {
            base.Move();
        }

    }
}
