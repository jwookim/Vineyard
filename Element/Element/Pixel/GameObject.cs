using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElementSwap
{
    enum Element
    {
        Void, // 어떤 영향도 받지않고 물리적 영향도 주지 않음 <- 없는게 낫지않을까?
        Air, // 대류현상을 재현할 수 있을까? - 온도가 높아질수록 가볍게 만들면 빠르게 움직일 수 있고 차갑고 무거운 공기에 부딪히면 치여서 위로 올라가지 않을까?
        Fire, // 근처의 나무를 태우며 땅의 수분을 날림, 물에 닿으면 물에게 온도를 빼앗기고 일정 온도 밑으로 내려가면 불이 꺼짐
        Water, // 수원이 있으면 물방울을 만듦, 물방울은 밑으로 떨어지며 아래에 흙이나 물이 있으면 옆으로 번짐, 온도가 100도가 넘으면 사라짐
        Water_Source, // 물방울을 지속적으로 생성하는 지점, 물방울이 닿아있는동안은 생성이 멈춘다
        Grass, //수분이 공급되는 한 계속 자란다, 수분이 없으면 죽음
        Earth // 물이 근처에 있으면 수분을 머금고 근처로 수분이 번져나가며, 표면의 흙이 수분을 충분히 머금으면 초목이 자람
    }
    struct Vector
    {
        public float Vertical { get; set; } // 수직
        public float Horizontal { get; set; } // 수평
    }
    abstract class GameObject
    {
        public PictureBox myPicturebox;

        Vector vector;

        private int time;

        private float moveX;
        private float moveY;

        private float G = 9.81f;
        private float gInterval = 0.2f;

        public State myState { get; set; }
        abstract public void Move();


        public GameObject(int x, int y)
        {
            myPicturebox = new PictureBox();
            myPicturebox.Top = y;
            myPicturebox.Left = x;
            myPicturebox.BackColor = Color.Black;
            myPicturebox.Visible = true;
            myState = State.Drop;
        }

        public void Gravity()
        {
            vector.Vertical += G * gInterval;
        }
    }
}
