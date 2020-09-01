using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElementSimulate
{
    enum Element
    {
        Void, // 어떤 영향도 받지않고 물리적 영향도 주지 않음 <- 없는게 낫지않을까?
        Air = 0, // 대류현상을 재현할 수 있을까? - 온도가 높아질수록 가볍게 만들면 빠르게 움직일 수 있고 차갑고 무거운 공기에 부딪히면 치여서 위로 올라가지 않을까?
        Fire, // 근처의 나무를 태우며 땅의 수분을 날림, 물에 닿으면 물에게 온도를 빼앗기고 일정 온도 밑으로 내려가면 불이 꺼짐
        Water, // 수원이 있으면 물방울을 만듦, 물방울은 밑으로 떨어지며 아래에 흙이나 물이 있으면 옆으로 번짐, 온도가 100도가 넘으면 사라짐
        Water_Source = 2, // 물방울을 지속적으로 생성하는 지점, 물방울이 닿아있는동안은 생성이 멈춘다
        Grass, //수분이 공급되는 한 계속 자란다, 수분이 없으면 죽음
        Earth // 물이 근처에 있으면 수분을 머금고 근처로 수분이 번져나가며, 표면의 흙이 수분을 충분히 머금으면 초목이 자람
    }
    struct Vector
    {
        public float Vertical { get; set; } // 수직
        public float Horizontal { get; set; } // 수평

        public Vector(float ver, float hor)
        {
            Vertical = ver;
            Horizontal = hor;
        }
        public static Vector operator +(Vector v1, Vector v2)
        {
            Vector tmp = new Vector();
            tmp.Vertical = v1.Vertical + v2.Vertical;
            tmp.Horizontal = v1.Horizontal + v2.Horizontal;
            return tmp;
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            Vector tmp = new Vector();
            tmp.Vertical = v1.Vertical - v2.Vertical;
            tmp.Horizontal = v1.Horizontal - v2.Horizontal;
            return tmp;
        }
        public static Vector operator *(float f, Vector v1)
        {
            Vector tmp = new Vector();
            tmp.Vertical = v1.Vertical * f;
            tmp.Horizontal = v1.Horizontal * f;
            return tmp;
        }

        public static Vector operator *(Vector v1, float f)
        {
            Vector tmp = new Vector();
            tmp.Vertical = v1.Vertical * f;
            tmp.Horizontal = v1.Horizontal * f;
            return tmp;
        }
        public static Vector operator /(Vector v1, float f)
        {
            Vector tmp = new Vector();
            tmp.Vertical = v1.Vertical / f;
            tmp.Horizontal = v1.Horizontal / f;
            return tmp;
        }

        public void HeadCut()
        {
            Vertical -= (int)Vertical;
            Horizontal -= (int)Horizontal;
        }

        public void Resist()
        {
            Vertical *= 0.98f;
            Horizontal *= 0.98f;
        }

        public void VerticalReflect()
        {
            Vertical *= -1f;
        }

        public void HorizontalReflect()
        {
            Horizontal *= -1f;
        }
    }

    

    class GameObject
    {
        //Random random = new Random();
        
        
        public PictureBox myPicturebox;

        Vector vector;
        Vector MoveInterval;

        public Vector Vec
        {
            get
            {
                return vector;
            }
        }

        private float gTime;
        private float G;
        private float Interval;
        private float Elasticity;
        public float Mass { get; private set; }

        public State myState { get; set; }


        public GameObject(int x, int y, Form1 form1)
        {
            myPicturebox = new PictureBox
            {
                Top = y,
                Left = x,
                BackColor = Color.Black,
                Visible = true,
            };
            form1.Controls.Add(myPicturebox);

            vector = default;
            MoveInterval = default;
            vector.Horizontal = (float)(new Random().Next(-10, 10) * new Random().NextDouble());
            //vector.Horizontal = 10f;
            myState = State.Drop;
            gTime = 0;
            G = 9.81f;
            Interval = 0.1f;
            Elasticity = 0.2f;
            Mass = 1f;
        }

        /*public GameObject(int x, int y, int width, int height, Form1 form1)
        {
            myPicturebox = new PictureBox
            {
                Top = y,
                Left = x,
                Width = width,
                Height = height,
                BackColor = Color.Black,
                Visible = true,
            };
            form1.Controls.Add(myPicturebox);

            vector = default;
            MoveInterval = default;
            //vector.Horizontal = (float)(random.Next(-5, 5) * random.NextDouble());
            vector.Horizontal = 50f;
            myState = State.Drop;
            time = 0;
            G = 9.81f;
            Interval = 0.1f;
            Elasticity = 0.3f;
            Mass = 1f;
        }*/

        /*public GameObject(PictureBox box)
        {
            myPicturebox = box;
            vector = default;
            MoveInterval = default;
            time = 0;
            G = 9.81f;
            Interval = 0.2f;
            Elasticity = 0f;
            Mass = 10f;

        }*/

        public virtual void Move()
        {
            MoveInterval += vector;

            myPicturebox.Left += (int)MoveInterval.Horizontal;
            myPicturebox.Top += (int)MoveInterval.Vertical;


            MoveInterval.HeadCut();


            if (gTime <= 3f)
            {
                if (vector.Vertical > -1f && vector.Vertical < 1f)
                    gTime += Interval;
            }
        }

        public void Gravity()
        {
            if (gTime <= 3f)
                vector.Vertical += G * Interval;
        }

        public void Resist()
        {
            vector.Resist();
        }

        public void Collision(Vector v, float _mass)
        {
            //vector = vector - _mass * (1f + Elasticity) / (Mass + _mass) * (vector - v); // 비탄성 충돌
            //vector = (vector * (Mass - _mass) + 2f * _mass * v) / (Mass + _mass); //완전 탄성 충돌
            //둘다 1차원...


            vector = ((Mass - Elasticity * _mass) * v.Vertical + _mass * (1 + Elasticity) * v.Vertical) / (Mass + _mass) * new Vector(1f, 0) - vector.Horizontal * new Vector(0, 1f);

            if (vector.Vertical < -1f)
                gTime = 0;
        }

        public void Overlap(int left, int top)
        {
            myPicturebox.Left += left;
            myPicturebox.Top += top;
        }

        public void VerticalReflect()
        {
            vector.Vertical *= -Elasticity;
        }

        public void HorizontalReflect()
        {
            vector.Horizontal *= -Elasticity;
        }



    }
}
