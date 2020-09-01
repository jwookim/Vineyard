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

        public Pixel(int x, int y, int size, Form1 form1) : base(x, y, form1)
        {
            myPicturebox.SizeMode = PictureBoxSizeMode.Normal;
            myPicturebox.Height = size;
            myPicturebox.Width = size;
        }

        public override void Move()
        {
            base.Move();
        }

        public override void Generate(int x, int y, int size)
        {
            myPicturebox.Width = size;
            myPicturebox.Height = size;
            base.Generate(x, y, size);
        }
    }
}
