using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElementSimulate
{
    class Character : GameObject
    {

        bool Attackable;
        float AtkCooldown;
        const float AtkDelay = 1.0f;
        public Character(Form1 form, bool _atkable = false):base(form.Width/2, 0, form)
        {
            Elasticity = 0.1f;
            Mass = 15f;
            Attackable = _atkable;

            myPicturebox.SizeMode = PictureBoxSizeMode.AutoSize;
            myPicturebox.Image = Properties.Resources.RockMan_Idle_R_01;
        }

        public override void Move()
        {
            if (AtkCooldown < AtkDelay)
                AtkCooldown += Interval;

            base.Move();
        }

        public void Control_Up(KeyEventArgs e)
        {

        }

        public void Control_Down(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {

            }

            if(Attackable)
            {
                if (e.KeyCode == Keys.Control)
                {

                }
            }
        }

        public void Control_Press(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                vector += new Vector(-5.0f, 0f);
            }

            if (e.KeyCode == Keys.Right)
            {

            }
        }
    }
}
