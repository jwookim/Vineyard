using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwitch
{
    public bool push { get; set; }

    bool StateCheck();
}
