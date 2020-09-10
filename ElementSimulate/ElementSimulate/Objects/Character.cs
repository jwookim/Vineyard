using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementSimulate
{
    class Character : GameObject
    {

        public Character(Form1 form, bool aim = false):base(form.Width/2, 0, form)
        {
            Elasticity = 0.1f;
            Mass = 15f;
        }

        public override void Move()
        {

            base.Move();
        }
    }
}
