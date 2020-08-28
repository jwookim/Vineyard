using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElementSwap
{
    abstract class IObject
    {
        public PictureBox myPicturebox;
        public State myState { get; set; }
        abstract public void Move();
    }
}
