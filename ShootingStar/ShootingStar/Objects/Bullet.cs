using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootingStar
{
    class Bullet : GameObject
    {
        float Speed;
        public Bullet(int x, int y, Form1 form) : base(x, y, form)
        {
            myPicturebox.Image = Properties.Resources.Bullet1;
            myPicturebox.SizeMode = PictureBoxSizeMode.AutoSize;
            Generate(x, y, 0);
        }

        public override void Gravity()
        {
        }

        public override void Resist()
        {
        }

        public void SetDirect(int angle, Direct _dir)
        {
            float dir = 1f;

            if (_dir == Direct.Left)
                dir *= -1f;

            double rad = angle / 180d * 3.141592d;
            vector.Vertical = -(float)Math.Sin(rad) * Speed;
            vector.Horizontal = (float)Math.Cos(rad) * Speed * dir;
        }

        public override void Generate(int x, int y, int diff)
        {
            Speed = 20f;
            myPicturebox.SendToBack();
            base.Generate(x, y, diff);
        }

    }
}
