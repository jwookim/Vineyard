using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingStar
{
    class Bullet : GameObject
    {
        float Speed;
        public Bullet(int x, int y, int diff, Form1 form) : base(x, y, form)
        {
            Generate(x, y, diff);
        }

        public override void Gravity()
        {
        }

        public override void Resist()
        {
        }

        public override void Generate(int x, int y, int diff)
        {
            Speed = 20f - diff;
            base.Generate(x, y, diff);
        }
    }
}
