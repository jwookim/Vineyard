using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementSwap
{
    class ObjectManager : IObject
    {
        List<IObject> Objects = new List<IObject>();
        List<IObject> 
        public void Create(int x, int y)
        {
            Pixel temp = new Pixel(x, y);
            foreach(var ob in Objects)
            {
                if (ob.myPicturebox.Top == y && ob.myPicturebox.Left == x)
                {
                    temp.
                    return;
                }
            }
            Objects.Add(temp);
        }

        public void Check()
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
        }

        public override void Move()
        {

        }
    }
}
