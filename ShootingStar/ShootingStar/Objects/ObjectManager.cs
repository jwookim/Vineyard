using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootingStar
{
    /*struct Point
    {
        public int x;
        public int y;


        static public bool operator ==(Point p1, Point p2)
        {
            if (p1.x == p2.x && p1.y == p2.y)
                return true;

            return false;
        }

        static public bool operator !=(Point p1, Point p2)
        {
            if (p1.x == p2.x && p1.y == p2.y)
                return false;

            return true;
        }
    }*/
    class ObjectManager
    {
        List<GameObject> Objects = new List<GameObject>();
        Stack<Star> dummyObjects = new Stack<Star>();
        Stack<StarTail> dummyTails = new Stack<StarTail>();
        Random random = new Random();
        Form1 form1;

        Pen myPen;
        Pen Eraser;
        Graphics graphics;

        PointF AimStart;
        PointF AimEnd;

        Character Player;

        public ObjectManager(Form1 _form1)
        {
            form1 = _form1;
            Player = new Character(form1);
            Objects.Add(Player);
            AimStart = default;
            AimEnd = default;
            myPen = new Pen(Color.Red);
            Eraser = new Pen(form1.BackColor);
            graphics = form1.CreateGraphics();
        }


        public void Init()
        {
            graphics.Clear(form1.BackColor);
            AimStart = default;
            AimEnd = default;
            int num = 0;
            while (num < Objects.Count)
            {
                var ob = Objects[num];

                if (ob.GetType() == typeof(Star))
                {
                    ob.Extinction();
                    dummyObjects.Push((Star)ob);
                    Objects.Remove(ob);
                    continue;
                }

                else if (ob.GetType() == typeof(StarTail))
                {
                    ob.Extinction();
                    dummyTails.Push((StarTail)ob);
                    Objects.Remove(ob);
                    continue;
                }

                else if (ob == Player)
                    num++;
            }

            Player.Init();
        }
        

        

        public void StarCreate(int x, int y, int Difficulty)
        {
            Star temp;

            if (dummyObjects.Count > 0)
            {
                temp = dummyObjects.Pop();
                temp.Generate(x, y, Difficulty);
            }
            else
                temp = new Star(x, y, Difficulty, form1);

            Objects.Add(temp);
        }

        public void TailCreate(int x, int y, int size)
        {
            StarTail temp;

            if (dummyTails.Count > 0)
            {
                temp = dummyTails.Pop();
                temp.Generate(x, y, size);
            }
            else
                temp = new StarTail(x, y, size, form1);

            Objects.Add(temp);
        }

        public void Rainism(int Difficulty)
        {
            int num = random.Next(-10 - Difficulty / 2, 3 + Difficulty / 2);

            for (int i = 0; i < num; i++)
                StarCreate(random.Next(10, form1.Width - 10), 0, Difficulty);
        }

        public int HpCheck()
        {
            return (int)Player.Health;
        }

        public void Gravity()
        {
            foreach (var ob in Objects)
                ob.Gravity();
        }

        public void Move()
        {
            int num = 0;
            while (num < Objects.Count)
            {
                var ob = Objects[num];
                if (ob.GetType() == typeof(Star))
                    TailCreate(ob.myPicturebox.Left, ob.myPicturebox.Top, ob.myPicturebox.Width / 2);
                ob.Move();

                num++;
            }
        }

        public void AimLine()
        {
            PointF moveStart = default;
            PointF moveEnd = default;

            double dir = 1;

            if (Player.direct == Direct.Left)
                dir = -1;

            double rad = (double)Player.Angle / 180d * 3.141592;
            moveStart.X = Player.myPicturebox.Left + Player.myPicturebox.Width / 2;
            moveStart.Y = Player.myPicturebox.Top + Player.myPicturebox.Height / 2;
            moveEnd.X = Player.myPicturebox.Left + Player.myPicturebox.Width / 2 + (float)(Math.Cos(rad) * 100d * dir);
            moveEnd.Y = Player.myPicturebox.Top + Player.myPicturebox.Height / 2 - (float)(Math.Sin(rad) * 100d);

            if (!moveStart.Equals(AimStart) || !moveEnd.Equals(AimEnd))
            {
                graphics.DrawLine(Eraser, AimStart, AimEnd);
                graphics.DrawLine(myPen, moveStart, moveEnd);

                AimStart = moveStart;
                AimEnd = moveEnd;
            }
        }

        public void CollisionCheck()
        {
            foreach (var ob in Objects)
            {
                if (ob.GetType() != typeof(StarTail))
                {
                    foreach (var target in Objects)
                    {
                        if (ob != target && target.GetType() != typeof(StarTail))
                        {
                            if (ob.myPicturebox.Bounds.IntersectsWith(target.myPicturebox.Bounds))
                            {
                                var obVec = ob.Vec;
                                var obMass = ob.Mass;
                                ob.Collision(target.Vec, target.Mass);
                                target.Collision(obVec, obMass);

                                Overlap(ob, target);
                            }
                        }
                    }


                    //맵 밖으로 나가려 할 경우
                    //if (ob.myPicturebox.Bottom >= form1.Height - 39)
                    //{
                    //    ob.myPicturebox.Top = (form1.Height - 39) - ob.myPicturebox.Height;
                    //    ob.VerticalReflect();
                    //}

                    if (ob.myPicturebox.Left < 0)
                    {
                        ob.myPicturebox.Left = 0;
                        ob.HorizontalReflect();
                    }

                    if (ob.myPicturebox.Right > form1.Width - 15)
                    {
                        ob.myPicturebox.Left = form1.Width - 15 - ob.myPicturebox.Width;
                        ob.HorizontalReflect();
                    }
                }
            }
        }

        public int ExtinctionCheck()
        {
            int score = 0;
            int num = 0;
            while (num < Objects.Count)
            {
                var ob = Objects[num];

                if (ob.GetType() == typeof(Star))
                {
                    if (ob.myPicturebox.Bottom >= form1.Height)
                    {
                        score += (int)ob.Mass;
                        ob.Extinction();
                        dummyObjects.Push((Star)ob);
                        Objects.Remove(ob);
                        continue;
                    }
                    else
                        num++;
                }

                else if (ob.GetType() == typeof(StarTail))
                {
                    if (((StarTail)ob).time <= 0)
                    {
                        ob.Extinction();
                        dummyTails.Push((StarTail)ob);
                        Objects.Remove(ob);
                        continue;
                    }
                    else
                        num++;
                }

                else if (ob == Player)
                {
                    if (ob.myPicturebox.Bottom >= form1.Height - 39 && ob.Vec.Vertical > 0)
                    {
                        ((Character)ob).Landing();
                        ob.myPicturebox.Top = (form1.Height - 39) - ob.myPicturebox.Height;
                    }
                    num++;
                }
            }

            return score;
        }


        public void Resist()
        {
            foreach (var ob in Objects)
                ob.Resist();
        }

        public void Overlap(GameObject ob1, GameObject ob2)
        {
            int left;
            int top;
            if(ob1.myPicturebox.Left <= ob2.myPicturebox.Left)
            {
                left = ob1.myPicturebox.Right - ob2.myPicturebox.Left;

                if (ob1.myPicturebox.Top < ob2.myPicturebox.Top)
                {
                    top = ob1.myPicturebox.Bottom - ob2.myPicturebox.Top;

                    ob1.Overlap(left / 2 % 2 == 0 ? -(left / 2) : -(left / 2 + 1), top / 2 % 2 == 0 ? -(top / 2) : -(top / 2 + 1));
                    ob2.Overlap(left / 2, top / 2);
                }
                else
                {
                    top = ob2.myPicturebox.Bottom - ob1.myPicturebox.Top;
                    ob1.Overlap(left / 2 % 2 == 0 ? -(left / 2) : -(left / 2 + 1), top / 2 % 2 == 0 ? top / 2 : top / 2 + 1);
                    ob2.Overlap(left / 2, -(top / 2));
                }

            }
            else
            {
                left = ob2.myPicturebox.Right - ob1.myPicturebox.Left;

                if (ob1.myPicturebox.Top < ob2.myPicturebox.Top)
                {
                    top = ob1.myPicturebox.Bottom - ob2.myPicturebox.Top;

                    ob1.Overlap(left / 2 % 2 == 0 ? left / 2 : left / 2 + 1, top / 2 % 2 == 0 ? -(top / 2) : -(top / 2 + 1));
                    ob2.Overlap(-(left / 2), top / 2);
                }
                else
                {
                    top = ob2.myPicturebox.Bottom - ob1.myPicturebox.Top;
                    ob1.Overlap(left / 2 % 2 == 0 ? left / 2 : left / 2 + 1, top / 2 % 2 == 0 ? top / 2 : top / 2 + 1);
                    ob2.Overlap(-(left / 2), -(top / 2));
                }
            }

            
        }

        public void ChangeAttackable(bool _atk)
        {
            Player.ChangeAttackable(_atk);
        }

        public void KeyUp(KeyEventArgs e)
        {
            Player.Control_Up(e);
        }

        public void KeyDown(KeyEventArgs e)
        {
            Player.Control_Down(e);
        }

    }
}
