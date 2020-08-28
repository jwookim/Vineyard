using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElementSwap
{
    enum Element
    {
        Void, // 어떤 영향도 받지않고 물리적 영향도 주지 않음 <- 없는게 낫지않을까?
        Fire, // 근처의 나무를 태우며 땅의 수분을 날림, 물에 닿으면 물에게 온도를 빼앗기고 일정 온도 밑으로 내려가면 불이 꺼짐
        Water, // 수원이 있으면 물방울을 만듦, 물방울은 밑으로 떨어지며 아래에 흙이나 물이 있으면 옆으로 번짐, 온도가 100도가 넘으면 사라짐
        Water_Source, // 물방울을 지속적으로 생성하는 지점, 물방울이 닿아있는동안은 생성이 멈춘다
        Grass, //수분이 공급되는 한 계속 자란다, 수분이 없으면 죽음
        Earth // 물이 근처에 있으면 수분을 머금고 근처로 수분이 번져나가며, 표면의 흙이 수분을 충분히 머금으면 초목이 자람
    }

    enum State
    {
        Drop,
        Stop,
        Spread
    }

    class Pixel : IObject
    {
        
        
        protected Element myElement { get; set; }
        protected int Temperature { get; set; }
        protected int Moisture { get; set; }

        private int time;
        public Pixel(int x, int y)
        {
            myPicturebox = new PictureBox();
            myPicturebox.Top = y;
            myPicturebox.Left = x;
            myPicturebox.Visible = true;
            myState = State.Drop;
            time = 0;

        }

        public override void Move()
        {
            switch(myState)
            {
                case State.Drop:

                    break;
                case State.Spread:
                    if(time >= (int)myElement)
                    time++;
                    break;
            }
        }



        public void Drop()
        {
            myPicturebox.Top++;
        }

        public void Spread()
        {

        }


    }
}
