using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementSwap
{
    class ObjectManager
    {
        List<GameObject> Objects = new List<GameObject>();
        Stack<GameObject> dummyObjects = new Stack<GameObject>(); 
        public void Create(int x, int y)
        {
            GameObject temp;
            if (dummyObjects.Count > 0)
                temp = dummyObjects.Pop();
            temp = new Pixel(x, y);
            foreach(var ob in Objects)
            {
                if (ob.myPicturebox.Top == y && ob.myPicturebox.Left == x)
                {
                    dummyObjects.Push(temp);
                    return;
                }

            }
            Objects.Add(temp);
        }

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
    }
}
