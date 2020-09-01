using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElementSwap
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

        public Pixel(int x, int y) : base(x, y)
        {
            
            

        }

        public override void Move()
        {
            /*switch(myState)
            {
                case State.Drop:

                    break;
                case State.Spread:
                    if(time >= (int)myElement)
                    time++;
                    break;
            }*/
        }



        /*public void Drop()
        {
            myPicturebox.Top++;
        }

        public void Spread()
        {

        }*/


    }
}
