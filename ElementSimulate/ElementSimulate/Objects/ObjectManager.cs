using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElementSimulate
{
    class ObjectManager
    {
        List<GameObject> Objects = new List<GameObject>();
        Stack<GameObject> dummyObjects = new Stack<GameObject>();
        Random random = new Random();
        Form1 form1;

        public ObjectManager(Form1 _form1)
        {
            form1 = _form1;
        }
        public void Create(int x, int y)
        {
            GameObject temp;

            if (dummyObjects.Count > 0)
                temp = dummyObjects.Pop();
            else
                temp = new Pixel(x, y, form1);
            /*foreach (var ob in Objects)
            {
                if (ob.myPicturebox.Top == y && ob.myPicturebox.Left == x)
                {
                    dummyObjects.Push(temp);
                    return;
                }

            }*/
            Objects.Add(temp);
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
            foreach (var ob in Objects)
                ob.Move();
        }

        public void CollisionCheck()
        {
            foreach(var ob in Objects)
            {
                foreach(var target in Objects)
                {
                    if (ob != target)
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
                if (ob.myPicturebox.Bottom >= form1.Height - 39)
                {
                    ob.myPicturebox.Top = (form1.Height - 39) - ob.myPicturebox.Height;
                    ob.VerticalReflect();
                }

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
