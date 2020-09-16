using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingStar
{
    class StarTail : GameObject
    {

        public int time { get; private set; }

        public StarTail(int x, int y, int size, Form1 form1):base(x, y, form1)
        {
            myPicturebox.BackColor = Color.Yellow;
            Generate(x, y, size);
        }


        public override void Move()
        {
            time--;
            myPicturebox.BackColor = Color.FromArgb(255 / 5 * time, myPicturebox.BackColor);
        }

        public override void Generate(int x, int y, int size)
        {
            myPicturebox.Width = size;
            myPicturebox.Height = size;
            time = 5;
            base.Generate(x, y, size);
        }
    }
}
