using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElementSimulate
{
    class ObjectManager
    {
        List<GameObject> Objects = new List<GameObject>();
        Stack<Star> dummyObjects = new Stack<Star>();
        Stack<StarTail> dummyTails = new Stack<StarTail>();
        Random random = new Random();
        Form1 form1;

        public ObjectManager(Form1 _form1)
        {
            form1 = _form1;
            form1.BackColor = Color.MidnightBlue;
            form1.Height = 1000;
        }

        public void StarCreate(int x, int y)
        {
            Star temp;

            if (dummyObjects.Count > 0)
            {
                temp = dummyObjects.Pop();
                temp.Generate(x, y);
            }
            else
                temp = new Star(x, y, form1);

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

        public void Rainism()
        {
            int num = random.Next(-10, 3);

            for (int i = 0; i < num; i++)
                StarCreate(random.Next(10, form1.Width - 10), 0);
        }
        /*public void Add(PictureBox ob)
        {
            GameObject temp = new GameObject(ob);
            Objects.Add(temp);
        }*/

        /*public void Check()
        {
            foreach (var ob in Objects)
            {
                switch (ob.myState)
                {
                    case State.Drop:
                        foreach (var target in Objects)
                        {
                            if (ob.myPicturebox.Top + 1 == target.myPicturebox.Top)
                            {

                            }
                        }
                        break;
                }
            }
        }*/


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

        public void CollisionCheck()
        {
            foreach (var ob in Objects)
            {
                if (ob.GetType() == typeof(Star))
                {
                    foreach (var target in Objects)
                    {
                        if (ob != target && target.GetType() == typeof(Star))
                        {
                            if (ob.myPicturebox.Bounds.IntersectsWith(target.myPicturebox.Bounds))
                            {
                                ob.Collision(target.Vec, target.Mass);
                                target.Collision(ob.Vec, ob.Mass);

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

        public void ExtinctionCheck()
        {
            int num = 0;
            while (num < Objects.Count)
            {
                var ob = Objects[num];

                if (ob.GetType() == typeof(Star))
                {
                    if (ob.myPicturebox.Bottom >= form1.Height)
                    {
                        ob.Extinction();
                        dummyObjects.Push((Star)ob);
                        Objects.Remove(ob);
                        continue;
                    }
                    else
                        num++;
                }

                else if(ob.GetType() == typeof(StarTail))
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
            }
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
    }
}
